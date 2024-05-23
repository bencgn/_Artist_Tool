using UnityEngine;
using UnityEditor;

public class CustomEditorTool : Editor
{
    [MenuItem("_Artist_Tool/Show UI")]
    private static void ShowUI()
    {
        // Create a custom editor window.
        EditorWindow.GetWindow<CustomEditorWindow>("UI Tool");
    }
}

public class CustomEditorWindow : EditorWindow
{
    private bool showUI = false;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Click the button to show the UI:");

        if (GUILayout.Button("Click Here"))
        {
            showUI = !showUI;
        }

        if (showUI)
        {
            // Display your UI elements here.
            EditorGUILayout.LabelField("UI is visible!");
        }
    }
}
