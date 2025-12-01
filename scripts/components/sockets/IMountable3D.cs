using Godot;

namespace Common.Components.Sockets {
    public interface IMountable3D {
        Vector3 GetMountPoint ();
        Vector3 GetMountRotation ();
    }
}
