using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour
{
    [SerializeField] private float powerMultiplier = 1.5f;
    public float maxPower = 20f;
    public float currentPower = 0f;

    [SerializeField] private Camera cam;
    private Rigidbody rb;
    
    private bool isCharging;
    private Vector3 mouseStartPos;
    private bool isMoving = false;

    [SerializeField] private float maxLineDistance = 2f;
    private LineRenderer lineRenderer;
    
    [SerializeField] private GameManager gameManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cam == null)
            cam = Camera.main;
        
        // Set up line renderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Can't shoot when moving
        if (isMoving) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
            isCharging = true;
            lineRenderer.enabled = true;
        }
        
        if (Input.GetMouseButton(0) && isCharging)
        {
            Vector3 dragVector = Input.mousePosition - mouseStartPos;
            currentPower = Mathf.Clamp(dragVector.magnitude / 100f * powerMultiplier, 0f, maxPower);
            UpdateAimLine();
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            ShootBall();
            isCharging = false;
            lineRenderer.enabled = false;
        }
    }
    
    private void UpdateAimLine()
    {
        if (cam == null) return;

        // Use camera direction
        Vector3 shootDir = cam.transform.forward;
        shootDir.y = 0;
        shootDir.Normalize();

        // Scale line length based on current power ratio
        float powerRatio = currentPower / maxPower;
        float lineLength = maxLineDistance * powerRatio;

        // Color from yellow → red based on power
        Color lineColor = Color.Lerp(Color.yellow, Color.red, powerRatio);
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        // Draw line
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + shootDir * lineLength);
    }

    private void ShootBall()
    {
        if (gameManager != null)
            gameManager.AddStroke();
        
        Vector3 shootDir = cam.transform.forward;
        shootDir.y = 0;
        shootDir.Normalize();
        
        rb.AddForce(shootDir * currentPower, ForceMode.Impulse);
        currentPower = 0f;

        StartCoroutine(CheckIfBallStopped());
    }

    private IEnumerator CheckIfBallStopped()
    {
        isMoving = true;

        // Wait until the ball slows below the threshold
        while (rb.linearVelocity.magnitude > 0.05f)
            yield return null;

        
        // Then wait a little extra time to make sure it really stopped
        float stillTime = 0f;
        const float requiredStillDuration = 0.5f; // half a second of stillness

        while (stillTime < requiredStillDuration)
        {
            if (rb.linearVelocity.magnitude > 0.05f)
            {
                stillTime = 0f; // reset timer if it starts moving again
            }
            else
            {
                stillTime += Time.deltaTime;
            }
            yield return null;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isMoving = false;
    }
}
