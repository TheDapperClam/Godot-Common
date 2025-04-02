using Common.Items.Projectiles;
using Common.Logging;
using Godot;

namespace Common.Items.Fireables.Weapons.Guns {
	[GlobalClass]
	public partial class Gun3D : Node3D, IHoldFireable {
		private static readonly Logger logger = new Logger (typeof (Gun3D));

		[Export]
		private Node3D muzzle;
		[Export]
		private Node3D projectilePrefab;
		[Export]
		private int projectilesToFire = 1;
		[Export]
		private uint fireIntervalMsec = 100;

		private ulong nextFireTime;
		private int projectilesFired;

		public void Fire () {
			if (projectilesToFire >= 0 && projectilesFired >= projectilesToFire) {
				return;
			}
			if (nextFireTime > Time.GetTicksMsec ()) {
				return;
			}
			nextFireTime = Time.GetTicksMsec () + fireIntervalMsec;
			projectilesFired++;
			SpawnProjectile (projectilePrefab, muzzle.GlobalPosition, muzzle.GlobalRotation);
		}

		public void FireReleased () {
			projectilesFired = 0;
		}

		private void SpawnProjectile (Node3D prefab, Vector3 position, Vector3 rotation) {
			if (prefab == null) {
				logger.Error ("Projectile for {0} hasn't been assigned", Name);
				return;
			}

			Node3D newInstance = prefab.Duplicate () as Node3D;
			newInstance.Position = position;
			newInstance.Rotation = rotation;
			GetTree ().CurrentScene.AddChild (newInstance);

			if (newInstance is IProjectile projectile) {
				projectile.SetActive (true);
			}
		}
	}
}
