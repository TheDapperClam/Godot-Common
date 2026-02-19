using Common.Managers.Settings;
using Godot;

namespace Common.Components.Physics {
    [GlobalClass]
    public partial class CharacterPhysics3D : Node {
        private const string GRAVITY_SETTING = "physics/3d/default_gravity";

        private static readonly Logging.Logger logger = new (typeof (CharacterPhysics3D));

        [Export]
        public float GravityScale { get; set; } = 1.0f;

        private float gravity;
        private CharacterBody3D parentBody;

        public override void _Ready () {
            base._Ready ();

            gravity = (float) SettingsManager.GetSetting (GRAVITY_SETTING, 9.8f).AsDouble ();
            if ((parentBody = GetParent () as CharacterBody3D) == null) {
                logger.Error ("Parent node {0} is not a CharacterBody3D", GetParent ().Name);
            }
        }

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            if (parentBody != null) {
                Vector3 velocity = parentBody.Velocity;
                velocity.Y -= gravity * GravityScale * (float) delta;
                parentBody.Velocity = velocity;
                parentBody.MoveAndSlide ();
            }
        }
    }
}
