using UnityEditor;
using UnityEngine;

public class DivideObjectsTool : EditorWindow
{
	private float spacing = 0.5f; // Default spacing value

    [MenuItem("_Artist_Tool/Divide Objects")]
    public static void ShowWindow()
    {
        GetWindow<DivideObjectsTool>("Divide Objects");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select GameObjects and set spacing:");

        spacing = EditorGUILayout.FloatField("Spacing:", spacing);

        if (GUILayout.Button("Divide z"))
        {
            DivideSelectedObjects();
        }
    }

    private void DivideSelectedObjects()
    {
        // Get the selected GameObjects
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length < 2)
        {
            Debug.LogWarning("Select at least two GameObjects to divide along the z-axis.");
            return;
        }

        // Arrange the objects along the X-axis
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            float xPosition = i * spacing;
            Vector3 newPosition = selectedObjects[i].transform.position;
            newPosition.z = xPosition;
            selectedObjects[i].transform.position = newPosition;
        }
    }
}
