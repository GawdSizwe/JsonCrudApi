
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class HmacAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKey = "your-api-key"; // Replace with your actual API key

public HmacAuthMiddleware(RequestDelegate next)
{
    _next = next;
}

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization header missing");
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();
        if (!IsValidHmac(authHeader, context.Request))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid HMAC");
            return;
        }

        await _next(context);
    }

    private bool IsValidHmac(string authHeader, HttpRequest request)
    {
        var secretKey = Encoding.UTF8.GetBytes(ApiKey);
        using (var hmac = new HMACSHA256(secretKey))
        {
            var requestContent = $"{request.Method}{request.Path}{request.QueryString}";
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestContent));
            var computedHashString = Convert.ToBase64String(computedHash);

            return authHeader == $"HMAC {computedHashString}";
        }
    }
}
