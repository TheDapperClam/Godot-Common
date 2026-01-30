using Common.Input.Mapping;
using Common.Input.Piping;
using Godot;

namespace Common.Components.Actions.Movement {
    public abstract partial class Moveable : Node, IInputPipe, IMoveable {
        private readonly InputActionMap inputActionMap = new ();

        [Export]
        protected Curve accelerationCurve;
        [Export]
        protected Curve decelerationCurve;
        [Export]
        protected float moveSpeed = 3.0f;
        [Export]
        private string moveActionDown = "move_down";
        [Export]
        private string moveActionLeft = "move_left";
        [Export]
        private string moveActionRight = "move_right";
        [Export]
        private string moveActionUp= "move_up";

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

            inputActionMap.Add (moveActionDown, MoveDownPressed, MoveDownReleased);
            inputActionMap.Add (moveActionLeft, MoveLeftPressed, MoveLeftReleased);
            inputActionMap.Add (moveActionRight, MoveRightPressed, MoveRightReleased);
            inputActionMap.Add (moveActionUp, MoveUpPressed, MoveUpReleased);
        }

        public void Pipe (InputEvent inputEvent) {
            inputActionMap.Process (inputEvent);

            float horizontal = movingRight - movingLeft;
            float vertical = movingDown - movingUp;
            moveDirection = new Vector2 (horizontal, vertical);
        }

        private void MoveDownPressed (float strength) {
            movingDown = strength;
        }

        private void MoveLeftPressed (float strength) {
            movingLeft = strength;
        }

        private void MoveRightPressed (float strength) {
            movingRight = strength;
        }

        private void MoveUpPressed (float strength) {
            movingUp = strength;
        }

        private void MoveDownReleased (float strength) {
            movingDown = 0.0f;
        }

        private void MoveLeftReleased (float strength) {
            movingLeft = 0.0f;
        }

        private void MoveRightReleased (float strength) {
            movingRight = 0.0f;
        }

        private void MoveUpReleased (float strength) {
            movingUp = 0.0f;
        }
    }
}
