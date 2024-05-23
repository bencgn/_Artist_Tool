using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameObjectRotationTool : EditorWindow
{
    [MenuItem("Tools/GameObject Rotation Tool")]
    public static void ShowWindow()
    {
        GetWindow<GameObjectRotationTool>("GameObject Rotation Tool");
    }

    [SerializeField] private GameObject targetObject;
    private float rotationAngle;

    private void OnGUI()
    {
        GUILayout.Label("Select Target Object:", EditorStyles.boldLabel);
        targetObject = EditorGUILayout.ObjectField(targetObject, typeof(GameObject), true) as GameObject;

        if (targetObject != null)
        {
            EditorGUILayout.Space();
            GUILayout.Label("Rotation Control:", EditorStyles.boldLabel);

            rotationAngle = EditorGUILayout.Slider("Rotation Angle", rotationAngle, 0f, 720f);
            rotationAngle = GUILayout.HorizontalScrollbar(rotationAngle, 0f, 0f, 720f);

            RotateObject();
        }
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void RotateObject()
    {
        if (targetObject != null)
        {
            Vector3 rotation = targetObject.transform.rotation.eulerAngles;
            rotation.y = rotationAngle;
            targetObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
