using UnityEngine;

public static class Extensions {
    public static bool HasComponent<T>(this GameObject go) {
        return go.GetComponent<T>() != null;
    }

    public static T GetComponent<T>(this Collision2D col) where T : Component {
        return col.gameObject.GetComponent<T>();
    }

    public static bool HasComponent<T>(this Collision2D col) where T : Component {
        return col.gameObject.HasComponent<T>();
    }
}