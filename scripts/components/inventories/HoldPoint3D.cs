using Godot;

namespace Common.Components.Inventories {
    [GlobalClass]
    public partial class HoldPoint3D : Node {
        [Export]
        private Node3D targetPoint;
        [Export]
        private InventorySelector targetSelector;

        private Node3D selectedNode;

        public override void _PhysicsProcess (double delta) {
            base._PhysicsProcess (delta);

            if (selectedNode != null) {
                selectedNode.GlobalPosition = targetPoint.GlobalPosition;
                selectedNode.GlobalRotation = targetPoint.GlobalRotation;
            }
        }

        public override void _Ready () {
            base._Ready ();

            OnSelectionChanged (targetSelector.GetSelected ());
            targetSelector.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged (Node selected) {
            if (selectedNode != null) {
                selectedNode.Hide ();
                selectedNode.TopLevel = false;
            }
            if (selected is Node3D node3d) {
                selectedNode = node3d;
                selectedNode.TopLevel = true;
                selectedNode.Show ();
            } else {
                selectedNode = null;
            }
        }
    }
}
