using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private BallMovement ballMovement;
    [SerializeField] private Slider slider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballMovement == null || slider == null) return;

        float normalizedPower = ballMovement.currentPower / ballMovement.maxPower;
        slider.value = normalizedPower;
    }   
}
