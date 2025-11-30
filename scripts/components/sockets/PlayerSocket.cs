using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class PlayerSocket : Socket {
        public override void _UnhandledInput (InputEvent @event) {
            base._Input (@event);
            Pipe (@event);
        }
    }
}
