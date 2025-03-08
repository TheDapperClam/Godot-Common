using Godot;

namespace Common.Components.Actions.Movement {
    public interface ILooker {
        void Look (Vector3 direction, Vector3 up);
    }
}
