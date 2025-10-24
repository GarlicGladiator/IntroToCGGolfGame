using UnityEngine;
using System.Collections;

public class Hazards : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float downSpeed = 5f;
    [SerializeField] private float downTime = 1f;
    [SerializeField] private float riseSpeed = 2f;
    [SerializeField] private float offSet = 0f;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BallMovement ball;
    
    private Vector3 initialPos;
    private bool isRising = false;
    private bool downDelay = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if (ball == null)
            ball = FindObjectOfType<BallMovement>();
        
        initialPos = transform.position;
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(offSet);
        StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        while (true)
        {
            if (!downDelay)
            {
                float speed = isRising ? riseSpeed : downSpeed;
                float direction = isRising ? 1f : -1f;
                transform.position += Vector3.up * direction * speed * Time.deltaTime;
                
                if (isRising && transform.position.y >= initialPos.y + moveDistance)
                    isRising = false;
                else if (!isRising && transform.position.y <= initialPos.y - moveDistance)
                {
                    transform.position = new Vector3(transform.position.x, initialPos.y - moveDistance, transform.position.z);
                    downDelay = true;
                    yield return new WaitForSeconds(downTime);
                    downDelay = false;
                    isRising = true;
                }
                yield return null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Crushed");
            ball.ResetBall();
        }
    }
}
