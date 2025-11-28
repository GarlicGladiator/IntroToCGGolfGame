using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColourGradeToggle : MonoBehaviour
{
    public Volume volume;      
    public Texture lutA;           
    public Texture lutB;                
    public Texture lutC;               

    ColorLookup colorLookup;

    void Start()
    {
        volume.profile.TryGet(out colorLookup);
    }

    public void SetLUT(Texture newLUT)
    {
        if (colorLookup != null)
        {
            colorLookup.texture.Override(newLUT);
            colorLookup.contribution.Override(1f);   
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4)) //4
            SetLUT(lutA);

        if (Input.GetKeyDown(KeyCode.Alpha5)) //5
            SetLUT(lutB);

        if (Input.GetKeyDown(KeyCode.Alpha6)) //6
            SetLUT(lutC);
    }
}
