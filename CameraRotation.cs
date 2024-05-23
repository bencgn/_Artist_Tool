using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust this to control the speed of rotation

    private bool isRotating = false;
    private Vector3 lastMousePosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) // Check for left mouse button release
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationAmount = delta.x * rotationSpeed * Time.deltaTime;

            // Calculate the new rotation based on the initial rotation and the accumulated rotation amount
            Quaternion newRotation = Quaternion.AngleAxis(rotationAmount, Vector3.up) * initialRotation;

            // Apply the new rotation
            transform.rotation = newRotation;

            lastMousePosition = Input.mousePosition;
        }
    }
}
