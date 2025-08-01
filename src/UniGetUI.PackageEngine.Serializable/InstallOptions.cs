using System.Collections.Concurrent;
using System.Text;
using System.Text.Json.Nodes;
using UniGetUI.Core.Data;

namespace UniGetUI.PackageEngine.Serializable
{
    public class InstallOptions: SerializableComponent<InstallOptions>
    {
        private const string SKIP_HASH = "SkipHashCheck";
        private const string INTERACTIVE = "InteractiveInstallation";
        private const string AS_ADMIN = "RunAsAdministrator";
        private const string PRERELEASE = "PreRelease";
        private const string SKIP_MINOR = "SkipMinorUpdates";
        private const string REMOVE_DATA_UNINST = "RemoveDataOnUninstall";
        private const string CLEAR_PREV_VER = "UninstallPreviousVersionsOnUpdate";
        private const string ABORT_PRE_INST_FAIL = "AbortOnPreInstallFail";
        private const string ABORT_PRE_UPD_FAIL = "AbortOnPreUpdateFail";
        private const string ABORT_PRE_UNINST_FAIL = "AbortOnPreUninstallFail";

        private const string ARCH = "Architecture";
        private const string SCOPE = "InstallationScope";
        private const string LOCATION = "CustomInstallLocation";
        private const string VERSION = "Version";
        private const string PRE_INST_CMD = "PreInstallCommand";
        private const string POST_INST_CMD = "PostInstallCommand";
        private const string PRE_UPD_CMD = "PreUpdateCommand";
        private const string POST_UPD_CMD = "PostUpdateCommand";
        private const string PRE_UNINST_CMD = "PreUninstallCommand";
        private const string POST_UNINST_CMD = "PostUninstallCommand";

        private const string INST_PARAMS = "CustomParameters_Install";
        private const string UPD_PARAMS = "CustomParameters_Update";
        private const string UNINST_PARAMS = "CustomParameters_Uninstall";
        private const string KILL_BEFORE_OP = "KillBeforeOperation";

        public readonly IReadOnlyDictionary<string, bool> _defaultBoolValues = new Dictionary<string, bool>()
        {   // OverridesNextLevelOpts is deliberately skipped here
            { SKIP_HASH, false },
            { INTERACTIVE, false },
            { AS_ADMIN, false },
            { PRERELEASE, false },
            { SKIP_MINOR, false },
            { REMOVE_DATA_UNINST, false },
            { CLEAR_PREV_VER, false },
            { ABORT_PRE_INST_FAIL, true },
            { ABORT_PRE_UPD_FAIL, true },
            { ABORT_PRE_UNINST_FAIL, true },
        };

        public readonly IReadOnlyList<string> _stringKeys = [
            ARCH,
            SCOPE,
            LOCATION,
            VERSION,
            PRE_INST_CMD,
            POST_INST_CMD,
            PRE_UPD_CMD,
            POST_UPD_CMD,
            PRE_UNINST_CMD,
            POST_UNINST_CMD,
        ];

        public readonly IReadOnlyList<string> _listKeys = [
            INST_PARAMS,
            UPD_PARAMS,
            UNINST_PARAMS,
            KILL_BEFORE_OP,
        ];

        public readonly ConcurrentDictionary<string, bool> _boolVal = new();
        public readonly ConcurrentDictionary<string, string> _strVal = new();
        public readonly ConcurrentDictionary<string, List<string>> _listVal = new();


        public bool SkipHashCheck { get => _boolVal[SKIP_HASH]; set => _boolVal[SKIP_HASH] = value; }
        public bool InteractiveInstallation { get => _boolVal[INTERACTIVE]; set => _boolVal[INTERACTIVE] = value; }
        public bool RunAsAdministrator { get => _boolVal[AS_ADMIN]; set => _boolVal[AS_ADMIN] = value; }
        public bool PreRelease { get => _boolVal[PRERELEASE]; set => _boolVal[PRERELEASE] = value; }
        public bool SkipMinorUpdates { get => _boolVal[SKIP_MINOR]; set => _boolVal[SKIP_MINOR] = value; }
        public bool RemoveDataOnUninstall { get => _boolVal[REMOVE_DATA_UNINST]; set => _boolVal[REMOVE_DATA_UNINST] = value; }
        public bool UninstallPreviousVersionsOnUpdate { get => _boolVal[CLEAR_PREV_VER]; set => _boolVal[CLEAR_PREV_VER] = value; }
        public bool AbortOnPreInstallFail { get => _boolVal[ABORT_PRE_INST_FAIL]; set => _boolVal[ABORT_PRE_INST_FAIL] = value; }
        public bool AbortOnPreUpdateFail { get => _boolVal[ABORT_PRE_UPD_FAIL]; set => _boolVal[ABORT_PRE_UPD_FAIL] = value; }
        public bool AbortOnPreUninstallFail { get => _boolVal[ABORT_PRE_UNINST_FAIL]; set => _boolVal[ABORT_PRE_UNINST_FAIL] = value; }

        public string Architecture { get => _strVal[ARCH]; set => _strVal[ARCH] = value; }
        public string InstallationScope { get => _strVal[SCOPE]; set => _strVal[SCOPE] = value; }
        public string CustomInstallLocation { get => _strVal[LOCATION]; set => _strVal[LOCATION] = value; }
        public string Version { get => _strVal[VERSION]; set => _strVal[VERSION] = value; }
        public string PreInstallCommand { get => _strVal[PRE_INST_CMD]; set => _strVal[PRE_INST_CMD] = value; }
        public string PostInstallCommand { get => _strVal[POST_INST_CMD]; set => _strVal[POST_INST_CMD] = value; }
        public string PreUpdateCommand { get => _strVal[PRE_UPD_CMD]; set => _strVal[PRE_UPD_CMD] = value; }
        public string PostUpdateCommand { get => _strVal[POST_UPD_CMD]; set => _strVal[POST_UPD_CMD] = value; }
        public string PreUninstallCommand { get => _strVal[PRE_UNINST_CMD]; set => _strVal[PRE_UNINST_CMD] = value; }
        public string PostUninstallCommand { get => _strVal[POST_UNINST_CMD]; set => _strVal[POST_UNINST_CMD] = value; }

        public List<string> CustomParameters_Install { get => _listVal[INST_PARAMS]; set => _listVal[INST_PARAMS] = value; }
        public List<string> CustomParameters_Update { get => _listVal[UPD_PARAMS]; set => _listVal[UPD_PARAMS] = value; }
        public List<string> CustomParameters_Uninstall { get => _listVal[UNINST_PARAMS]; set => _listVal[UNINST_PARAMS] = value; }
        public List<string> KillBeforeOperation { get => _listVal[KILL_BEFORE_OP]; set => _listVal[KILL_BEFORE_OP] = value; }

        public bool OverridesNextLevelOpts { get; set; }

        public override InstallOptions Copy()
        {
            var copy = new InstallOptions();

            foreach (var (boolKey, _) in _defaultBoolValues)
                copy._boolVal[boolKey] = this._boolVal[boolKey];

            foreach (var stringKey in _stringKeys)
                copy._strVal[stringKey] = this._strVal[stringKey];

            foreach (var listKey in _listKeys)
                copy._listVal[listKey] = this._listVal[listKey].ToList();

            // Handle non-automated OverridesNextLevelOpts
            copy.OverridesNextLevelOpts = OverridesNextLevelOpts;
            return copy;
        }

        public override void LoadFromJson(JsonNode data)
        {
            foreach (var (boolKey, defValue) in _defaultBoolValues)
                _boolVal[boolKey] = data[boolKey]?.GetVal<bool>() ?? defValue;

            foreach (var stringKey in _stringKeys)
                _strVal[stringKey] = data[stringKey]?.GetVal<string>() ?? "";

            foreach (var listKey in _listKeys)
                _listVal[listKey] = _readArrayFromJson(data, listKey);

            // Handle case where setting has not been migrated yet to have three different entries for CustomParameters
            if (this.CustomParameters_Install.Count is 0 &&
                this.CustomParameters_Update.Count  is 0 &&
                this.CustomParameters_Uninstall.Count is 0 &&
                ((data as JsonObject)?.ContainsKey("CustomParameters") ?? false))
            {
                this.CustomParameters_Install = _readArrayFromJson(data, "CustomParameters");
                this.CustomParameters_Update = _readArrayFromJson(data, "CustomParameters");
                this.CustomParameters_Uninstall = _readArrayFromJson(data, "CustomParameters");
            }

            // if OverridesNextLevelOpts is not found on the JSON, set it to true or false depending
            // on whether the current settings instances are different from the default values.
            // This entry shall be checked the last one, to ensure all other properties are set
            this.OverridesNextLevelOpts = data[nameof(OverridesNextLevelOpts)]?.GetValue<bool>() ?? DiffersFromDefault();
        }

        public override JsonObject AsJsonNode()
        {
            JsonObject obj = new();

            // OverridesNextLevelOpts is not among the automated properties, must be dealt manually
            if (OverridesNextLevelOpts is true || this.DiffersFromDefault())
                obj.Add(nameof(OverridesNextLevelOpts), OverridesNextLevelOpts);

            foreach (var (boolKey, defValue) in _defaultBoolValues)
            {
                bool currentValue = _boolVal[boolKey];
                if (currentValue != defValue) obj.Add(boolKey, currentValue);
            }

            foreach (var stringKey in _stringKeys)
            {
                string currentValue = _strVal[stringKey];
                if (currentValue.Any()) obj.Add(stringKey, currentValue);
            }

            foreach (var listKey in _listKeys)
            {
                var currentValue = _listVal[listKey];
                if (currentValue.Where(x => x.Any()).Any())
                {
                    obj.Add(listKey, new JsonArray(currentValue.Select(x => JsonValue.Create(x) as JsonNode).ToArray()));
                }
            }

            return obj;
        }

        private static List<string> _readArrayFromJson(JsonNode data, string name)
        {
            List<string> result = new List<string>();
            foreach (var element in data[name]?.AsArray2() ?? [])
                if (element is not null)
                    result.Add(element.GetVal<string>());
            return result.Where(x => x.Any()).ToList();
        }

        public bool DiffersFromDefault()
        {
            foreach (var (boolKey, defValue) in _defaultBoolValues)
                if (_boolVal[boolKey] != defValue) return true;

            foreach (var stringKey in _stringKeys)
                if (_strVal[stringKey].Any()) return true;

            foreach (var listKey in _listKeys)
                if (_listVal[listKey].Where(x => x.Any()).Any()) return true;

            return false;
            // OverridesNextLevelOpts does not need to be checked here, since
            // this method is invoked before this property has been set
        }

        public InstallOptions() : base()
        {
            // Initialize default values, ensure keys exist
            foreach (var (boolKey, defValue) in _defaultBoolValues)
                _boolVal[boolKey] = defValue;

            foreach (var stringKey in _stringKeys)
                _strVal[stringKey] = "";

            foreach (var listKey in _listKeys)
                _listVal[listKey] = new();
        }

        public InstallOptions(JsonNode data)
        {
            // No need to ensure keys exist, LoadFromJson will do that
            LoadFromJson(data);
        }

        public override string ToString()
        {
            StringBuilder b = new("<InstallOptions instance (only non-default values are shown)");
            foreach (var (boolKey, defValue) in _defaultBoolValues)
            {
                bool currentValue = _boolVal[boolKey];
                if (currentValue != defValue) b.Append($"\n\t{boolKey}: {currentValue}");
            }

            foreach (var stringKey in _stringKeys)
            {
                string currentValue = _strVal[stringKey];
                if (currentValue.Any()) b.Append($"\n\t{stringKey}: \"{currentValue}\"");
            }

            foreach (var listKey in _listKeys)
            {
                var currentValue = _listVal[listKey];
                if (currentValue.Where(x => x.Any()).Any())
                {
                    b.Append($"\n\t{listKey}: [{string.Join(", ", currentValue)}]");
                }
            }

            b.Append($"\n\tOverridesNextLevelOpts: {OverridesNextLevelOpts}>");
            return b.ToString();
        }
    }
}
