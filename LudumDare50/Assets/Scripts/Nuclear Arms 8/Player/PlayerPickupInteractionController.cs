using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupInteractionController : MonoBehaviour
{

    public float interactRange = 3f;
    public LayerMask layerMask = new LayerMask();

    public GameObject dialogPickupPrefab;

    private DialogOrbPickupController lookingAt;
    private GameObject lookingAtGameObject;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lookingAt)
        {
            PickupDialogOption();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(GameState.GetChoice(GameState.CurrentScene));
        }
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * interactRange;
        Debug.DrawRay(transform.position, forward, Color.yellow);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, forward, out hit, interactRange, layerMask))
        {
            if (!lookingAt)
            {
                lookingAtGameObject = hit.transform.gameObject;
                lookingAt = lookingAtGameObject.GetComponent<DialogOrbPickupController>();
                lookingAt.ShowFloatingText();
            }
        }
        else
        {
            if (lookingAt)
            {
                lookingAt.HideFloatingText();
                lookingAt = null;
                lookingAtGameObject = null;
            }
        }
    }

    void PickupDialogOption()
    {
        int currentChoice = GameState.GetChoice(GameState.CurrentScene);
        lookingAt.PickupDialogChoice();
        Destroy(lookingAtGameObject);
        lookingAt = null;
        lookingAtGameObject = null;
        //TODO: Tiny explosion effect?

        if (currentChoice != 3)
        {
            GameObject oldChoice = Instantiate(dialogPickupPrefab, transform.parent.position + (transform.parent.forward * 3f), Quaternion.identity);
            oldChoice.GetComponent<DialogOrbPickupController>().SetChoiceType(DialogOrbPickupController.GetChoiceTypeFromIndex(currentChoice));
        }
    }
}
