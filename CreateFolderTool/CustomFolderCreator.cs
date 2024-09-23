using UnityEditor;
using UnityEngine;
using System.IO;

public static class CustomFolderCreator
{
    [MenuItem("Assets/Create Folder", false, 0)]
    private static void CreateParentFolder()
    {
        // Get the selected directory in the project window
        string selectedPath = "Assets";

        // Check if something is selected
        UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        if (selection.Length > 0)
        {
            string path = AssetDatabase.GetAssetPath(selection[0]);
            if (File.Exists(path))
            {
                // If a file is selected, get its directory
                selectedPath = Path.GetDirectoryName(path);
            }
            else
            {
                // If a directory is selected, use it
                selectedPath = path;
            }
        }

        // Create Unknown Folder
        string unknownFolderPath = Path.Combine(selectedPath, "Unknown Folder");
        if (!Directory.Exists(unknownFolderPath))
        {
            AssetDatabase.CreateFolder(selectedPath, "Unknown Folder");
            Debug.Log("Parent folder created: " + unknownFolderPath);
        }
        else
        {
            Debug.Log("Parent folder already exists: " + unknownFolderPath);
        }
    }

    [MenuItem("Assets/Create 3 Folders", false, 1)]
    private static void CreateFolders()
    {
        // Get the selected directory in the project window
        string selectedPath = "Assets";

        // Check if something is selected
        UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        if (selection.Length > 0)
        {
            string path = AssetDatabase.GetAssetPath(selection[0]);
            if (File.Exists(path))
            {
                // If a file is selected, get its directory
                selectedPath = Path.GetDirectoryName(path);
            }
            else
            {
                // If a directory is selected, use it
                selectedPath = path;
            }
        }

        // Create Materials folder
        CreateFolder(selectedPath, "Materials");

        // Create Models folder
        CreateFolder(selectedPath, "Models");

        // Create Prefabs folder
        CreateFolder(selectedPath, "Prefabs");
    }

    private static void CreateFolder(string parentDirectory, string folderName)
    {
        string folderPath = Path.Combine(parentDirectory, folderName);

        // Check if the folder already exists
        if (!Directory.Exists(folderPath))
        {
            // If not, create the folder
            AssetDatabase.CreateFolder(parentDirectory, folderName);
            Debug.Log("Subfolder created: " + folderPath);
        }
        else
        {
            Debug.Log("Subfolder already exists: " + folderPath);
        }
    }
}
