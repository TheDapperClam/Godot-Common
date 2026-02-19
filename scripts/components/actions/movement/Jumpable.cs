using Godot;

namespace Common.Components.Actions.Movement {
    public abstract partial class Jumpable : ActionBase, IJumpable {
        [Export]
        protected float force = 10.0f;

        [Export]
        private string jumpActionName = "jump";


        public abstract void Jump ();

        public override void _Ready () {
            base._Ready ();

            inputActionMap.Add (jumpActionName, JumpPressed, null);
        }

        private void JumpPressed (float strength) {
            Jump ();
        }
    }
}
