using Common.Controllers.Npcs;
using Godot;
using System;

namespace Common.Characters.Npcs {
    [Obsolete ("Characters are deprecated, use sockets instead")]
    public partial class NonPlayableCharacter : Character3D {
        [Export]
        private NonPlayableController controller;

        public override void _Ready () {
            base._Ready ();

            controller.Possess (this);
        }
    }
}
