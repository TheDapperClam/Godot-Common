using Common.Input.Mapping;
using Common.Input.Piping;
using Godot;

namespace Common.Components.Actions {
    public abstract partial class ActionBase : Node, IInputPipe {
        protected readonly IInputActionMap inputActionMap = new InputActionMap ();

        public virtual void Pipe (InputEvent inputEvent) {
            inputActionMap.Process (inputEvent);
        }
    }
}
