using Common.Input.Events;
using Common.Input.Translation;
using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class PlayerSocket : Socket {
        private readonly IInputEventTranslator[] translators = {
            // TODO: Make this action configurable
            new MouseActionTranslator("mouse_axis")
        };

        public override void _Input (InputEvent @event) {
            base._Input (@event);

            Variant arg = default;
            InputEvent inputEvent = @event;

            foreach (IInputEventTranslator translator in translators) {
                inputEvent = translator.Translate (inputEvent);
            }
            if (inputEvent is InputEventVariantAction variantAction) {
                arg = variantAction.Variant;
            }
            Pipe (inputEvent, arg);
        }
    }
}
