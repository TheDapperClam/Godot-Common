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

        private Vector2 moveDirection;
        private float movingDown;
        private float movingLeft;
        private float movingRight;
        private float movingUp;

        public abstract void Move (Vector2 direction, double delta);

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            Move (moveDirection, delta);
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

            float horizontal = movingRight - movingLeft;
            float vertical = movingDown - movingUp;
            moveDirection = new Vector2 (horizontal, vertical);
        }

        private void MoveDownPressed (Variant arg) {
            movingDown = (float) arg.AsDouble ();
        }

        private void MoveLeftPressed (Variant arg) {
            movingLeft = (float) arg.AsDouble ();
        }

        private void MoveRightPressed (Variant arg) {
            movingRight = (float) arg.AsDouble ();
        }

        private void MoveUpPressed (Variant arg) {
            movingUp = (float) arg.AsDouble ();
        }

        private void MoveDownReleased (Variant arg) {
            movingDown = 0.0f;
        }

        private void MoveLeftReleased (Variant arg) {
            movingLeft = 0.0f;
        }

        private void MoveRightReleased (Variant arg) {
            movingRight = 0.0f;
        }

        private void MoveUpReleased (Variant arg) {
            movingUp = 0.0f;
        }
    }
}
