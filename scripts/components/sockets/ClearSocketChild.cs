using Common.Input.Mapping;
using Common.Input.Piping;
using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class ClearSocketChild : Node, IInputPipe {
        private readonly InputActionMap inputActionMap = new ();

        [Export]
        private Node target;
        [Export]
        private string clearActionName = "use";

        public override void _Ready () {
            base._Ready ();

            inputActionMap.Add (clearActionName, ClearPressed, null);
        }

        public void Pipe (InputEvent inputEvent) {
            inputActionMap.Process (inputEvent);
        }

        private void ClearPressed (float strength) {
            if (target is ISocket socket) {
                socket.Disconnect ();
            }
        }
    }
}
