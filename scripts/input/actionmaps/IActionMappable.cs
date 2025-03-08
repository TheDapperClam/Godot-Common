using Common.Input.ActionMaps;
using System;
using System.Collections.Generic;

namespace Common.Input.ActionMaps {
    public interface IActionMappable {
        Dictionary<string, PressReleaseActions> GetActions ();
    }
}
