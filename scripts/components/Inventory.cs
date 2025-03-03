using Common.Logging;
using Godot;
using Godot.Collections;

namespace Common.Components.Inventories {
    [GlobalClass]
    public partial class Inventory : Node {
        private static readonly Logger logger = new (typeof(Inventory));

        private Array<Node> items = new ();

        public void Add (Node item) {
            if (item == null) {
                logger.Error ("Tried to add null to inventory " + Name);
                return;
            }
            if (items.Contains (item)) {
                logger.Error ("Tried to add item " + item.Name + " already in inventory " + Name);
                return;
            }
            logger.Debug ("Adding item " + item.Name + " to inventory " + Name);
            items.Add (item);
        }

        public void Remove (Node item) {
            if (item == null) {
                logger.Error ("Tried to remove null item from inventory " + Name);
                return;
            }
            if (!items.Contains (item)) {
                logger.Error ("Tried to remove item " + item.Name + " not in inventory " + Name);
                return;
            }
            logger.Debug ("Removing item " + item.Name + " to inventory " + Name);
            items.Remove (item);
        }
    }
}
