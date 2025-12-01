using Godot;

namespace Common.Managers.Settings {
	public static class SettingsManager {
		private const string ROOT_PATH = "godot_common/settings/";

		public static Variant GetSetting (string name, Variant defaultValue) {
			string path = BuildPath (name);

			if (!ProjectSettings.HasSetting (path)) {
				GD.PushWarning (string.Format ("Setting {0} does not exist, creating with default value: {1}", path, defaultValue.AsString ()));
				ProjectSettings.SetSetting (path, defaultValue);
				ProjectSettings.Save ();
				return defaultValue;
			}
			return ProjectSettings.GetSettingWithOverride (path);
		}

		private static string BuildPath (string name) {
			return ROOT_PATH + name;
		}
	}
}
