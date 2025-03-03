using Godot;

namespace Common.Gamemodes {
    public abstract partial class Gamemode : Node {
        public override void _Ready () {
            base._Ready ();

            CallDeferred (nameof (GamemodeReady));
        }

        /// <summary>
        /// Called during the idle frame when the gamemode is ready
        /// </summary>
        protected virtual void GamemodeReady () {
            // Base does nothing
        }
    }
}
