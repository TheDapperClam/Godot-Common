using Common.Input.Piping;
using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class Socket : Node, IInputPipe, ISocket {
        [Signal]
        public delegate void InputEventHandler (InputEventAction inputEvent);
        [Signal]
        public delegate void ChildSocketConnectedEventHandler ();
        [Signal]
        public delegate void ChildSocketDisconnectedEventHandler ();

        [Export]
        private Node child;
        [Export]
        private bool childBlockInput = true;

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
            EmitSignal (SignalName.ChildSocketConnected);
        }

        public void Disconnect () {
            child.Disconnect (Node.SignalName.TreeExiting, Callable.From (Disconnect));
            child = null;
            EmitSignal (SignalName.ChildSocketDisconnected);
        }

        public bool IsConnected () {
            return child != null;
        }

        public void Pipe (InputEvent inputEvent) {
            if (child is IInputPipe pipe) {
                pipe.Pipe (inputEvent);
                if (childBlockInput) {
                    return;
                }
            }
            EmitSignal (SignalName.Input, inputEvent);
        }
    }
}
