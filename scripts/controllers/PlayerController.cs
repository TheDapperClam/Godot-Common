using Common.Characters;
using Godot;

namespace Common.Controllers {
    public abstract partial class PlayerController : Camera3D, IController {
        public ICharacter PossessedCharacter { get; private set; }

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            ProcessShared (delta);
            if (IsPossessing ()) {
                ProcessPossessed (delta);
            } else {
                ProcessDepossessed (delta);
            }
        }

        public override void _Process (double delta) {
            base._Process (delta);

            if (IsPossessing ()) {
                GlobalPosition = PossessedCharacter.GetViewPosition ();
            }
        }

        public void Depossess () {
            if (PossessedCharacter == null) {
                return;
            }
            PossessedCharacter = null;
        }

        public bool IsPossessing () {
            return PossessedCharacter != null;
        }

        public void Possess (ICharacter character) {
            if (character == null && character == PossessedCharacter) {
                return;
            }
            PossessedCharacter = character;
        }

        /// <summary>
        /// Called every physics frame for when the controller currently isn't possessing a character.
        /// Called after <see cref="ProcessShared"/>
        /// </summary>
        /// <param name="delta"></param>
        protected virtual void ProcessDepossessed (double delta) {
            // Base does nothing. We want this to be optional, so no abstract
        }

        /// <summary>
        /// Called every physics frame for when the controller is currently possessing a character.
        /// Called after <see cref="ProcessShared"/>
        /// </summary>
        /// <param name="delta"></param>
        protected virtual void ProcessPossessed (double delta) {
            // Base does nothing. We want this to be optional, so no abstract
        }

        /// <summary>
        /// Called every physics frame regardless of if the controller is posessing a character or not.
        /// Called before <see cref="ProcessDepossessed"/> and <see cref="ProcessPossessed"/>
        /// </summary>
        /// <param name="delta"></param>
        protected virtual void ProcessShared (double delta) {
            // Base does nothing. We want this to be optional, so no abstract
        }
    }
}
