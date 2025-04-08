using Common.Components.Hurtables;
using Common.Utils.Nodes;
using Godot;

namespace Common.Items.Projectiles {
	[GlobalClass]
	public partial class HitscanProjectile : RayCast3D, IProjectile {
		private const float TARGET_POSITION_OFFSET = 0.1f;

		[Export]
		private float damage = 1f;
		[Export]
		private float range = 1000.0f;

		public void Hit (Node target) {
			IHurtable hurtable = NodeUtils.GetChildOfType<IHurtable> (target);
			hurtable?.Hurt (damage, this);
		}

		public void Initiate (ProjectileFireProperties properties) {
			Vector3 vecToTarget = properties.TargetPosition - properties.MuzzlePosition;
			GlobalPosition = properties.MuzzlePosition;
			TargetPosition = vecToTarget.Normalized () * Mathf.Min (vecToTarget.Length (), range);
			TargetPosition += TargetPosition.Normalized () * TARGET_POSITION_OFFSET;
		}

		public void SetActive (bool active) {
			Enabled = active;
			Visible = active;

			if (Enabled) {
				ForceRaycastUpdate ();
				if (IsColliding () && GetCollider () is Node node) {
					Hit (node);
				}
				QueueFree ();
			}
		}
	}
}
