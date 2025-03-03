using Godot;

namespace Common.Utils.Nodes {
    public static class NodeUtils {
        public static T GetChildOfType<T> (Node parent, bool recursive = true) where T : Node {
            foreach (Node child in parent.GetChildren ()) {
                if (child is T childOfType) {
                    return childOfType;
                }
                if (recursive && child.GetChildCount () > 0) {
                    T recursiveChildOfType = GetChildOfType<T> (child, recursive);
                    if (recursiveChildOfType != null) {
                        return recursiveChildOfType;
                    }
                }
            }
            return null;
        }
    }
}
