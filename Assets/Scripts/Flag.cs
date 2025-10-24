using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Transform flag;
    [SerializeField] private Transform ball;
    [SerializeField] private float height = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 6f;
    
    private Vector3 initialPos;
    private Vector3 targetPos;
    private bool isRaised = false;

    void Start()
    {
        if (flag == null)
            flag = transform;
        
        initialPos = flag.position;
        targetPos = initialPos + Vector3.up * height;
    }

    void Update()
    {
        if (ball == null) return;
        
        float distance = Vector3.Distance(ball.position, flag.position);
        bool isInRange = distance <= range;
        
        Vector3 desiredPos = isInRange ? targetPos : initialPos;
        flag.position = Vector3.Lerp(flag.position, desiredPos, speed * Time.deltaTime);
    }
}
