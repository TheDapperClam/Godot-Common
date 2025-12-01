using Common.Input.Events;
using Godot;

namespace Common.Input.Translation {
    public class MouseActionTranslator : IInputEventTranslator {
        private readonly StringName actionName;

        public MouseActionTranslator (string actionName) {
            this.actionName = actionName;
        }

        public InputEvent Translate (InputEvent inputEvent) {
            if (inputEvent is InputEventMouseMotion mouseMotion) {
                return new InputEventVariantAction () {
                    Action = actionName,
                    Pressed = true,
                    Variant = mouseMotion.Relative
                };
            }
            return inputEvent;
        }
    }
}
