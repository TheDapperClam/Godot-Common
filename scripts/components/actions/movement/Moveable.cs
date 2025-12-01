using Common.Input.Events;
using Common.Input.Mapping;
using Common.Input.Piping;
using Common.Managers.Settings;
using Godot;

namespace Common.Components.Actions.Movement {
    public abstract partial class Moveable : Node, IInputPipe, IMoveable {
        private const string GRAVITY_SETTING = "physics/3d/default_gravity";

        private readonly InputActionMap inputActionMap = new ();

        [Export]
        public float GravityScale { get; set; } = 1.0f;

        [Export]
        protected float moveSpeed = 200.0f;
        [Export]
        private string moveActionDown = "move_down";
        [Export]
        private string moveActionLeft = "move_left";
        [Export]
        private string moveActionRight = "move_right";
        [Export]
        private string moveActionUp= "move_up";

        protected float gravity;

        private bool movingDown;
        private bool movingLeft;
        private bool movingRight;
        private bool movingUp;

        public abstract void Move (Vector2 direction, double delta);

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            float horizontal = (movingLeft ? -1f : 0f) + (movingRight ? 1f : 0f);
            float vertical = (movingUp ? -1f : 0f) + (movingDown ? 1f : 0f);
            Move (new Vector2 (horizontal, vertical).Normalized (), delta);
        }

        public override void _Ready () {
            base._Ready ();

            gravity = (float) SettingsManager.GetSetting (GRAVITY_SETTING, 9.8f).AsDouble ();

            inputActionMap.Add (moveActionDown, MoveDownPressed, MoveDownReleased);
            inputActionMap.Add (moveActionLeft, MoveLeftPressed, MoveLeftReleased);
            inputActionMap.Add (moveActionRight, MoveRightPressed, MoveRightReleased);
            inputActionMap.Add (moveActionUp, MoveUpPressed, MoveUpReleased);
        }

        public void Pipe (InputEvent inputEvent, Variant arg) {
            inputActionMap.Process (inputEvent, arg);
        }

        private void MoveDownPressed (Variant arg) {
            movingDown = true;
        }

        private void MoveLeftPressed (Variant arg) {
            movingLeft = true;
        }

        private void MoveRightPressed (Variant arg) {
            movingRight = true;
        }

        private void MoveUpPressed (Variant arg) {
            movingUp = true;
        }

        private void MoveDownReleased (Variant arg) {
            movingDown = false;
        }

        private void MoveLeftReleased (Variant arg) {
            movingLeft = false;
        }

        private void MoveRightReleased (Variant arg) {
            movingRight = false;
        }

        private void MoveUpReleased (Variant arg) {
            movingUp = false;
        }
    }
}
