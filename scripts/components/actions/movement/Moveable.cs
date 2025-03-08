using Common.Input.ActionMaps;
using Common.Managers.Settings;
using Godot;
using System.Collections.Generic;

namespace Common.Components.Actions.Movement {
    [GlobalClass]
    public partial class Moveable : Node, IMoveable, IActionMappable {
        private const string GRAVITY_SETTING = "physics/3d/default_gravity";

        [Export]
        public float GravityScale { get; set; } = 1.0f;

        [Export]
        private string moveDownAction = "move_down";
        [Export]
        private string moveLeftAction = "move_left";
        [Export]
        private string moveRightAction = "move_right";
        [Export]
        private string moveUpAction = "move_up";
        [Export]
        private float moveSpeed = 200.0f;

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

            gravity = (float) SettingsManager.GetSetting (GRAVITY_SETTING, 9.8f).AsDouble ();
        }

        public Dictionary<string, PressReleaseActions> GetActions () {
            return new () {
                { moveDownAction, new (MoveDownPressed, MoveDownReleased) },
                { moveLeftAction, new (MoveLeftPressed, MoveLeftReleased) },
                { moveRightAction, new (MoveRightPressed, MoveRightReleased) },
                { moveUpAction, new (MoveUpPressed, MoveUpReleased) }
            };
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

        private void MoveDownPressed () {
            movingDown = true;
        }

        private void MoveLeftPressed () {
            movingLeft = true;
        }

        private void MoveRightPressed () {
            movingRight = true;
        }

        private void MoveUpPressed () {
            movingUp = true;
        }

        private void MoveDownReleased () {
            movingDown = false;
        }

        private void MoveLeftReleased () {
            movingLeft = false;
        }

        private void MoveRightReleased () {
            movingRight = false;
        }

        private void MoveUpReleased () {
            movingUp = false;
        }
    }
}
