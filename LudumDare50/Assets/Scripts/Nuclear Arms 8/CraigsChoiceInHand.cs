using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraigsChoiceInHand : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public GameObject particleSystemObj;

    private Light lightSource;
    

    public bool hide = true;

    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
        meshRenderer = GetComponent<MeshRenderer>();
        InitializeColor(2);
    }

    // Update is called once per frame
    void Update()
    {
        RotateAnimation();
    }

    void RotateAnimation()
    {
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
    }

    public void InitializeColor(int choice)
    {

        if (choice == 2) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            if (lightSource != null) {
                lightSource.color = Color.green;
            }
        } else if (choice == 1) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            if (lightSource != null) {
                lightSource.color = Color.yellow;
            }
        } else if(choice == 0) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            if (lightSource != null) {
                lightSource.color = Color.red;
            }
        }
          
    }

    public void EnableRenderer() {
        meshRenderer.enabled = true;
        lightSource.enabled = true;
        particleSystemObj.SetActive(true);
    }

    public void DisableRenderer() {
        meshRenderer.enabled = false;
        lightSource.enabled = false;
        particleSystemObj.SetActive(false);
    }
}
