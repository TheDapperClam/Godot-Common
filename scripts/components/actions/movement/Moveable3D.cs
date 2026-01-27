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
                bool moving = direction.Length () > 0f;
                Vector2 relativeDirection = direction.Rotated (directionTarget != null ? -directionTarget.GlobalRotation.Y : parentBody.GlobalRotation.Y);
                Vector3 velocity = parentBody.GetRealVelocity ();

                if (moving) {
                    velocity.X += relativeDirection.X * acceleration * (float) delta;
                    velocity.Z += relativeDirection.Y * acceleration * (float) delta;
                }

                Vector3 horizontalVel = velocity.Slide (Vector3.Up);
                Vector3 horizontalDir = horizontalVel.Normalized ();

                if (!moving) {
                    horizontalVel -= horizontalDir * Mathf.Min (horizontalVel.Length (), deceleration * (float) delta);
                }
                parentBody.Velocity = horizontalDir * Mathf.Min (horizontalVel.Length (), moveSpeed) + Vector3.Up * velocity.Y;
            }
        }
    }
}
