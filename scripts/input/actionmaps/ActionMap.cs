using Common.Logging;
using Common.Utils.Nodes;
using Godot;
using System;
using System.Collections.Generic;

namespace Common.Input.ActionMaps {
    public partial class ActionMap : Node {
        private const string PRESSED_ACTION_SUFFIX = "Pressed";
        private const string RELEASED_ACTION_SUFFIX = "Released";

        private static readonly Logger logger = new (typeof (ActionMap));

        private readonly Dictionary<string, Dictionary<IActionMappable, List<PressReleaseActions>>> actionMap = [];
        private readonly HashSet<IActionMappable> mappablesWithMappedActions = [];

        public override void _Ready () {
            base._Ready ();

            GetParent ().ChildEnteredTree += OnParentChildEnteredTree;
            GetParent ().ChildExitingTree += OnParentChildExitingTree;
            foreach (IActionMappable mappableChild in NodeUtils.GetChildrenOfType<IActionMappable> (GetParent ())) {
                MapActions (mappableChild);
            }
        }

        public void ProcessInput (InputEvent @event) {
            foreach (string actionName in actionMap.Keys) {
                if (@event.IsAction (actionName)) {
                    foreach (KeyValuePair<IActionMappable, List<PressReleaseActions>> kvp in actionMap[actionName]) {
                        foreach (PressReleaseActions actions in kvp.Value) {
                            if (@event.IsPressed ()) {
                                actions.PressedAction?.Invoke ();
                            } else {
                                actions.ReleasedAction?.Invoke ();
                            }
                        }
                    }
                }
            }
        }

        private void MapActions (IActionMappable mappable) {
            logger.Debug ("Mapping actions for {0}", ((Node) mappable).Name);
            mappablesWithMappedActions.Add (mappable);
            foreach (KeyValuePair<string, PressReleaseActions> kvp in mappable.GetActions ()) {
                if (!actionMap.ContainsKey (kvp.Key)) {
                    actionMap.Add (kvp.Key, []);
                }
                if (!actionMap[kvp.Key].ContainsKey (mappable)) {
                    actionMap[kvp.Key].Add (mappable, []);
                }
                actionMap[kvp.Key][mappable].Add (kvp.Value);
            }
        }

        private void OnParentChildEnteredTree (Node child) {
            if (child is IActionMappable mappable) {
                MapActions (mappable);
            }
        }

        private void OnParentChildExitingTree (Node child) {
            if (child is IActionMappable mappable && mappablesWithMappedActions.Contains (mappable)) {
                UnmapActions (mappable);
            }
        }

        private void UnmapActions (IActionMappable mappable) {
            logger.Debug ("Unmapping actions for {0}", ((Node) mappable).Name);
            mappablesWithMappedActions.Remove (mappable);
            foreach (string action in mappable.GetActions ().Keys) {
                actionMap[action].Remove (mappable);
            }
        }
    }
}
