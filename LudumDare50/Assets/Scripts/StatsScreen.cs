using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsScreen : MonoBehaviour
{
    private GameManager gameManager;
    public TMP_Text passiveStats;
    public GameObject[] iconAndContainers = new GameObject[5];
    public TMP_Text[] iconTexts = new TMP_Text[5];


	void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        float damagePercentage = (gameManager.GetPlayer().playerStats.damagePercentBonus - 1f) * 100f;
        float cooldownPercentage = (gameManager.GetPlayer().playerStats.cooldownPercentBonus - 1f) * 100f;

        string stats = "";
        stats += "Max Health:      " + gameManager.GetPlayer().playerStats.maxPlayerHealth + "\n";
        stats += "Armor:                " + gameManager.GetPlayer().playerStats.armor + "\n";
        stats += "Damage:          +" + damagePercentage + "%\n";
        stats += "Cooldown:      +" + cooldownPercentage + "%\n";
        stats += "Overdrives:       " + gameManager.GetPlayer().playerStats.overdrive + "\n";

        passiveStats.text = stats;
        // gameManager.GetPlayer().playerStats.maxPlayerHealth;
        for(int i = 0; i < gameManager.powerupListForUI.Count; i++) {
            iconAndContainers[i].GetComponent<Image>().sprite = gameManager.powerupListForUI[i].getPowerupIcon();
            iconTexts[i].text = gameManager.powerupListForUI[i].getPowerupName() + ": Level " + gameManager.powerupListForUI[i].Level;
            iconAndContainers[i].SetActive(true);
        }
    }
}
