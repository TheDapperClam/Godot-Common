using Common.Input.Piping;
using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class Socket : Node, IInputPipe, ISocket {
        [Signal]
        public delegate void InputEventHandler (InputEventAction inputEvent);

        [Export]
        private Node child;

        public override void _Ready () {
            base._Ready ();

            if (child != null) {
                Connect (child);
            }
        }

        public override void _Process (double delta) {
            base._Process (delta);

            if (child is IMountable3D mountable3D && GetParent() is Node3D parent3D) {
                parent3D.GlobalPosition = mountable3D.GetMountPoint ();
                parent3D.GlobalRotation = mountable3D.GetMountRotation ();
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
            if (child is IInputPipe pipe) {
                pipe.Pipe (inputEvent);
                return;
            }
            EmitSignal (SignalName.Input, inputEvent);
        }
    }
}
