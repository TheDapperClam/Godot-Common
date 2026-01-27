using Godot;
using Godot.Collections;

namespace Common.Components.Inventories {
    [GlobalClass]
    public partial class Inventory : Node {
        private static readonly Logging.Logger logger = new (typeof(Inventory));

        [Export]
        private Array<Node> items = [];

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

        public Node Get (int index) {
            if (index < 0 || index >= items.Count) {
                return null;
            }
            return items[index];
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

        public int Size () {
            return items.Count;
        }
    }
}
