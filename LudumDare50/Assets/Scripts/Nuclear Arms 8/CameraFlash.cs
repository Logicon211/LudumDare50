using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{

    Light cameraLight;
    public float flashInterval;
    public float flashLength;
    // Start is called before the first frame update

    private float currentTime;
    private bool flashing;
    public float intensityFade;
    public float initialOffset = 0;

    private float initialIntensity;

    void Start()
    {
        cameraLight = gameObject.GetComponent<Light>();
        initialIntensity = cameraLight.intensity;
        currentTime = /*flashInterval + */initialOffset;
        flashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(flashing) {
            cameraLight.intensity -= intensityFade;
            if(currentTime <= 0) {
                flashing = false;
                cameraLight.enabled = false;
                currentTime = flashInterval;
                cameraLight.intensity = initialIntensity;
            }
        } else {
            if(currentTime <= 0) {
                cameraLight.enabled = true;
                flashing = true;
                currentTime = flashLength;
            }
        }
    }

    private void FixedUpdate() {
    }
}
