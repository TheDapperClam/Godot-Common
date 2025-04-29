using Common.Characters;
using Godot;

namespace Common.Controllers.Npcs {
    public partial class NonPlayableController : Node, IController {
        public ICharacter PossessedCharacter { get; private set; }

        public void Depossess () {
            if (PossessedCharacter == null) {
                return;
            }
            PossessedCharacter = null;
        }

        public void Possess (ICharacter character) {
            if (character == null || character == PossessedCharacter) {
                return;
            }
            PossessedCharacter = character;
        }
    }
}
