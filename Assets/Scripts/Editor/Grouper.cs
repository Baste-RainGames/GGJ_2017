using UnityEditor;
using UnityEngine;

public static class Grouper {

    [MenuItem("Commands/Group %g")]
    public static void GroupSelected() {
        var objects = Selection.gameObjects;
        if (objects.Length == 0)
            return;

        var rootObject = new GameObject(objects[0].name + "_Group");
        Undo.RegisterCreatedObjectUndo(rootObject, "Create group");

        foreach (var gameObject in Selection.gameObjects) {
            Undo.SetTransformParent(gameObject.transform, rootObject.transform, "Create group");
        }

        Selection.activeGameObject = rootObject;
    }
}