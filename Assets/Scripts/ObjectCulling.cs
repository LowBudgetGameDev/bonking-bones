using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectCulling : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private ShadowCaster2D shadowCaster; // Shadow caster cannot be plugged in in editor because it is added on runtime
    
    private float renderRange = 50f;
    private float fastCheckRange = 100f;

    private float checkTimer;
    private float fastCheckTimerMax = 0.02f;
    private float slowCheckTimerMax = 0.2f;

    private bool slowCheckTimerEnabled = false;

    private void Awake()
    {
        shadowCaster = spriteRenderer.GetComponent<ShadowCaster2D>();
    }

    private void Update()
    {
        checkTimer -= Time.deltaTime;

        if (checkTimer < 0f)
        {
            Check();

            checkTimer = slowCheckTimerEnabled ? slowCheckTimerMax : fastCheckTimerMax; 
        }
    }

    private void Check()
    {
        Vector3 cameraPosition = UtilsClass.GetMainCamera().transform.position;
        cameraPosition.z = 0f;

        float distance = (cameraPosition - transform.position).magnitude;

        spriteRenderer.enabled = distance < renderRange;
        shadowCaster.enabled = distance < renderRange;
        slowCheckTimerEnabled = distance > fastCheckRange;
    }
}
