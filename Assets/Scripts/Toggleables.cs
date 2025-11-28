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

    // runtime instance of default material to safely modify textures
    private Material runtimeDefault;
    private Texture originalDefaultTexture;

    void Start()
    {
        target = GetComponent<Renderer>();
        if (target == null || materialDefault == null)
        {
            Debug.LogWarning($"{name} missing Renderer or Default Material.");
            enabled = false;
            return;
        }

        // Create runtime instance of default
        runtimeDefault = new Material(materialDefault);
        originalDefaultTexture = runtimeDefault.mainTexture;

        // Start with default material
        target.material = runtimeDefault;
    }

    public void SetOldMaterial(bool oldActive)
    {
        isOldActive = oldActive;

        // Only apply if no-light is NOT active
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

        // If old material is active, switch back to default first
        if (isOldActive)
        {
            isOldActive = false;
            if (!isNoLightActive)
                target.material = runtimeDefault;
        }

        // If no-light is active, disable it first
        if (isNoLightActive)
        {
            isNoLightActive = false;
            target.material = runtimeDefault;
        }

        // Toggle texture on runtime default material
        if (runtimeDefault.mainTexture != null)
            runtimeDefault.mainTexture = null;
        else
            runtimeDefault.mainTexture = originalDefaultTexture;

        // Ensure target shows runtime default if neither old nor no-light is active
        if (!isOldActive && !isNoLightActive)
            target.material = runtimeDefault;
    }
}
