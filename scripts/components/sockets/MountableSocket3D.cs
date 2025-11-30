using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class MountableSocket3D : Socket, IMountable3D {
        [Export]
        private Node3D mountPoint;

        public Vector3 GetMountPoint () {
            return mountPoint.GlobalPosition;
        }
    }
}
