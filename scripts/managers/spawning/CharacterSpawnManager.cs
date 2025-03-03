using Common.Characters;
using Common.Logging;
using Common.Utils.Vectors;
using Godot;

namespace Common.Managers.Spawning {
    public partial class CharacterSpawnManager : Node {
        private static readonly Logger logger = new (typeof(CharacterSpawnManager));

        public ICharacter Spawn (PackedScene prefab) {
            if (ISpawnPoint.SpawnPoints.Count <= 0) {
                logger.Error (Name + " tried to spawn character, but no points were found.");
                return null;
            }

            RandomNumberGenerator rng = new ();
            rng.Randomize ();
            int spawnIndex = rng.RandiRange (0, ISpawnPoint.SpawnPoints.Count - 1);
            Vector3 spawnPosition = ISpawnPoint.SpawnPoints[spawnIndex].GetSpawnPosition ();
            Node character = prefab.Instantiate ();
            GetTree ().CurrentScene.AddChild (character);

            switch (character) {
                case Node3D:
                    ((Node3D) character).GlobalPosition = spawnPosition;
                    break;
                case Node2D:
                    ((Node2D) character).GlobalPosition = VectorUtils.Vector3XYToVector2XY (spawnPosition);
                    break;
            }
            return (ICharacter) character;
        }
    }
}
