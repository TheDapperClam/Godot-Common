using Common.Components.Hurtables;
using Common.Utils.Nodes;
using Godot;

namespace Common.Items.Projectiles {
	public partial class HitscanProjectile : RayCast3D {
		[Export]
		private float damage = 1f;

		public override void _Ready () {
			base._Ready ();

			ForceRaycastUpdate ();
			if (IsColliding () && GetCollider () is Node node) {
				IHurtable hurtable = NodeUtils.GetChildOfType<IHurtable> (node);
				hurtable?.Hurt (damage, this);
			}
			QueueFree ();
		}
	}
}
