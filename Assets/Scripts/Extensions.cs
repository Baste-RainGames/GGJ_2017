using UnityEngine;

public static class Extensions {
    public static bool HasComponent<T>(this GameObject go) {
        return go.GetComponent<T>() != null;
    }

    public static T GetComponent<T>(this Collision2D col) {
        return col.gameObject.GetComponent<T>();
    }

    public static bool HasComponent<T>(this Collision2D col) {
        return col.gameObject.HasComponent<T>();
    }

    public static bool HasComponent<T>(this Component comp) {
        return comp.gameObject.HasComponent<T>();
    }

    public static Vector3 WithZ(this Vector3 vector, float newZ) {
        vector.z = newZ;
        return vector;
    }

    public static Vector3 WithZ(this Vector2 vector, float newZ) {
        Vector3 threeDee = vector;
        threeDee.z = newZ;
        return threeDee;
    }

    public static Transform[] GetDirectChildren(this Transform transform) {
        var children = new Transform[transform.childCount];
        for (int i = 0; i < children.Length; i++) {
            children[i] = transform.GetChild(i);
        }

        return children;
    }
}