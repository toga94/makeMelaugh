using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float edgeThreshold = 10f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 screenBounds = new Vector2(100, 100); // Adjust according to your game world size

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector3 currentPosition = virtualCamera.transform.position;

        // Check if the mouse is in the left or right corners of the screen
        if (Input.mousePosition.x >= Screen.width - edgeThreshold )
            moveDirection.x = 1;
        else if (Input.mousePosition.x <= edgeThreshold )
            moveDirection.x = -1;

        // Keyboard input for left and right movement
        moveDirection.x += Input.GetAxis("Horizontal");

        // Normalize to ensure consistent movement speed in all directions
        moveDirection.Normalize();

        // Move the camera
        currentPosition += moveDirection * moveSpeed * Time.deltaTime;

        // Clamp camera position within bounds
        currentPosition.x = Mathf.Clamp(currentPosition.x, -screenBounds.x, screenBounds.x);

        virtualCamera.transform.position = currentPosition;
    }
}
