using UnityEngine;
using UnityEditor;

public class ChupHinh : EditorWindow
{
    private GameObject parentObject;
    private int currentIndex = 0;
    private RenderTexture rt;
    private string baseFolderPath = "Assets/_Artist_Tool/Scene_Capture/";

    [MenuItem("_Artist_Tool/SwapChupHinh")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ChupHinh>("SwapChupHinh");
    }

    private void OnGUI()
    {
        GUILayout.Label("Hierarchy Swap Tool", EditorStyles.boldLabel);
        parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object", parentObject, typeof(GameObject), true);

        if (parentObject != null)
        {
            if (GUILayout.Button("Show Next"))
            {
                ShowNextChild();
            }

            if (GUILayout.Button("Show Previous"))
            {
                ShowPreviousChild();
            }
        }
        else
        {
            GUILayout.Label("Please assign a Parent Object.");
        }

        GUILayout.Space(20);
        GUILayout.Label("RenderTexture Screenshot Tool", EditorStyles.boldLabel);

        rt = EditorGUILayout.ObjectField("RenderTexture", rt, typeof(RenderTexture), true) as RenderTexture;

        if (GUILayout.Button("Save Current Frame"))
        {
            if (rt != null)
            {
                SaveRTToFile();
            }
            else
            {
                Debug.LogError("RenderTexture must be assigned.");
            }
        }

        if (GUILayout.Button("Create Folder Structure"))
        {
            CreateFolderStructure();
        }
    }

    private void ShowNextChild()
    {
        if (parentObject != null)
        {
            int childCount = parentObject.transform.childCount;
            if (childCount > 0)
            {
                // Hide current child
                parentObject.transform.GetChild(currentIndex).gameObject.SetActive(false);

                // Show next child
                currentIndex = (currentIndex + 1) % childCount;
                parentObject.transform.GetChild(currentIndex).gameObject.SetActive(true);
            }
        }
    }

    private void ShowPreviousChild()
    {
        if (parentObject != null)
        {
            int childCount = parentObject.transform.childCount;
            if (childCount > 0)
            {
                // Hide current child
                parentObject.transform.GetChild(currentIndex).gameObject.SetActive(false);

                // Show previous child
                currentIndex = (currentIndex - 1 + childCount) % childCount;
                parentObject.transform.GetChild(currentIndex).gameObject.SetActive(true);
            }
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

        byte[] bytes = tex.EncodeToPNG();
        string fileName = "RenderTexture_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string fullPath = baseFolderPath + fileName;

        System.IO.File.WriteAllBytes(fullPath, bytes);
        AssetDatabase.Refresh();

        Debug.Log("Saved current frame to " + fullPath);
    }

    private void CreateFolderStructure()
    {
        if (!AssetDatabase.IsValidFolder("Assets/_Artist_Tool"))
        {
            AssetDatabase.CreateFolder("Assets", "_Artist_Tool");
        }

        if (!AssetDatabase.IsValidFolder(baseFolderPath))
        {
            AssetDatabase.CreateFolder("Assets/_Artist_Tool", "Scene_Capture");
        }
    }
}
