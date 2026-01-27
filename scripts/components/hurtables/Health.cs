using Godot;

namespace Common.Components.Hurtables {
    [GlobalClass]
    public partial class Health : Node, IHurtable {
        private static readonly Logging.Logger logger = new (typeof (Health));

        [Signal]
        public delegate void DamagedEventHandler ();
        [Signal]
        public delegate void DeathEventHandler ();

        [Export]
        public float MaxHealth { get; set; } = 1f;
        [Export]
        public float CurrentHealth { get; set; } = 1f;
        public bool IsAlive { get; private set; } = true;

        public void Hurt (float amount, Node source) {
            if (!IsAlive) {
                return;
            }

            CurrentHealth = Mathf.Clamp (CurrentHealth - amount, 0f, float.MaxValue);
            if (amount > 0) {
                EmitSignal (SignalName.Damaged);
                logger.Debug ("{0} damaged {1} for {2}, {3}/{4} remaining", source.Name, GetParent ().Name, amount, MaxHealth, CurrentHealth);
            }
            if (CurrentHealth <= 0) {
                IsAlive = false;
                EmitSignal (SignalName.Death);
                logger.Debug ("{0} killed {1}", source.Name, GetParent().Name);
            }
        }
    }
}
