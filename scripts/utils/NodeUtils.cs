using Godot;
using System.Collections.Generic;

namespace Common.Utils.Nodes {
    public static class NodeUtils {
        public static T GetChildOfType<T> (Node parent) {
            foreach (Node child in parent.GetChildren ()) {
                if (child is T childOfType) {
                    return childOfType;
                }
            }
            return default;
        }

        public static List<T> GetChildrenOfType<T> (Node parent) {
            List<T> childrenOfType = new ();

            foreach (Node child in parent.GetChildren ()) {
                if (child is T childOfType) {
                    childrenOfType.Add (childOfType);
                }
            }
            return childrenOfType;
        }
    }
}
