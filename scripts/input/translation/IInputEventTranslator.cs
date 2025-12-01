using Godot;

namespace Common.Input.Translation {
    public interface IInputEventTranslator {
        InputEvent Translate (InputEvent inputEvent);
    }
}
