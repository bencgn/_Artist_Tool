using UnityEngine;
using UnityEditor;

public class SaveRenderTextureToFile : EditorWindow
{
    private RenderTexture rt;
    private bool autoRenderFrames;
    private bool isRenderingPaused;
    private float frameRenderInterval = 0.1f;
    private int frameNumber;

    // Modify the base folder path
    private string baseFolderPath = "Assets/_Artist_Tool/Scene_Capture/";

    [MenuItem("_Artist_Tool/Save RenderTexture to file &q")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SaveRenderTextureToFile>("Save RT to File");
    }

    private void OnGUI()
    {
        GUILayout.Label("Save RenderTexture to file", EditorStyles.boldLabel);

        rt = EditorGUILayout.ObjectField("RenderTexture", rt, typeof(RenderTexture), true) as RenderTexture;

        if (GUILayout.Button("Save Current Frame"))
        {
            SaveRTToFile();
        }

        GUILayout.Space(10);
        GUILayout.Label("Auto Render Frames", EditorStyles.boldLabel);
        autoRenderFrames = EditorGUILayout.Toggle("Enable Auto Render", autoRenderFrames);

        EditorGUI.BeginDisabledGroup(!autoRenderFrames);
        {
            frameRenderInterval = EditorGUILayout.FloatField("Frame Interval (s)", frameRenderInterval);

            if (GUILayout.Button("Start Auto Render", GUILayout.Height(30)))
            {
                frameNumber = 0;
                EditorApplication.update += AutoRenderFrames;
                isRenderingPaused = false;
            }

            if (GUILayout.Button("Pause Auto Render", GUILayout.Height(30)))
            {
                isRenderingPaused = !isRenderingPaused;
            }

            if (GUILayout.Button("Stop Auto Render", GUILayout.Height(30)))
            {
                EditorApplication.update -= AutoRenderFrames;
            }
        }
        EditorGUI.EndDisabledGroup();

        GUILayout.Space(10);

        // Button to create the folder structure
        if (GUILayout.Button("Create Folder Structure"))
        {
            CreateFolderStructure();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Create Render Texture"))
        {
            CreateRenderTexture();
        }
    }

    private void CreateFolderStructure()
    {
        // Create the "_Artist_Tool" folder
        if (!AssetDatabase.IsValidFolder(baseFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "_Artist_Tool");
        }

        // Create the "_Artist_Tool/Scene_Capture" folder
        if (!AssetDatabase.IsValidFolder(baseFolderPath + "Scene_Capture"))
        {
            AssetDatabase.CreateFolder(baseFolderPath, "Scene_Capture");
        }
    }

    private void SaveRTToFile()
    {
        if (rt == null)
        {
            Debug.LogError("No RenderTexture selected!");
            return;
        }

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes;
        bytes = tex.EncodeToPNG();

        // Create the folder structure if it doesn't exist
        if (!System.IO.Directory.Exists(baseFolderPath))
        {
            string parentFolder = System.IO.Path.GetDirectoryName(baseFolderPath);
            string folderName = System.IO.Path.GetFileName(baseFolderPath);
            System.IO.Directory.CreateDirectory(parentFolder);
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(parentFolder, folderName));
        }

        string fileName = rt.name + "_Frame_" + frameNumber + ".png";
        string fullPath = baseFolderPath + fileName;

        System.IO.File.WriteAllBytes(fullPath, bytes);

        AssetDatabase.Refresh();

        Debug.Log("Saved frame " + frameNumber + " to " + fullPath);

        frameNumber++; // Increment frame number after saving
    }

    private void AutoRenderFrames()
    {
        // ... (rest of the AutoRenderFrames method remains the same)
    }

    private void CreateRenderTexture()
    {
        // Create a new Render Texture
        RenderTexture newRenderTexture = new RenderTexture(512, 512, 24);
        newRenderTexture.name = "NewRenderTexture"; // Set the name of the Render Texture

        // Use the base folder path for saving the RenderTexture asset
        AssetDatabase.CreateAsset(newRenderTexture, baseFolderPath + newRenderTexture.name + ".renderTexture");
        AssetDatabase.SaveAssets();

        // Set the newly created Render Texture as the selected one
        rt = newRenderTexture;
    }
}
