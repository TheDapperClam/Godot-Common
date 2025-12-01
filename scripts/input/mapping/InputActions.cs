using Godot;
using System;

namespace Common.Input.Mapping {
    public class InputActions {
        public Action<float> Pressed { get; set; }
        public Action<float> Released { get; set; }

        public InputActions (Action<float> pressed, Action<float> released) {
            Pressed = pressed;
            Released = released;
        }
    }
}
