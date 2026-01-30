using Common.Utils.Vectors;
using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Moveable3D : Moveable {
        private static readonly Logging.Logger logger = new (typeof (Moveable3D));

        [Export]
        private Node3D directionTarget;

        private CharacterBody3D parentBody;

        public override void _Ready () {
            base._Ready ();

            if ((parentBody = GetParent () as CharacterBody3D) == null) {
                logger.Error ("Parent node {0} is not a CharacterBody3D", GetParent ().Name);
            }
        }

        public override void Move (Vector2 direction, double delta) {
            if (parentBody != null) {
                Vector3 velocity = parentBody.GetRealVelocity ();
                Vector3 horizontalVel = velocity.Slide (Vector3.Up);
                Vector2 relativeMoveDirection = direction.Rotated (directionTarget != null ? -directionTarget.GlobalRotation.Y : parentBody.GlobalRotation.Y);
                float t = horizontalVel.Length () / moveSpeed;

                horizontalVel += VectorUtils.Vector2XYToVector3XZ (relativeMoveDirection) * accelerationCurve.Sample (t) * (float) delta;
                horizontalVel -= horizontalVel.Normalized () * Mathf.Min (horizontalVel.Length (), decelerationCurve.Sample (t) * (float) delta);
                if (directionTarget != null && momentumTransferCurve != null) {
                    Vector3 horizontalForwardDir = -directionTarget.GlobalBasis.Z.Slide (Vector3.Up);
                    horizontalVel = horizontalVel.MoveToward (horizontalForwardDir * Mathf.Sign (horizontalForwardDir.Dot (horizontalVel.Normalized ())) *
                        horizontalVel.Length (), momentumTransferCurve.Sample (t) * (float) delta);
                }
                parentBody.Velocity = horizontalVel.Normalized () * Mathf.Min (horizontalVel.Length (), moveSpeed) + velocity.Y * Vector3.Up;
            }
        }
    }
}
