using Godot;
using System;

namespace Common.Components.Sockets {
    [GlobalClass]
    public partial class PlayerSocket : Socket {
        public override void _Input (InputEvent @event) {
            base._Input (@event);

            Pipe (@event);
        }
    }
}
