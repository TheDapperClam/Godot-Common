using Common.Input.Events;
using Godot;

namespace Common.Input.Translation {
    public class MouseActionTranslator : IInputEventTranslator {
        private readonly StringName actionName;

        private Vector2 sensitivity;

        public MouseActionTranslator (string actionName, Vector2 sensitivity) {
            this.actionName = actionName;
            this.sensitivity = sensitivity;
        }

        public InputEvent Translate (InputEvent inputEvent) {
            if (inputEvent is InputEventMouseMotion mouseMotion) {
                return new InputEventVariantAction () {
                    Action = actionName,
                    Pressed = true,
                    Variant = mouseMotion.Relative * sensitivity
                };
            }
            return inputEvent;
        }
    }
}
