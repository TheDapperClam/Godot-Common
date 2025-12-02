using Common.Logging;
using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Jumpable3D : Jumpable {
        private static readonly Logger logger = new (typeof (Jumpable3D));

        private CharacterBody3D parentBody;

        public override void _Ready () {
            base._Ready ();

            if ((parentBody = GetParent () as CharacterBody3D) == null) {
                logger.Error ("Parent node {0} is not a CharacterBody3D", GetParent ().Name);
            }
        }

        public override void Jump () {
            if (parentBody != null && parentBody.IsOnFloor ()) {
                Vector3 velocity = parentBody.GetRealVelocity ();
                velocity.Y = force;
                parentBody.Velocity = velocity;
            }
        }
    }
}
