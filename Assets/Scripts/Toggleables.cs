using UnityEngine;

public class Toggleables : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material materialDefault;
    [SerializeField] private Material materialOld;
    [SerializeField] private Material materialNoLight;

    private Renderer target;

    private bool isOldActive = false;
    private bool isNoLightActive = false;

    private Material runtimeDefault;
    private Texture originalDefaultTexture;

    void Start()
    {
        target = GetComponent<Renderer>();
        if (target == null || materialDefault == null)
        {
            enabled = false;
            return;
        }

        runtimeDefault = new Material(materialDefault);
        originalDefaultTexture = runtimeDefault.mainTexture;

        target.material = runtimeDefault;
    }

    public void SetOldMaterial(bool oldActive)
    {
        isOldActive = oldActive;

        if (!isNoLightActive)
        {
            target.material = isOldActive ? materialOld : runtimeDefault;
        }
    }

    public void SetNoLight(bool noLightActive)
    {
        isNoLightActive = noLightActive;
        target.material = isNoLightActive ? materialNoLight : (isOldActive ? materialOld : runtimeDefault);
    }

    public void ToggleTexture()
    {
        if (runtimeDefault == null)
            return;

        if (isOldActive)
        {
            isOldActive = false;
            if (!isNoLightActive)
                target.material = runtimeDefault;
        }

        if (isNoLightActive)
        {
            isNoLightActive = false;
            target.material = runtimeDefault;
        }

        if (runtimeDefault.mainTexture != null)
            runtimeDefault.mainTexture = null;
        else
            runtimeDefault.mainTexture = originalDefaultTexture;

        if (!isOldActive && !isNoLightActive)
            target.material = runtimeDefault;
    }
}
