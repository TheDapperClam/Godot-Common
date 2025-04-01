using Godot;

namespace Common.Items.Fireables.Weapons.Guns {
	[GlobalClass]
	public partial class Gun3D : Node3D, IHoldFireable {
		[Export]
		private Node3D muzzle;
		[Export]
		private PackedScene projectilePrefab;
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

		private void SpawnProjectile (PackedScene prefab, Vector3 position, Vector3 rotation) {
			Node3D projectile = prefab.Instantiate<Node3D> ();
			projectile.Position = position;
			projectile.Rotation = rotation;
			GetTree ().CurrentScene.AddChild (projectile);
		}
	}
}
