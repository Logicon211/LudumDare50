using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private GameManager gameManager;
    private bool isPaused = false;

	void Start() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused = !isPaused;
            if(isPaused) {
                Time.timeScale = 0;
            } else {
                if(!gameManager.isInLevelUpScreen) {
                    Time.timeScale = 1;
                }
            }
        }

        if(isPaused) {
            pauseMenu.SetActive(true);
        } else {
            pauseMenu.SetActive(false);
        }
    }

    public void ExitGame() {
        Debug.Log("Hitting exit game button");
        Application.Quit();
    }


// #if !MOBILE_INPUT
// 	void Update()
// 	{
// 		if(Input.GetKeyUp(KeyCode.Escape))
// 		{
// 		    m_MenuToggle.isOn = !m_MenuToggle.isOn;
//             Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
// 		}
// 	}
// #endif

}
