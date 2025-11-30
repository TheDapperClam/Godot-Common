using Godot;
using System;
using System.Collections.Generic;

namespace Common.Input.Mapping {
    public class InputActionMap : IInputActionMap {
        private readonly Dictionary<string, InputActions> mappedActions = [];

        public void Add (string action, Action pressedAction, Action releasedAction) {
            mappedActions.Add (action, new (pressedAction, releasedAction));
        }

        public void Process (InputEvent inputEvent) {
            foreach (KeyValuePair<string, InputActions> mappedAction in mappedActions) {
                if (inputEvent.IsActionPressed (mappedAction.Key)) {
                    mappedAction.Value.Pressed.Invoke ();
                } else if (inputEvent.IsActionReleased (mappedAction.Key)) {
                    mappedAction.Value.Released.Invoke ();
                }
            }
        }
    }
}
