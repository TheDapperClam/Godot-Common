using System;

namespace Common.Input.Mapping {
    public class InputActions {
        public Action Pressed { get; set; }
        public Action Released { get; set; }

        public InputActions (Action pressed, Action released) {
            Pressed = pressed;
            Released = released;
        }
    }
}
