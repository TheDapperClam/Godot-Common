using Common.Input.Mapping;
using Common.Input.Piping;
using Common.Managers.Settings;
using Common.Utils.Vectors;
using Godot;

namespace Common.Components.Actions.Rotation {
    [GlobalClass]
    public partial class Rotateable3D : Node, IInputPipe, IRotateable3D {
        private readonly IInputActionMap inputActionMap = new InputActionMap ();

        [Export]
        private string rotateActionDown = "look_down";
        [Export]
        private string rotateActionLeft = "look_left";
        [Export]
        private string rotateActionRight = "look_right";
        [Export]
        private string rotateActionUp = "look_up";
        [Export]
        private float rotateSpeed = 300.0f;

        private Vector3 rotateAmount;
        private float rotateDown;
        private float rotateLeft;
        private float rotateRight;
        private float rotateUp;

        public override void _Ready () {
            base._Ready ();

            inputActionMap.Add (rotateActionDown, RotateDownPressed, RotateDownReleased);
            inputActionMap.Add (rotateActionLeft, RotateLeftPressed, RotateLeftReleased);
            inputActionMap.Add (rotateActionRight, RotateRightPressed, RotateRightReleased);
            inputActionMap.Add (rotateActionUp, RotateUpPressed, RotateUpReleased);
        }

        public override void _Process (double delta) {
            base._Process (delta);

            float horizontal = rotateRight - rotateLeft;
            float vertical = rotateDown - rotateUp;

            if (horizontal != 0f || vertical != 0f) {
                Vector2 axis = new (horizontal, vertical);
                rotateAmount = VectorUtils.Vector2XYToVector3XY (axis) * rotateSpeed * (float) delta;
            }
            if (rotateAmount.Length () > 0f) {
                Rotate (rotateAmount * (float) delta);
                rotateAmount = Vector3.Zero;
            }
        }

        public void Pipe (InputEvent inputEvent) {
            inputActionMap.Process (inputEvent);
        }

        public void Rotate (Vector3 amount) {
            if (GetParent () is Node3D parent3D) {
                Vector3 clampedRotation = parent3D.Rotation;
                clampedRotation.Y -= amount.X;
                clampedRotation.X = Mathf.Clamp (clampedRotation.X - amount.Y, Mathf.DegToRad (-90f), Mathf.DegToRad (90f));
                parent3D.Rotation = clampedRotation;
            }
        }

        private void RotateDownPressed (float strength) {
            rotateDown = strength;
        }

        private void RotateDownReleased (float strength) {
            rotateDown = 0f;
        }

        private void RotateLeftPressed (float strength) {
            rotateLeft = strength;
        }

        private void RotateLeftReleased (float strength) {
            rotateLeft = 0f;
        }

        private void RotateRightPressed (float strength) {
            rotateRight = strength;
        }

        private void RotateRightReleased (float strength) {
            rotateRight = 0f;
        }

        private void RotateUpPressed (float strength) {
            rotateUp = strength;
        }

        private void RotateUpReleased (float strength) {
            rotateUp = 0f;
        }
    }
}
