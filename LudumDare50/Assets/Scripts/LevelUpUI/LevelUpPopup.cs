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
		choice1.getTitle().text = "TITLE FOR ONE";
		choice2.getTitle().text = "TITLE FOR TWO";
		choice3.getTitle().text = "title for three how long can I go";

		choice1.getContent().text = "CONTENT FOR ONE";
		choice2.getContent().text = "The content on the second one";
		choice3.getContent().text = "How much text can I put in the third one and it not go crazy how much longer can I go oh my god I hope I don't need this much text";

		Time.timeScale = 0f;
	}

	public void ClosePopUp() {
		gameObject.SetActive(false);
	}

}
