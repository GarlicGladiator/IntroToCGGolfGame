using UnityEngine;

public class Camera : MonoBehaviour
{
    //[SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 8f;
    [SerializeField] private float height = 3f;
    
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float smoothSpeed = 5f;
    
    private float currentYaw = 0f;
    
    private void LateUpdate()
    {
        if (target == null) return;

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
