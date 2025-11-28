using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    [Header("Map Objects")]
    [SerializeField] private GameObject mapNew;
    [SerializeField] private GameObject mapOld;

    private bool newMapActive = true;
    private bool noLightActive = false;

    private Toggleables[] toggleables;

    void Start()
    {
        // Safe checks
        if (mapNew != null) mapNew.SetActive(true);
        if (mapOld != null) mapOld.SetActive(false);

        toggleables = FindObjectsOfType<Toggleables>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ToggleWorld();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ToggleLights();

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (var t in toggleables)
            {
                if (t != null)
                    t.ToggleTexture();
            }
        }
    }

    private void ToggleWorld()
    {
        newMapActive = !newMapActive;

        if (mapNew != null) mapNew.SetActive(newMapActive);
        if (mapOld != null) mapOld.SetActive(!newMapActive);

        foreach (var t in toggleables)
            if (t != null)
                t.SetOldMaterial(!newMapActive);
    }

    private void ToggleLights()
    {
        bool turningOn = !noLightActive;

        newMapActive = true;
        if (mapNew != null) mapNew.SetActive(true);
        if (mapOld != null) mapOld.SetActive(false);

        foreach (var t in toggleables)
        {
            if (t == null) continue;

            t.SetOldMaterial(false);
            t.SetNoLight(false);
        }

        noLightActive = turningOn;

        foreach (var t in toggleables)
            if (t != null)
                t.SetNoLight(noLightActive);
    }
}