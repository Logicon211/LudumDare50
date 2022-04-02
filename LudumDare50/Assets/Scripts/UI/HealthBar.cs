using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private Transform bar;
    private SpriteRenderer barRenderer;
    private float currentHealth;

    private GameObject healthLossPreviewRed;
    private SpriteRenderer healthLossPreviewSpriteRendererRed;

    private GameObject healthLossPreviewGreen;
    private SpriteRenderer healthLossPreviewSpriteRendererGreen;
    
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        barRenderer = bar.Find("BarSprite").gameObject.GetComponent<SpriteRenderer>();
        
        healthLossPreviewRed = transform.Find("HealthLossPreviewRed").gameObject;
        healthLossPreviewSpriteRendererRed = healthLossPreviewRed.transform.Find("HealthLossPreviewRedSprite")
            .gameObject.GetComponent<SpriteRenderer>();
        
        healthLossPreviewGreen = transform.Find("HealthLossPreviewGreen").gameObject;
        healthLossPreviewSpriteRendererGreen = healthLossPreviewGreen.transform.Find("HealthLossPreviewGreenSprite")
            .gameObject.GetComponent<SpriteRenderer>();
        
        HideHealthLossPreview();
        currentHealth = 1f;
    
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // Increase health by a percentage and then return current health
    public float IncreaseHealth(float normalizedHealth)
    {
        currentHealth = Mathf.Min(1f, currentHealth + normalizedHealth);
        bar.localScale = new Vector3(currentHealth, 1f);
        return currentHealth;
    }

    // Decrease health by a percentage and then return current health
    public float DecreaseHealth(float normalizedHealth)
    {
        currentHealth = Mathf.Max(0f, currentHealth - normalizedHealth);
        bar.localScale = new Vector3(currentHealth, 1f);
        return currentHealth;
    }

    // Preview how much health will be lost if the power up is purchased.
    public void ShowHealthLossPreview(float normalizedHealth)
    {
        float healthToLose = Mathf.Min(normalizedHealth, currentHealth);
        healthLossPreviewRed.transform.localScale = new Vector3(currentHealth, 1f);
        healthLossPreviewGreen.transform.localScale = new Vector3(currentHealth - healthToLose, 1f);
        healthLossPreviewSpriteRendererRed.enabled = true;
        healthLossPreviewSpriteRendererGreen.enabled = true;
        barRenderer.enabled = false;
    }

    public void HideHealthLossPreview()
    {
        healthLossPreviewSpriteRendererRed.enabled = false;
        healthLossPreviewSpriteRendererGreen.enabled = false;
        barRenderer.enabled = true;
    }

    // Set health to a percentage and then return current health
    public float SetHealth(float normalizedHealth)
    {
        currentHealth = Mathf.Clamp(normalizedHealth, 0f, 1f);
        bar.localScale = new Vector3(currentHealth, 1f);
        return currentHealth;
    }

}
