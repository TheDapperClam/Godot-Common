using Godot;
using System;

namespace Common.Characters {
    [Obsolete ("Characters are deprecated, use sockets instead")]
    public interface ICharacter {
        Vector3 GetViewPosition ();
    }
}
