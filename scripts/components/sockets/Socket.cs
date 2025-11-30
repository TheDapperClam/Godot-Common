using Common.Input.Piping;
using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class Socket : Node, IActionPipe, ISocket {
        [Signal]
        public delegate void InputEventHandler (InputEvent inputEvent);

        [Export]
        private Node child;

        public override void _Ready () {
            base._Ready ();

            if (child != null) {
                Connect (child);
            }
        }

        public void Connect (Node child) {
            child.Connect (Node.SignalName.TreeExiting, Callable.From (Disconnect));
            this.child = child;
        }

        public void Disconnect () {
            child.Disconnect (Node.SignalName.TreeExiting, Callable.From (Disconnect));
            child = null;
        }

        public bool IsConnected () {
            return child != null;
        }

        public void Pipe (InputEvent inputEvent) {
            if (child is IActionPipe pipe) {
                pipe.Pipe (inputEvent);
                return;
            }
            EmitSignal (SignalName.Input, inputEvent);
        }
    }
}
