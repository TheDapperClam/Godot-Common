using Godot;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class MountableSocket3D : Socket, IMountable3D {
        [Export]
        private Node3D mountPoint;
        [Export]
        private Vector3 positionOffset;

        public Vector3 GetMountPoint () {
            return mountPoint.GlobalPosition +
                mountPoint.GlobalTransform.Basis.X * positionOffset.X +
                mountPoint.GlobalTransform.Basis.Y * positionOffset.Y +
                mountPoint.GlobalTransform.Basis.Z * positionOffset.Z;
        }

        public Vector3 GetMountRotation () {
            return mountPoint.GlobalRotation;
        }
    }
}
