using Common.Components.Actions.Movement;
using Common.Input.ActionMaps;
using Godot;
using System;

namespace Common.Characters {
    [Obsolete ("Characters are deprecated, use sockets instead")]
    [GlobalClass]
	public partial class PlayerCharacter : Character3D, IActionResponder {
		private ActionMap actionMap;

		public override void _Ready () {
			base._Ready ();

			actionMap = new ();
			AddChild (actionMap);
		}

		public ActionMap GetActionMap () {
			return actionMap;
		}
	}
}
