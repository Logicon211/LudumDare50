using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchyScreenEffect : MonoBehaviour
{
    public ShaderEffect_BleedingColors bleedingColors;
    public ShaderEffect_CorruptedVram corruptedVram;

    public float glitchInterval = 2f;
    public float randomInterval = 1f;
    private float currentGlitchTimer;

    public float glitchDuration = 0.5f;
    private float glitchRunningTimer;

    private bool isGlitchRunning = false;
    // Start is called before the first frame update

    public Sprite mainSlide;
    public Sprite altSlide;

    public SpriteRenderer spriteRenderer;

    public AudioClip staticSound;
    private AudioSource AS;

    public bool showMainSlide = true;
    void Start()
    {
        AS = this.GetComponent<AudioSource>();
        currentGlitchTimer = glitchInterval;
        glitchRunningTimer = glitchDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(showMainSlide) {
            spriteRenderer.sprite = mainSlide;
        } else {
            spriteRenderer.sprite = altSlide;
        }

        if (currentGlitchTimer > 0f) {
            currentGlitchTimer -= Time.deltaTime;
        } else {
            currentGlitchTimer = Random.Range(glitchInterval - randomInterval, glitchInterval + randomInterval);
            isGlitchRunning = true;
            AS.PlayOneShot(staticSound);
            ToggleSlide();
        }

        if(isGlitchRunning) {
            if(glitchRunningTimer > 0f) {
                bleedingColors.enabled = true;
                corruptedVram.enabled = true;
                glitchRunningTimer -= Time.deltaTime;
            } else {
                bleedingColors.enabled = false;
                corruptedVram.enabled = false;
                glitchRunningTimer = glitchDuration;
                isGlitchRunning = false;
            }


        }
    }

    void ToggleSlide(){
        showMainSlide = !showMainSlide;
    }
}
