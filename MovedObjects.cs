using UnityEngine;
using UnityEditor;

public class MoveToNewParentEditor : MonoBehaviour
{
    [MenuItem("GameObject/Move To New Parent %#m", false, 0)]
    private static void MoveToNewParent()
    {
        // Check if there are any selected GameObjects
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("No GameObjects selected in the Hierarchy");
            return;
        }

        // Create a new parent GameObject
        GameObject newParent = new GameObject("NewParent");

        // Loop through all selected GameObjects
        foreach (GameObject obj in Selection.gameObjects)
        {
            // Set the new parent
            Undo.SetTransformParent(obj.transform, newParent.transform, "Move to New Parent");
        }
    }
}
