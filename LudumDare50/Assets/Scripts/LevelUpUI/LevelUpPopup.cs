using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpPopup : MonoBehaviour
{

	public LevelUpChoice choice1;
	public LevelUpChoice choice2;
	public LevelUpChoice choice3;

	private GameManager gameManager;
	void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	public void PopUp (List<Powerup> powerupList) {
		if (powerupList[0]) {
			choice1.setPowerUp(powerupList[0]);
			choice1.setTitle(powerupList[0].getPowerupName());
			choice1.setContent(powerupList[0].GetPowerupLevelDescription(powerupList[0].GetLevel()+1));
			choice1.setIcon(powerupList[0].getPowerupIcon());
		}

		if (powerupList[1]) {
			choice2.setPowerUp(powerupList[1]);
			choice2.setTitle(powerupList[1].getPowerupName());
			choice2.setContent(powerupList[1].GetPowerupLevelDescription(powerupList[1].GetLevel()+1));
			choice2.setIcon(powerupList[1].getPowerupIcon());
		}

		if (powerupList[2]) {
			choice3.setPowerUp(powerupList[2]);
			choice3.setTitle(powerupList[2].getPowerupName());
			choice3.setContent(powerupList[2].GetPowerupLevelDescription(powerupList[2].GetLevel()+1));
			choice3.setIcon(powerupList[2].getPowerupIcon());
		}

		Time.timeScale = 0f;
	}

	public void ClosePopUp() {
		gameObject.SetActive(false);
		gameManager.disableLowPassFilter();
	}

}
