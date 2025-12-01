using Godot;
using System;

namespace Common.Input.Mapping {
    public interface IInputActionMap {
        void Add (string action, Action<float> pressedAction, Action<float> releasedAction);
        void Process (InputEvent inputEvent);
    }
}
