using Common.Components.Actions.Movement;
using Godot;

namespace Common.Characters {
    public abstract partial class Character3D : CharacterBody3D, ICharacter, ILooker {
        [Export]
        public Node3D ViewPoint { get; set; }

        public Vector3 GetViewPosition () {
            return ViewPoint != null ? ViewPoint.GlobalPosition : Vector3.Zero;
        }

        public void Look (Vector3 direction, Vector3 up) {
            Vector3 flatDirection = new Plane (Vector3.Up).Project (direction);
            LookAt (GlobalPosition + flatDirection, up);
            ViewPoint.LookAt (ViewPoint.GlobalPosition + direction, up);
        }
    }
}
