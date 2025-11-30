using Common.Input.ActionMaps;
using System;
using System.Collections.Generic;

namespace Common.Input.ActionMaps {
    [Obsolete ("Action maps are depricated, use input piping")]
    public interface IActionMappable {
        Dictionary<string, PressReleaseActions> GetActions ();
    }
}
