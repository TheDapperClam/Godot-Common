using System;

namespace Common.Input.Piping {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PippedAction : Attribute {
        public string Action { get; private set; }
        public bool Pressed { get; private set; }

        public PippedAction (string action, bool pressed) {
            Action = action;
            Pressed = pressed;
        }
    }
}
