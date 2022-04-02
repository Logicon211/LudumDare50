using System;
using UnityEngine;
using TMPro;

public class LevelUpPopup : MonoBehaviour
{

	public LevelUpChoice choice1;
	public LevelUpChoice choice2;
	public LevelUpChoice choice3;
	public void PopUp () {
		// TODO: Generate the new level up list
		choice1.setTitle("TITLE FOR ONE");
		choice2.setTitle("TITLE FOR TWO");
		choice3.setTitle("title for three how long can I go");

		choice1.setContent("CONTENT FOR ONE");
		choice2.setContent("The content on the second one");
		choice3.setContent("How much text can I put in the third one and it not go crazy how much longer can I go oh my god I hope I don't need this much text");

		choice1.setIcon();
		choice2.setIcon();
		choice3.setIcon();

		Time.timeScale = 0f;
	}

	public void ClosePopUp() {
		gameObject.SetActive(false);
	}

}
