using Common.Logging;
using Godot;
using System;
using System.Collections.Generic;

namespace Common.Input.Mapping {
    public class InputActionMap : IInputActionMap {
        private static readonly Logger logger = new (typeof (InputActionMap));

        private readonly Dictionary<string, InputActions> mappedActions = [];

        public void Add (string action, Action<Variant> pressedAction, Action<Variant> releasedAction) {
            logger.Debug ("Mapping action '{0}' to {1}: {2}, {3}", action, ((Node) pressedAction.Target).Name, pressedAction?.Method.Name, releasedAction?.Method.Name);
            mappedActions.Add (action, new (pressedAction, releasedAction));
        }

        public void Process (InputEvent inputEvent, Variant arg) {
            foreach (KeyValuePair<string, InputActions> mappedAction in mappedActions) {
                if (inputEvent.IsActionPressed (mappedAction.Key)) {
                    mappedAction.Value.Pressed?.Invoke (arg.VariantType != Variant.Type.Nil ? arg : inputEvent.GetActionStrength (mappedAction.Key));
                } else if (inputEvent.IsActionReleased (mappedAction.Key)) {
                    mappedAction.Value.Released?.Invoke (arg.VariantType != Variant.Type.Nil ? arg : inputEvent.GetActionStrength (mappedAction.Key));
                }
            }
        }
    }
}
