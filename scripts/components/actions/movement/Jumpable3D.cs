using Common.Logging;
using Common.Utils.Nodes;
using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Jumpable3D : Jumpable {
        private static readonly Logger logger = new (typeof (Jumpable3D));

        [Export]
        public Curve GravityCurve { get; set; }

        private bool doJump;
        private bool jumping;
        private bool grounded;
        private CharacterBody3D parentBody;
        private CharacterPhysics3D characterPhysics;

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            if (parentBody != null) {
                Vector3 velocity = parentBody.GetRealVelocity ();
                grounded = parentBody.IsOnFloor ();

                if (grounded) {
                    jumping = false;
                } else if (jumping && characterPhysics != null) {
                    characterPhysics.GravityScale = GravityCurve.Sample (velocity.Y);
                }
            }
            if (doJump) {
                Vector3 velocity = parentBody.GetRealVelocity ();
                velocity.Y = force;
                parentBody.Velocity = velocity;
                doJump = false;
                jumping = true;
            }
        }

        public override void _Ready () {
            base._Ready ();

            if ((parentBody = GetParent () as CharacterBody3D) == null) {
                logger.Error ("Parent node {0} is not a CharacterBody3D", GetParent ().Name);
            }

            characterPhysics = NodeUtils.GetChildOfType<CharacterPhysics3D> (parentBody);
        }

        public override void Jump () {
            if (grounded && !jumping) {
                doJump = true;
            }
        }
    }
}
