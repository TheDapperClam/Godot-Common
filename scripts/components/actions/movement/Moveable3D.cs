using Common.Logging;
using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Moveable3D : Moveable {
        private static readonly Logger logger = new (typeof (Moveable3D));

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
                Vector2 relativeDirection = direction.Rotated (directionTarget != null ? -directionTarget.GlobalRotation.Y : parentBody.GlobalRotation.Y);
                Vector3 velocity = parentBody.GetRealVelocity ();
                velocity.X = relativeDirection.X * moveSpeed * (float) delta;
                velocity.Z = relativeDirection.Y * moveSpeed * (float) delta;
                parentBody.Velocity = velocity;
            }
        }
    }
}
