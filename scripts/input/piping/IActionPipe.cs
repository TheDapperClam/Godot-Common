using Godot;

namespace Common.Input.Piping {
    public interface IActionPipe {
        void Pipe (InputEvent inputEvent);
    }
}
