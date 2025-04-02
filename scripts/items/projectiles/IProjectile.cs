using Godot;

namespace Common.Items.Projectiles {
    public interface IProjectile {
        void Hit (Node target);
        void SetActive (bool active);
    }
}
