using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform[] checkpoints; // Array of checkpoints
    public float speed = 5f;        // Speed of the camera movement
    public float rotationSpeed = 5f; // Speed of camera rotation
    private Transform targetCheckpoint; // The current target checkpoint

    void Update()
    {
        // Check if we have a target checkpoint
        if (targetCheckpoint == null) return;

        // Move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetCheckpoint.position, speed * Time.deltaTime);

        // Rotate the camera towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetCheckpoint.rotation, rotationSpeed * Time.deltaTime);
        
        // Check if the camera is close enough to the checkpoint to stop moving
        if (Vector3.Distance(transform.position, targetCheckpoint.position) < 0.1f)
        {
            targetCheckpoint = null; // Stop moving after reaching the target
        }
    }

    // This function is called by the button to move to the respective checkpoint
    public void MoveToCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex >= 0 && checkpointIndex < checkpoints.Length)
        {
            targetCheckpoint = checkpoints[checkpointIndex];
        }
    }
}
