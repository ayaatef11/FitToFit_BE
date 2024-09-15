using System.Collections.ObjectModel;

namespace SharedKernal.ModuleInstaller
{
    public sealed record InstallResult
    {
        public Collection<Type> RegisteredDatabases { get; set; } = new();
    }
}
