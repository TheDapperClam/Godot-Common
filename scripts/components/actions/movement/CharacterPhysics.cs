using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class CharacterPhysics : Node {
        CharacterBody3D parentBody;

        public override void _Ready () {
            base._Ready ();

            parentBody = GetParent () as CharacterBody3D;
        }

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            parentBody?.MoveAndSlide ();
        }
    }
}
