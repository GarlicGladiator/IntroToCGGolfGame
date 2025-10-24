using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("WINNER");
            gameManager.WinGame();
        }
    }
}
