using Godot;

namespace Common.Items.Projectiles {
    public interface IProjectile {
        void Hit (Node target);
        void Initiate (ProjectileFireProperties properties);
        void SetActive (bool active);
    }
}
