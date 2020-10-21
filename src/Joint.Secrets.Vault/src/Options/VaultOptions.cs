using System.Collections.Generic;

namespace Joint.Secrets.Vault.Options
{
    public class VaultOptions
    {
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public string AuthType { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RevokeLeaseOnShutdown { get; set; }
        public int RenewalsInterval { get; set; }
        public KeyValueOptions Kv { get; set; }
        public PkiOptions Pki { get; set; }
        public IDictionary<string, LeaseOptions> Lease { get; set; }
    }
}