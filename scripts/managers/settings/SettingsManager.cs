using Godot;

namespace Common.Managers.Settings {
	public static class SettingsManager {
		public static Variant GetSetting (string name, Variant defaultValue) {
			if (!ProjectSettings.HasSetting (name)) {
				GD.PushWarning (string.Format("Setting {0} does not exist, using default value: {1}", name, defaultValue.AsString ()));
				return defaultValue;
			}
			return ProjectSettings.GetSettingWithOverride (name);
		}
	}
}
