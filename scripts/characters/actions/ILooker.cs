using Godot;

namespace Common.Characters.Actions {
    public interface ILooker {
        void Look (Vector3 direction, Vector3 up);
    }
}
