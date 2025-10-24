using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    [Header("Camera Settings")]
    [SerializeField] private float height = 3f;
    [SerializeField] private float distance = 8f;
    
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float zoomSpeed = 8f;
    
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float smoothSpeed = 5f;
    
    private float currentYaw = 0f;
    
    private void LateUpdate()
    {
        if (target == null) return;

        // Scroll wheel zoom
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            distance -= scrollInput * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
            
        // Rotate around the ball using mouse input
        currentYaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // Calculate new position
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        Vector3 desiredPosition = target.position + offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        // Look at the ball
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }
}
