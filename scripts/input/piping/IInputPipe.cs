using Godot;

namespace Common.Input.Piping {
    public interface IInputPipe {
        void Pipe (InputEvent inputEvent);
    }
}
