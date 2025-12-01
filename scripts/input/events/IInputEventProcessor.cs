using Godot;

namespace Common.Input.Events {
    public interface IInputEventProcessor {
        bool Process (InputEvent inputEvent);
    }
}
