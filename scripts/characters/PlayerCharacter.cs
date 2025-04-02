using Common.Components.Actions.Movement;
using Common.Input.ActionMaps;
using Godot;

namespace Common.Characters {
	[GlobalClass]
	public partial class PlayerCharacter : Character3D, IActionResponder, ILooker {
		private ActionMap actionMap;

		public override void _Ready () {
			base._Ready ();

			actionMap = new ();
			AddChild (actionMap);
		}

		public ActionMap GetActionMap () {
			return actionMap;
		}

		public void Look (Vector3 direction, Vector3 up) {
			Vector3 flatDirection = new Plane (Vector3.Up).Project (direction);
			LookAt (GlobalPosition + flatDirection, up);
			ViewPoint.LookAt (ViewPoint.GlobalPosition + direction, up);
		}
	}
}
