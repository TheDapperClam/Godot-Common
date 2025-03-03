using Godot;
using System.Collections.Generic;

namespace Common.Managers.Spawning {
    public interface ISpawnPoint {
        public static List<ISpawnPoint> SpawnPoints { get; private set; } = new List<ISpawnPoint> ();

        Vector3 GetSpawnPosition ();
    }
}
