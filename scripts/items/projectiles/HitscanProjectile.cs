using Common.Components.Hurtables;
using Common.Utils.Nodes;
using Godot;

namespace Common.Items.Projectiles {
	[GlobalClass]
	public partial class HitscanProjectile : RayCast3D, IProjectile {
		[Export]
		private float damage = 1f;

		public void Hit (Node target) {
			IHurtable hurtable = NodeUtils.GetChildOfType<IHurtable> (target);
			hurtable?.Hurt (damage, this);
		}

		public void SetActive (bool active) {
			Enabled = active;

			if (Enabled) {
				ForceRaycastUpdate ();
				if (IsColliding () && GetCollider () is Node node) {
					Hit (node);
				}
				QueueFree ();
			}
		}

		public override void _Ready () {
			base._Ready ();

			SetActive (false);
		}
	}
}
