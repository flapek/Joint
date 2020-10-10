using System.Collections.Generic;

namespace Joint.Secrets.Vault.Options
{
    public class LeaseOptions
    {
        public bool Enabled { get; set; }
        public string Type { get; set; }
        public string RoleName { get; set; }
        public string MountPoint { get; set; }
        public bool AutoRenewal { get; set; }
        public IDictionary<string, string> Templates { get; set; }
    }
}