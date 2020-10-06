using System.Collections.Generic;

namespace Joint.Secrets.Vault
{
    public interface ILeaseService
    {
        IReadOnlyDictionary<string, LeaseData> All { get; }
        LeaseData Get(string key);
        void Set(string key, LeaseData data);
    }
}