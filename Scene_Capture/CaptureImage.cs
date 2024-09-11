using UnityEngine;
using UnityEngine.UI;
using System.IO;  // For handling file paths

public class CaptureImage : MonoBehaviour
{
    public Camera captureCamera;
    public RenderTexture renderTexture;
    public Button saveButton;

    void Start()
    {
        // Assign the Button's onClick event
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(CaptureAndSaveImage);
        }
    }

    public void CaptureAndSaveImage()
    {
        // Set the render texture
        captureCamera.targetTexture = renderTexture;
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        captureCamera.Render();

        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();

        // Ensure the directory exists
        string screenshotsDirectory = Path.Combine(Application.dataPath, "Screenshots");
        if (!Directory.Exists(screenshotsDirectory))
        {
            Directory.CreateDirectory(screenshotsDirectory);
        }

        // Create a unique filename
        string fileName = Path.Combine(screenshotsDirectory, $"Screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png");

        // Convert to PNG
        byte[] byteArray = screenshot.EncodeToPNG();
        File.WriteAllBytes(fileName, byteArray);

        Debug.Log($"Image Saved to {fileName}!");

        // Clean up
        RenderTexture.active = null;
        captureCamera.targetTexture = null;
        
        // Refresh the AssetDatabase if you're in the Editor
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    void OnDestroy()
    {
        // Clean up the event listener when the script is destroyed
        if (saveButton != null)
        {
            saveButton.onClick.RemoveListener(CaptureAndSaveImage);
        }
    }
}
