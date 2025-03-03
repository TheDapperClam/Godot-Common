using Godot;

namespace Common.Managers.Spawning {
    [GlobalClass]
    public partial class SpawnPoint3D : Marker3D, ISpawnPoint {
        public override void _EnterTree () {
            base._EnterTree ();

            ISpawnPoint.SpawnPoints.Add (this);
        }

        public override void _ExitTree () {
            base._ExitTree ();

            ISpawnPoint.SpawnPoints.Add (this);
        }

        public Vector3 GetSpawnPosition () {
            return GlobalPosition;
        }
    }
}
