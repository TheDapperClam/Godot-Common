using Godot;

namespace Common.Components.Actions.Movement {
    public interface IMoveable {
        void Move (Vector2 direction, double delta);
    }
}
