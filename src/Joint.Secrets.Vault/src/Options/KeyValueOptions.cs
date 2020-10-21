namespace Joint.Secrets.Vault.Options
{
    public class KeyValueOptions
    {
        public bool Enabled { get; set; }
        public bool AllInOne { get; set; } = true;
        public int EngineVersion { get; set; } = 2;
        public string MountPoint { get; set; } = "kv";
        public string Path { get; set; }
        public int? Version { get; set; }
    }
}