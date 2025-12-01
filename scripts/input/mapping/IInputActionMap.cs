using Godot;
using System;

namespace Common.Input.Mapping {
    public interface IInputActionMap {
        void Add (string action, Action<Variant> pressedAction, Action<Variant> releasedAction);
        void Process (InputEvent inputEvent, Variant arg);
    }
}
