using System;

namespace Common.Input.ActionMaps {
    [Obsolete ("Action maps are depricated, use input piping")]
    public struct PressReleaseActions {
        public Action PressedAction { get; set; }
        public Action ReleasedAction { get; set; }

        public PressReleaseActions (Action pressedAction, Action releasedAction) {
            PressedAction = pressedAction;
            ReleasedAction = releasedAction;
        }
    }
}
