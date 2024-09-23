using UnityEngine;
using UnityEditor;

public class CreateMaterialsFromTextures : MonoBehaviour
{
    [MenuItem("Assets/Create Materials From Textures", false, 10)]
    public static void CreateMaterials()
    {
        // Get all selected objects
        Object[] selectedObjects = Selection.objects;

        // Check if there are any selected objects
        if (selectedObjects.Length == 0)
        {
            Debug.LogError("No textures selected. Please select at least one texture.");
            return;
        }

        foreach (Object obj in selectedObjects)
        {
            // Check if the object is a texture
            Texture2D texture = obj as Texture2D;
            if (texture == null)
            {
                Debug.LogWarning($"Selected object '{obj.name}' is not a texture. Skipping...");
                continue;
            }

            // Find the custom shader "FlatKit/Stylized Surface"
            Shader customShader = Shader.Find("FlatKit/Stylized Surface");
            if (customShader == null)
            {
                Debug.LogError("Shader 'FlatKit/Stylized Surface' not found. Please ensure it is imported.");
                return;
            }

            // Create a new material with the custom shader
            Material newMaterial = new Material(customShader);

            // Assign the texture to the material's main texture
            newMaterial.mainTexture = texture;

            // Get the path of the selected texture
            string path = AssetDatabase.GetAssetPath(texture);

            // Create a path for the new material
            string materialPath = path.Replace(".png", ".mat"); // Assuming the texture is a PNG, adapt if necessary
            materialPath = materialPath.Replace(".jpg", ".mat"); // Handle JPG as well

            // Save the material to the same folder as the texture
            AssetDatabase.CreateAsset(newMaterial, materialPath);

            // Rename the material to match the texture's name
            newMaterial.name = texture.name;

            Debug.Log($"Material created with shader 'FlatKit/Stylized Surface' for texture: {texture.name}");
        }

        // Save the assets
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    // Enable the menu item only if at least one texture is selected
    [MenuItem("Assets/Create Materials From Textures", true)]
    public static bool ValidateCreateMaterials()
    {
        // Only enable if multiple textures are selected
        return Selection.objects.Length > 0 && Selection.objects[0] is Texture2D;
    }
}
