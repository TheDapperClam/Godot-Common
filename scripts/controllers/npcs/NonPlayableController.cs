using Common.Characters;
using Common.Logging;
using Godot;
using System;

namespace Common.Controllers.Npcs {
    [Obsolete ("Controllers are deprecated, use sockets instead")]
    public partial class NonPlayableController : Node3D, IController {
        private static readonly Logger logger = new (typeof (NonPlayableController));

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

        public virtual void Depossess () {
            if (PossessedCharacter == null) {
                return;
            }
            logger.Debug ("{0} depossessed {1} and is queuing to be freed", Name, ((Node) PossessedCharacter).Name);
            PossessedCharacter = null;
            QueueFree ();
        }

        public bool IsPossessing () {
            return PossessedCharacter != null;
        }

        public virtual void Possess (ICharacter character) {
            if (character == null || character == PossessedCharacter) {
                return;
            }
            PossessedCharacter = character;
            ((Node) PossessedCharacter).TreeExiting += OnPossessedCharacterExitingTree;
            logger.Debug ("{0} possessed {1}", Name, ((Node) PossessedCharacter).Name);
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

        private void OnPossessedCharacterExitingTree () {
            Depossess ();
        }
    }
}
