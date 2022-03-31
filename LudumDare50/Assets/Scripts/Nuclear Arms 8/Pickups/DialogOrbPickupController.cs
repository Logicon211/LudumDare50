using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogOrbPickupController : MonoBehaviour
{

    public enum ChoiceType 
    {
        GOOD,
        OKAY,
        BAD
    }

    public ChoiceType choiceType = ChoiceType.BAD;

    public float bobDistance = 0.38f, bobSpeed = 2.99f;
    public float rotationSpeed = 50f;

    public bool animationsEnabled = true;

    // How far above powerup floating text should be
    public Vector3 floatingTextOffset = new Vector3(0f, 1f, 0f);

    public GameObject floatingTextPrefab;

    // Update to set the floating text above the powerup
    public string dialog = "Press [E] to pickup";

    private GameObject floatingText;
    private float originalY;
    private bool isTextVisible;
    private GameObject camera;

    private Light lightSource;

    public float pulseRange = 4.0f;
    public float pulseSpeed = 3.0f;
  
    public float pulseMinimum = 1.0f;
    private RoomManager roomManager;

    private CraigsChoiceInHand craigsHand;

    // Start is called before the first frame update
    void Start()
    {
        craigsHand = GameObject.Find("CraigsChoice").GetComponent<CraigsChoiceInHand>();
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        lightSource = GetComponent<Light>();
        this.originalY = transform.position.y;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        InitializeFloatingText();
        InitializeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationsEnabled)
        {
            FloatAnimation();
        }
        TextFacePlayer(camera.transform.position);

        lightSource.range = pulseMinimum + Mathf.PingPong(Time.time * pulseSpeed, pulseRange - pulseMinimum);
    }

    void FloatAnimation()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(bobSpeed * Time.time) * bobDistance),
            transform.position.z);
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
    }

    void InitializeFloatingText()
    {
        floatingText = Instantiate(floatingTextPrefab, transform.position + floatingTextOffset, Quaternion.identity, transform);
        TextMesh textMesh = floatingText.GetComponent<TextMesh>();
        textMesh.text = dialog;
        HideFloatingText();
    }

    void TextFacePlayer(Vector3 playerPos)
    {
        if (isTextVisible)
        {
            floatingText.transform.LookAt(2 * floatingText.transform.position - playerPos);
        }
        
    }

    void InitializeColor()
    {
        switch (choiceType)
        {
            case ChoiceType.GOOD:
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                if (lightSource != null) {
                    lightSource.color = Color.green;
                }
                break;
            }
            case ChoiceType.OKAY:
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                if (lightSource != null) {
                    lightSource.color = Color.yellow;
                }
                break;
            }
            case ChoiceType.BAD:
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                if (lightSource != null) {
                    lightSource.color = Color.red;
                }
                break;
            }
            default:
            {
                break;
            }
            

        }
    }

    public void PickupDialogChoice()
    {
        int currentScene = GameState.CurrentScene;
        int choice;
        switch (choiceType)
        {
            case ChoiceType.GOOD:
                {
                    choice = 2;
                    break;
                }
            case ChoiceType.OKAY:
                {
                    choice = 1;
                    break;
                }
            case ChoiceType.BAD:
                {
                    choice = 0;
                    break;
                }
            default:
                {
                    choice = 3;
                    break;
                }


        }

        GameState.UpdateChoice(currentScene, choice);
        craigsHand.InitializeColor(choice);
        craigsHand.EnableRenderer();
        roomManager.PlayPickupSound(choice);
    }

    public void ShowFloatingText()
    {
        isTextVisible = true;
        floatingText.SetActive(true);
    }

    public void HideFloatingText()
    {
        isTextVisible = false;
        floatingText.SetActive(false);
    }

    public void SetChoiceType(ChoiceType type)
    {
        choiceType = type;
        InitializeColor();
    }

    public static ChoiceType GetChoiceTypeFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                {
                    return ChoiceType.BAD;
                }
            case 1:
                {
                    return ChoiceType.OKAY;
                }
            case 2:
                {
                    return ChoiceType.GOOD;
                }
            default:
                {
                    return ChoiceType.BAD;
                }
        }
    }

}
