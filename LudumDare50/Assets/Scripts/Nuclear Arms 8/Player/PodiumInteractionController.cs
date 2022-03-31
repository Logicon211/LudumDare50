using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumInteractionController : MonoBehaviour
{

    public float interactRange = 3f;
    public LayerMask layerMask = new LayerMask();

    // private PodiumController lookingAt;
    private GameObject lookingAtGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) /*&& lookingAt*/)
        {
            InteractWithPodium();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Can open door?: " + GameState.CanOpenDoor());
        }
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * interactRange;
        Debug.DrawRay(transform.position, forward, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, forward, out hit, interactRange, layerMask))
        {
            // if (!lookingAt)
            // {
            //     lookingAtGameObject = hit.transform.gameObject;
            //     lookingAt = lookingAtGameObject.GetComponent<PodiumController>();
            //     lookingAt.ShowFloatingText();
            // }
        }
        else
        {
            // if (lookingAt)
            // {
            //     lookingAt.HideFloatingText();
            //     lookingAt = null;
            //     lookingAtGameObject = null;
            // }
        }
    }

    void InteractWithPodium()
    {
        // Will only open door if game state allows it
        // lookingAt.OpenDoor();
        // lookingAt = null;
        // lookingAtGameObject = null;
    }
}
