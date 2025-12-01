using Common.Input.Mapping;
using Common.Input.Piping;
using Common.Logging;
using Common.Utils.Vectors;
using Godot;

namespace Common.Components.Actions.Rotation {
    [GlobalClass]
    public partial class Rotateable3D : Node, IInputPipe, IRotateable3D {
        private static readonly Logger logger = new (typeof (Rotateable3D));

        private readonly IInputActionMap inputActionMap = new InputActionMap ();

        [Export]
        private string rotateActionName = "mouse_axis";

        private Vector3 rotateAmount;

        public override void _Ready () {
            base._Ready ();

            inputActionMap.Add (rotateActionName, RotatePressed, null);
        }

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

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

        private void RotatePressed (Variant arg) {
            Vector2 axis = arg.AsVector2 ();
            rotateAmount = VectorUtils.Vector2XYToVector3XY (axis);
        }
    }
}
