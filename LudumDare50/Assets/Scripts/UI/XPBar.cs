using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBar : MonoBehaviour
{

    private Transform bar;
    private SpriteRenderer barRenderer;
    private float currentCooldown;
    private float currentXP;
    
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        barRenderer = bar.Find("BarSprite").gameObject.GetComponent<SpriteRenderer>();
        currentCooldown = 1f;
    }

    // public float SetCooldown(float normalizedCooldown)
    // {
    //     // currentCooldown = Mathf.Clamp(normalizedCooldown, 0f, 1f);
    //     // bar.localScale = new Vector3(currentCooldown, 1f);
    //     // barRenderer.color = new Color(currentCooldown, 0f, 1f - currentCooldown, 1f);
    //     // return currentCooldown;
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float SetXP(float normalizedXP)
    {
        currentXP = Mathf.Clamp(normalizedXP, 0f, 1f);
        bar.localScale = new Vector3(currentXP, 1f);
        return currentXP;
    }
}
