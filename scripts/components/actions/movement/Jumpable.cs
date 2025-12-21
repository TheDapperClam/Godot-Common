using Common.Input.Mapping;
using Common.Input.Piping;
using Godot;

namespace Common.Components.Actions.Movement {
    public abstract partial class Jumpable : Node, IInputPipe, IJumpable {
        private readonly InputActionMap inputActionMap = new ();

        [Export]
        protected float force = 10.0f;

        [Export]
        private string jumpActionName = "jump";


        public abstract void Jump ();

        public override void _Ready () {
            base._Ready ();

            inputActionMap.Add (jumpActionName, JumpPressed, null);
        }

        public void Pipe (InputEvent inputEvent) {
            inputActionMap.Process (inputEvent);
        }

        private void JumpPressed (float strength) {
            Jump ();
        }
    }
}
