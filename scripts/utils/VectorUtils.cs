using Godot;

namespace Common.Utils.Vectors {
    public static class VectorUtils {
        public static Vector3 Vector2XYToVector3XZ (Vector2 vector) {
            return new Vector3 (vector.X, 0.0f, vector.Y);
        }

        public static Vector2 Vector3XYToVector2XY (Vector3 vector) {
            return new Vector2 (vector.X, vector.Y);
        }
    }
}
