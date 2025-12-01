using Common.Items.Fireables;
using Common.Logging;
using Godot;

namespace Common.Components.Inventories {
    // TODO: Add support for socket system input
    [GlobalClass]
    public partial class InventorySelector : Node {
        private static readonly Logger logger = new (typeof (InventorySelector));

        [Signal]
        public delegate void SelectionChangedEventHandler (Node selected);

        [Export]
        private Inventory targetInventory;
        [Export]
        private string firePrimaryAction = "fire1";
        [Export]
        private string nextAction = "inventory_next";
        [Export]
        private string previousAction = "inventory_previous";

        private int index;
        private bool firePrimary;
        private bool firePrimaryReleased;

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            Node currentItem = GetSelected ();

            if (firePrimary) {
                if (currentItem is IFireable fireable) {
                    fireable.Fire ();
                }
            } else if (firePrimaryReleased) {
                if (currentItem is IHoldFireable holdFireable) {
                    holdFireable.FireReleased ();
                }
                firePrimaryReleased = false;
            }
        }

        public Node GetSelected () {
            return targetInventory.Get (index);
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

        private void FirePrimaryPressed () {
            firePrimary = true;
        }

        private void FirePrimaryReleased () {
            firePrimary = false;
            firePrimaryReleased = true;
        }

        private void InventoryNextPressed () {
            SetIndex (index + 1);
        }

        private void InventoryPreviousPressed () {
            SetIndex (index - 1);
        }
    }
}
