using System.Security.Cryptography.X509Certificates;

public class CertificatePolicy 
{
    public bool RequireCertificate { get; set; }

    public CertificatePolicy(bool requireCertificate)
    {
        RequireCertificate = requireCertificate;
    }

    public bool ValidateCertificate(X509Certificate2 clientCertificate)
    {
        // You can add custom validation logic here
        // For example, you can check the certificate's subject name, issuer name, or expiration date
        // Here's a simple example that checks if the certificate is not null and has a valid subject name
        if (clientCertificate == null)
        {
            return false;
        }

        string subjectName = clientCertificate.SubjectName.Name;
        if (string.IsNullOrEmpty(subjectName))
        {
            return false;
        }

        // You can add more validation logic here
        return true;
    }
}