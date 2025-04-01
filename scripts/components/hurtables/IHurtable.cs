using Godot;

namespace Common.Components.Hurtables {
    public interface IHurtable {
        void Hurt (float amount, Node source);
    }
}
