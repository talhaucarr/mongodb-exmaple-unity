using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetTransform : MonoBehaviour
{
    [MenuItem("GameObject/Reset Transform Ctrl Shift R %#r")]
    private static void Reset()
    {
        if (!Selection.activeTransform) return;
        
        Undo.RegisterCompleteObjectUndo(Selection.transforms, "Back to position");

        foreach (var transform in Selection.transforms)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        
    }
}
