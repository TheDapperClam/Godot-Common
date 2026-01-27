using Godot;
using System;
using System.Collections.Generic;

namespace Common.Input.Mapping {
    public class InputActionMap : IInputActionMap {
        private static readonly Logging.Logger logger = new (typeof (InputActionMap));

        private readonly Dictionary<string, InputActions> mappedActions = [];

        public void Add (string action, Action<float> pressedAction, Action<float> releasedAction) {
            if (string.IsNullOrEmpty (action)) {
                logger.Debug ("Empty action, skipping");
                return;
            }
            logger.Debug ("Mapping action '{0}' to {1}: {2}, {3}", action, ((Node) pressedAction.Target).Name, pressedAction?.Method.Name, releasedAction?.Method.Name);
            mappedActions.Add (action, new (pressedAction, releasedAction));
        }

        public void Process (InputEvent inputEvent) {
            foreach (KeyValuePair<string, InputActions> mappedAction in mappedActions) {
                if (inputEvent.IsActionPressed (mappedAction.Key)) {
                    mappedAction.Value.Pressed?.Invoke (inputEvent.GetActionStrength(mappedAction.Key));
                } else if (inputEvent.IsActionReleased (mappedAction.Key)) {
                    mappedAction.Value.Released?.Invoke (inputEvent.GetActionStrength (mappedAction.Key));
                }
            }
        }
    }
}
