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
        private string rotateActionMouse = "mouse_axis";
        [Export]
        private string rotateActionDown = "look_down";
        [Export]
        private string rotateActionLeft = "look_left";
        [Export]
        private string rotateActionRight = "look_right";
        [Export]
        private string rotateActionUp = "look_up";

        private Vector3 rotateAmount;
        private float rotateDown;
        private float rotateLeft;
        private float rotateRight;
        private float rotateUp;
        private Vector2 sensitivityJoy;
        private Vector2 sensitivityMouse;

        public override void _Ready () {
            base._Ready ();

            sensitivityJoy = SettingsManager.GetSetting ("input/joy_sensitivity", Vector2.One * 3.0f).AsVector2 ();
            sensitivityMouse = SettingsManager.GetSetting ("input/mouse_sensitivity", Vector2.One * 0.25f).AsVector2 ();

            inputActionMap.Add (rotateActionMouse, MouseAction, null);
            inputActionMap.Add (rotateActionDown, RotateDownPressed, RotateDownReleased);
            inputActionMap.Add (rotateActionLeft, RotateLeftPressed, RotateLeftReleased);
            inputActionMap.Add (rotateActionRight, RotateRightPressed, RotateRightReleased);
            inputActionMap.Add (rotateActionUp, RotateUpPressed, RotateUpReleased);
        }

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            float horizontal = rotateRight - rotateLeft;
            float vertical = rotateDown - rotateUp;

            if (horizontal != 0f || vertical != 0f) {
                Vector2 axis = new (horizontal, vertical);
                rotateAmount = VectorUtils.Vector2XYToVector3XY (axis * sensitivityJoy);
            }
            if (rotateAmount.Length () > 0f) {
                Rotate (rotateAmount * (float) delta);
                rotateAmount = Vector3.Zero;
            }
        }

        public void Pipe (InputEvent inputEvent, Variant arg) {
            inputActionMap.Process (inputEvent, arg);
        }

        public void Rotate (Vector3 amount) {
            if (GetParent () is Node3D parent3D) {
                Vector3 clampedRotation = parent3D.Rotation;
                clampedRotation.Y -= amount.X;
                clampedRotation.X = Mathf.Clamp (clampedRotation.X - amount.Y, -90, 90);
                parent3D.Rotation = clampedRotation;
            }
        }

        private void MouseAction (Variant arg) {
            Vector2 axis = arg.AsVector2 ();
            rotateAmount = VectorUtils.Vector2XYToVector3XY (axis * sensitivityMouse);
        }

        private void RotateDownPressed (Variant arg) {
            rotateDown = (float) arg.AsDouble ();
        }

        private void RotateDownReleased (Variant arg) {
            rotateDown = 0f;
        }

        private void RotateLeftPressed (Variant arg) {
            rotateLeft = (float) arg.AsDouble ();
        }

        private void RotateLeftReleased (Variant arg) {
            rotateLeft = 0f;
        }

        private void RotateRightPressed (Variant arg) {
            rotateRight = (float) arg.AsDouble ();
        }

        private void RotateRightReleased (Variant arg) {
            rotateRight = 0f;
        }

        private void RotateUpPressed (Variant arg) {
            rotateUp = (float) arg.AsDouble ();
        }

        private void RotateUpReleased (Variant arg) {
            rotateUp = 0f;
        }
    }
}
