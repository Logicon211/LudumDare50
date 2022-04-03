using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public TMP_Text timerTxt;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn) {
            if(TimeLeft > 0) {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else {
                Debug.Log("TIMES UP");
                TimerOn = false;
                // TODO: Something when time's up
                gameManager.TimesUp();
            }
        }
    }

    void updateTimer(float currentTime) {
        currentTime+=1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
