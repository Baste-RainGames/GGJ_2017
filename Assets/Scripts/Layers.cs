using UnityEngine;

public static class Layers {

    private static int geometryMaskWithoutBlink;
    private static int geometryMask;
    private static bool layersInitialized;

    public static int GeometryMask {
        get {
            EnsureLayersInitialized();
            return geometryMask;
        }
    }

    public static int GeometryMaskWithoutBlink {
        get {
            EnsureLayersInitialized();
            return geometryMaskWithoutBlink;
        }
    }

    private static void EnsureLayersInitialized() {
        if (layersInitialized)
            return;
        layersInitialized = true;

        geometryMaskWithoutBlink = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("3D") | 1 << LayerMask.NameToLayer("Geometry");
        geometryMask = geometryMaskWithoutBlink | 1 << LayerMask.NameToLayer("Can_Blink_Though");
    }
}