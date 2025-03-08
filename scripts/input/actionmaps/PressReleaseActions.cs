using System;

namespace Common.Input.ActionMaps {
    public struct PressReleaseActions {
        public Action PressedAction { get; set; }
        public Action ReleasedAction { get; set; }

        public PressReleaseActions (Action pressedAction, Action releasedAction) {
            PressedAction = pressedAction;
            ReleasedAction = releasedAction;
        }
    }
}
