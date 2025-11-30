using Common.Input.Piping;
using Common.Managers.Settings;
using Godot;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Moveable : Node, IActionPipe, IMoveable {
        private const string GRAVITY_SETTING = "physics/3d/default_gravity";

        [Export]
        public float GravityScale { get; set; } = 1.0f;

        [Export]
        private float moveSpeed = 200.0f;

        private PipeMapper pipeController;
        private float gravity;
        private bool movingDown;
        private bool movingLeft;
        private bool movingRight;
        private bool movingUp;

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            float horizontal = (movingLeft ? -1f : 0f) + (movingRight ? 1f : 0f);
            float vertical = (movingUp ? -1f : 0f) + (movingDown ? 1f : 0f);
            Move (new Vector2 (horizontal, vertical).Normalized (), delta);
        }

        public override void _Ready () {
            base._Ready ();

            pipeController = new (this);
            gravity = (float) SettingsManager.GetSetting (GRAVITY_SETTING, 9.8f).AsDouble ();
        }

        public void Move (Vector2 direction, double delta) {
            if (GetParent () is CharacterBody3D body3d) {
                Vector2 relativeDirection = direction.Rotated (-body3d.GlobalRotation.Y);
                Vector3 velocity = body3d.GetRealVelocity ();
                velocity.X = relativeDirection.X * moveSpeed * (float) delta;
                velocity.Z = relativeDirection.Y * moveSpeed * (float) delta;
                velocity.Y -= gravity * GravityScale * (float) delta;
                body3d.Velocity = velocity;
                body3d.MoveAndSlide ();
            }
        }

        public void Pipe (InputEvent inputEvent) {
            pipeController.Pipe (inputEvent);
        }

        [PippedAction ("move_down", true)]
        private void MoveDownPressed () {
            movingDown = true;
        }

        [PippedAction ("move_left", true)]
        private void MoveLeftPressed () {
            movingLeft = true;
        }

        [PippedAction ("move_right", true)]
        private void MoveRightPressed () {
            movingRight = true;
        }

        [PippedAction ("move_up", true)]
        private void MoveUpPressed () {
            movingUp = true;
        }

        [PippedAction ("move_down", false)]
        private void MoveDownReleased () {
            movingDown = false;
        }

        [PippedAction ("move_left", false)]
        private void MoveLeftReleased () {
            movingLeft = false;
        }

        [PippedAction ("move_right", false)]
        private void MoveRightReleased () {
            movingRight = false;
        }

        [PippedAction ("move_up", false)]
        private void MoveUpReleased () {
            movingUp = false;
        }
    }
}
