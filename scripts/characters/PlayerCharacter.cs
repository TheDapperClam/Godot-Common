using Common.Components.Actions.Movement;
using Common.Input.ActionMaps;
using Godot;

namespace Common.Characters {
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
