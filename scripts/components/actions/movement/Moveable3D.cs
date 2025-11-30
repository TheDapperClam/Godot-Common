using Common.Logging;
using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Moveable3D : Moveable {
        private static readonly Logger logger = new (typeof (Moveable3D));

        public override void Move (Vector2 direction, double delta) {
            if (GetParent () is CharacterBody3D body3d) {
                Vector2 relativeDirection = direction.Rotated (-body3d.GlobalRotation.Y);
                Vector3 velocity = body3d.GetRealVelocity ();
                velocity.X = relativeDirection.X * moveSpeed * (float) delta;
                velocity.Z = relativeDirection.Y * moveSpeed * (float) delta;
                velocity.Y -= gravity * GravityScale * (float) delta;
                body3d.Velocity = velocity;
                body3d.MoveAndSlide ();
            } else {
                logger.Error ("Parent node {0} is not a CharacterBody3D", GetParent ().Name);
            }
        }
    }
}
