using Common.Input.Events;
using Common.Input.Translation;
using Common.Managers.Settings;
using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class PlayerSocket : Socket {
        private readonly IInputEventTranslator[] translators = {
            new MouseActionTranslator("mouse_axis", SettingsManager.GetSetting("input/mouse_sensitivity", Vector2.One * 0.25f).AsVector2 ())
        };

        public override void _Input (InputEvent @event) {
            base._Input (@event);

            Variant arg = 0;
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
