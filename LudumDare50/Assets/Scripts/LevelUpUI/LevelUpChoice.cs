using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpChoice : MonoBehaviour
{

    public TMP_Text title;
    public TMP_Text content;

    public LevelUpPopup levelUpPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseLevelUp() {
        // TODO: Put some logic here on level up selection
        Debug.Log("Chose level up title: " + title.text);
        Time.timeScale = 1f;
        levelUpPopup.ClosePopUp();
    }

    public TMP_Text getTitle() {
        return title;
    }

    public TMP_Text getContent() {
        return content;
    }
}
