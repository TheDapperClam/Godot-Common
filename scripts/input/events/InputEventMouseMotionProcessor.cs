using Godot;

namespace Common.Input.Events {
    [GlobalClass]
    public partial class InputEventMouseMotionProcessor : Node, IInputEventProcessor {
        private const float STRENGTH_DAMP = 5.0f;

        [Export]
        private string mouseMoveDownAction = "look_down";
        [Export]
        private string mouseMoveLeftAction = "look_left";
        [Export]
        private string mouseMoveRightAction = "look_right";
        [Export]
        private string mouseMoveUpAction = "look_up";

        private Vector2 mouseRelative;
        private bool movingDown;
        private bool movingLeft;
        private bool movingRight;
        private bool movingUp;

        public override void _Input (InputEvent @event) {
            base._Input (@event);

            Process (@event);
        }

        public override void _Process (double delta) {
            base._Process (delta);

            ProcessDown (mouseRelative);
            ProcessLeft (mouseRelative);
            ProcessRight (mouseRelative);
            ProcessUp (mouseRelative);
            mouseRelative = Vector2.Zero;
        }

        public bool Process (InputEvent inputEvent) {
            if (inputEvent is InputEventMouseMotion mouseMotion) {
                mouseRelative = mouseMotion.Relative;
                return true;
            }
            return false;
        }

        private void ProcessDown (Vector2 relative) {
            if (relative.Y > 0f) {
                movingDown = true;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveDownAction,
                    Pressed = true,
                    Strength = Mathf.Abs (relative.Y) / STRENGTH_DAMP
                });
            } else if (movingDown) {
                movingDown = false;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveDownAction,
                    Pressed = false,
                    Strength = 0f
                });
            }
        }

        private void ProcessLeft (Vector2 relative) {
            if (relative.X < 0f) {
                movingLeft = true;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveLeftAction,
                    Pressed = true,
                    Strength = Mathf.Abs (relative.X) / STRENGTH_DAMP
                });
            } else if (movingLeft) {
                movingLeft = false;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveLeftAction,
                    Pressed = false,
                    Strength = 0f
                });
            }
        }

        private void ProcessRight (Vector2 relative) {
            if (relative.X > 0f) {
                movingRight = true;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveRightAction,
                    Pressed = true,
                    Strength = Mathf.Abs (relative.X) / STRENGTH_DAMP
                });
            } else if (movingRight) {
                movingRight = false;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveRightAction,
                    Pressed = false,
                    Strength = 0f
                });
            }
        }

        private void ProcessUp (Vector2 relative) {
            if (relative.Y < 0f) {
                movingUp = true;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveUpAction,
                    Pressed = true,
                    Strength = Mathf.Abs (relative.Y) / STRENGTH_DAMP
                });
            } else if (movingUp) {
                movingUp = false;
                Godot.Input.ParseInputEvent (new InputEventAction () {
                    Action = mouseMoveUpAction,
                    Pressed = false,
                    Strength = 0f
                });
            }
        }
    }
}
