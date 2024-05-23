using UnityEngine;
using UnityEditor;

public class CreateParent : MonoBehaviour
{
    [MenuItem("_Artist_Tool/Create Parent with Same Name %y", false, 0)]
    private static void CreateEmptyParentWithSameName()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("Please select at least one object in the hierarchy.");
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            GameObject parentObj = new GameObject(obj.name);
            parentObj.transform.SetParent(obj.transform.parent);
            obj.transform.SetParent(parentObj.transform);
        }
    }

    [MenuItem("_Artist_Tool/Create Parent with Same Name %y", true)]
    private static bool ValidateCreateEmptyParentWithSameName()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("_Artist_Tool/Drag to Folder as Prefab", false, 1)]
    private static void DragToFolderAsPrefab()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("Please select at least one object in the hierarchy.");
            return;
        }

        string folderPath = EditorUtility.OpenFolderPanel("Select folder to save prefab", "", "");
        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogWarning("Please select a valid folder.");
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            string prefabPath = folderPath + "/" + obj.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
        }
    }

    [MenuItem("_Artist_Tool/Drag to Folder as Prefab", true)]
    private static bool ValidateDragToFolderAsPrefab()
    {
        return Selection.gameObjects.Length > 0;
    }
}
