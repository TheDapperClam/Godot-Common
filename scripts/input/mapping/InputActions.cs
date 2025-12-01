using Godot;
using System;

namespace Common.Input.Mapping {
    public class InputActions {
        public Action<Variant> Pressed { get; set; }
        public Action<Variant> Released { get; set; }

        public InputActions (Action<Variant> pressed, Action<Variant> released) {
            Pressed = pressed;
            Released = released;
        }
    }
}
