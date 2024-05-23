using UnityEngine;
using UnityEditor;

public class Polycounter : Editor
{
    [MenuItem("GameObject/Count Polygons")]
    public static void CountPolygons()
    {
        GameObject selectedObject;

        Object prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);

        if (prefabRoot != null)
            selectedObject = (GameObject) prefabRoot;
        else
            selectedObject = Selection.activeGameObject;

        MeshFilter[] meshFilters = selectedObject.GetComponentsInChildren<MeshFilter>();
        int polyCount = 0;

        foreach (MeshFilter meshFilter in meshFilters)
        {
            Mesh mesh = meshFilter.sharedMesh;
            polyCount += mesh.triangles.Length / 3;
        }

        if (meshFilters.Length > 0)
        {
            Debug.Log("Object " + selectedObject.name + " contains " + meshFilters.Length
                      + " meshes with total " + polyCount + " triangles");

            // Create a text object
            GameObject textObject = new GameObject("PolygonCountText");
            TextMesh textMesh = textObject.AddComponent<TextMesh>();
            textMesh.text = "Triangles: " + polyCount;

            // Set the position, rotation, and scale of the text object to match the selected object
            textObject.transform.position = selectedObject.transform.position;
            textObject.transform.rotation = selectedObject.transform.rotation;
            textObject.transform.localScale = selectedObject.transform.localScale;
        }
        else
        {
            Debug.Log("Object " + selectedObject.name + " does not contain any mesh - keep looking");
        }
    }
}
