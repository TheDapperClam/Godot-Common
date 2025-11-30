using System;

namespace Common.Input.ActionMaps {
    [Obsolete ("Action maps are depricated, use input piping")]
    public interface IActionResponder {
        ActionMap GetActionMap ();
    }
}
