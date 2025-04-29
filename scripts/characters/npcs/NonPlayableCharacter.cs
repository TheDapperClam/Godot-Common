using Common.Controllers.Npcs;
using Godot;

namespace Common.Characters.Npcs {
    public partial class NonPlayableCharacter : Character3D {
        [Export]
        private NonPlayableController controller;

        public override void _Ready () {
            base._Ready ();

            controller.Possess (this);
        }
    }
}
