using Godot;

namespace Common.Characters {
    public abstract partial class Character3D : CharacterBody3D, ICharacter {
        [Export]
        public Node3D ViewPoint { get; set; }

        public Vector3 GetViewPosition () {
            return ViewPoint != null ? ViewPoint.GlobalPosition : Vector3.Zero;
        }
    }
}
