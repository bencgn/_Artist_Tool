using UnityEngine;
using UnityEditor;

public class CreateFoldersInProject : MonoBehaviour
{
    [MenuItem("_Artist_Tool/Create Folders")]
    public static void CreateFolders()
    {
        // Get the selected folder (this should be the "Assets" folder in your case)
        string selectedFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (!string.IsNullOrEmpty(selectedFolderPath))
        {
            // Create subdirectories for Materials, Prefabs, and Models
            CreateSubfolder(selectedFolderPath, "Materials");
            CreateSubfolder(selectedFolderPath, "Prefabs");
            CreateSubfolder(selectedFolderPath, "Models");
        }
        else
        {
            Debug.LogWarning("Please select a valid folder in the Project view.");
        }
    }

    private static void CreateSubfolder(string parentFolder, string subfolderName)
    {
        string subfolderPath = System.IO.Path.Combine(parentFolder, subfolderName);
        if (!AssetDatabase.IsValidFolder(subfolderPath))
        {
            AssetDatabase.CreateFolder(parentFolder, subfolderName);
            Debug.Log("Created folder: " + subfolderPath);
        }
        else
        {
            Debug.LogWarning("Folder already exists: " + subfolderPath);
        }
    }
}
