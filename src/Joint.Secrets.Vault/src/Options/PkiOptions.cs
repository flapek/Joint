namespace Joint.Secrets.Vault.Options
{
    public class PkiOptions
    {
        public bool Enabled { get; set; }
        public string RoleName { get; set; }
        public string MountPoint { get; set; }
        public string CertificateFormat { get; set; }
        public string PrivateKeyFormat { get; set; }
        public string CommonName { get; set; }
        public string TTL { get; set; }
        public string SubjectAlternativeNames { get; set; }
        public string OtherSubjectAlternativeNames { get; set; }
        public bool ExcludeCommonNameFromSubjectAlternativeNames { get; set; }
        public string IPSubjectAlternativeNames { get; set; }
        public string URISubjectAlternativeNames { get; set; }
        public bool ImportPrivateKey { get; set; }
    }
}