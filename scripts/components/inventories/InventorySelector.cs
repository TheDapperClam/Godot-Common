using Common.Input.ActionMaps;
using Common.Logging;
using Godot;
using System.Collections.Generic;

namespace Common.Components.Inventories {
    [GlobalClass]
    public partial class InventorySelector : Node, IActionMappable {
        private static readonly Logger logger = new (typeof (InventorySelector));

        [Signal]
        public delegate void SelectionChangedEventHandler (Node selected);

        [Export]
        private Inventory targetInventory;
        [Export]
        private string next_action = "inventory_next";
        [Export]
        private string previous_action = "inventory_previous";

        private int index;

        public Dictionary<string, PressReleaseActions> GetActions () {
            return new () {
                { next_action, new (InventoryNextPressed, null) },
                { previous_action, new (InventoryPreviousPressed, null) }
            };
        }

        public void SetIndex (int newIndex) {
            if (targetInventory.Size () > 0) {
                if (newIndex >= targetInventory.Size ()) {
                    newIndex = 0;
                } else if (newIndex < 0) {
                    newIndex = targetInventory.Size () - 1;
                }
                index = newIndex;
                logger.Debug ("Selected index {0} for inventory {1}", index, targetInventory.Name);
                EmitSignal (SignalName.SelectionChanged, targetInventory.Get (index));
            } else {
                if (logger.IsDebugEnabled) {
                    logger.Warn ("Inventory {0} has a size of 0, cannot set selector index for {1}", targetInventory.Name, Name);
                }
            }
        }

        private void InventoryNextPressed () {
            SetIndex (index + 1);
        }

        private void InventoryPreviousPressed () {
            SetIndex (index - 1);
        }
    }
}
