using Common.Managers.Settings;
using Godot;
using System;

namespace Common.Logging {
    public class Logger {
        private const string COLOR_DEBUG = "magenta";
        private const string COLOR_ERROR = "orangered";
        private const string COLOR_INFO = "silver";
        private const string COLOR_WARN = "gold";
        private const string MESSAGE_DEBUG_FORMAT = "[{0}] DEBUG: {1}";
        private const string MESSAGE_ERROR_FORMAT = "[{0}] ERROR: {1}";
        private const string MESSAGE_INFO_FORMAT = "[{0}] INFO: {1}";
        private const string MESSAGE_WARN_FORMAT = "[{0}] WARNING: {1}";
        private const string SETTING_DEBUG = "debug/settings/logging/debug_enabled";

        public readonly bool IsDebugEnabled;

        private readonly Type loggerType;

        public Logger (Type loggerType) {
            IsDebugEnabled = SettingsManager.GetSetting (SETTING_DEBUG, false).AsBool ();
            this.loggerType = loggerType ?? throw new ArgumentException ("Logger must be assigned a type");
        }

        public void Debug (string message, params object[] args) {
            if (IsDebugEnabled) {
                string log = BuildMessage (MESSAGE_DEBUG_FORMAT, message, COLOR_DEBUG, args);
                GD.PrintRich (log);
            }
        }

        public void Error (string message, params object[] args) {
            string log = BuildMessage (MESSAGE_ERROR_FORMAT, message, COLOR_ERROR, args);
            GD.PrintRich (log);
        }

        public void Info (string message, params object[] args) {
            string log = BuildMessage (MESSAGE_INFO_FORMAT, message, COLOR_INFO, args);
            GD.PrintRich (log);
        }

        public void Warn (string message, params object[] args) {
            string log = BuildMessage (MESSAGE_WARN_FORMAT, message, COLOR_WARN, args);
            GD.PrintRich (log);
        }

        private string BuildMessage (string template, string message, string color, params object[] args) {
            message ??= string.Empty;
            string typeString = loggerType.Namespace + "." + loggerType.Name;
            return string.Format("[color={0}]{1}[/color]", color, string.Format (template, typeString, string.Format (message, args)));
        }
    }
}
