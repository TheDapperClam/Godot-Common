using Godot;
using Common.Utils.Vectors;
using Common.Items.Usables;
using Common.Managers.Settings;
using Common.Components.Actions.Movement;

namespace Common.Controllers.Player {
	public partial class FlyCamPlayerController : PlayerController {
		private const float CAMERA_X_CLAMP = Mathf.Pi * 0.5f;
		private const string MOVE_FORWARD_ACTION = "move_up";
		private const string MOVE_BACK_ACTION = "move_down";
		private const string MOVE_LEFT_ACTION = "move_left";
		private const string MOVE_RIGHT_ACTION = "move_right";
		private const string USE_ACTION = "use";
		private const string SETTING_LOOK_SENSITIVITY = "godot_common/settings/input/mouse_and_keyboard/mouse_sensitivity";

		[Export]
		private float flySpeed = 10.0f;
		[Export]
		private float useRange = 3.0f;

		private bool doUse;
		private Vector2 lookSensitivity = Vector2.One;
		private Vector2 moveInput;
		private Vector2 mouseMovement;
		private RayCast3D useRaycast;

		public override void _Input (InputEvent @event) {
			base._Input (@event);

			if (Godot.Input.MouseMode == Godot.Input.MouseModeEnum.Captured) {
				moveInput.X = Godot.Input.GetAxis (MOVE_LEFT_ACTION, MOVE_RIGHT_ACTION);
				moveInput.Y = Godot.Input.GetAxis (MOVE_FORWARD_ACTION, MOVE_BACK_ACTION);
				moveInput = moveInput.Normalized ();
			} else {
				moveInput = Vector2.Zero;
			}

			switch (@event) {
				case InputEventMouseMotion mouseMotionEvent:
					mouseMovement = mouseMotionEvent.Relative;
					break;
			}

			doUse = Godot.Input.IsActionJustPressed (USE_ACTION);
		}

		public override void _PhysicsProcess (double delta) {
			base._PhysicsProcess (delta);

			// Happens after Controller process functions
			mouseMovement = Vector2.Zero;
		}

		public override void _Ready () {
			base._Ready ();

			Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
			lookSensitivity = SettingsManager.GetSetting (SETTING_LOOK_SENSITIVITY, Vector2.One).AsVector2 ();

			useRaycast = new () {
				TargetPosition = Vector3.Forward * useRange,
				Enabled = true
			};
			AddChild (useRaycast);
		}

		protected override void ProcessDepossessed (double delta) {
			base.ProcessDepossessed (delta);

			Vector3 movement = VectorUtils.Vector2XYToVector3XZ (moveInput);
			Translate (movement * flySpeed * (float) delta);
		}

		protected override void ProcessPossessed (double delta) {
			base.ProcessPossessed (delta);

			if (PossessedCharacter is ILooker looker) {
				looker.Look (-GlobalBasis.Z, GlobalBasis.Y);
			}
		}

		protected override void ProcessShared (double delta) {
			base.ProcessShared (delta);

			Vector2 mouseMovementWithSensitivity = -mouseMovement * lookSensitivity * (float) delta;
			Vector3 clampedRotation = Rotation;
			clampedRotation.Y += mouseMovementWithSensitivity.X;
			clampedRotation.X = Mathf.Clamp (clampedRotation.X + mouseMovementWithSensitivity.Y, -CAMERA_X_CLAMP, CAMERA_X_CLAMP);
			Rotation = clampedRotation;

			if (doUse) {
				doUse = false;
				if (useRaycast.IsColliding () && useRaycast.GetCollider () is IUsable usable) {
					usable.Use (PossessedCharacter == null ? this : (Node) PossessedCharacter);
				}
			}
		}
	}
}
