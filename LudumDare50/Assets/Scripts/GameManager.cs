using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private GameObject player;
	private Player playerScript;
	public static GameManager instance = null;

	private GameObject cameraObject;
	private AudioListener listener;

	private bool paused = false;
	private bool inCutScene = false;
	private bool victory = false;
	private bool loss = false;

	private string[] cutscenes = {"Pre-BossScene", "SecondBossScene"};

	private int nextCutsceneIndex = 0;
	private int currentCutSceneIndex;
	private AudioSource AS;
	private AudioLowPassFilter lpFilter;

	public AudioClip mainTheme;
	public AudioClip finalBossTheme;
	public AudioClip midBossTheme;

	public AudioSource oneShotAudioSource;
	public AudioClip errorPurchaseNoise;

	public AudioClip deathSound;

	public float volumeMax = 0.2f;

	private bool changeToShopMusic;

	public GameObject levelUpMenu;

	public GameObject[] powerupObjects;

	public List<Powerup> inactivePowerups = new List<Powerup>();
	public List<Powerup> activePowerups = new List<Powerup>();

	private void Awake() {
		// Load powerups
		LoadPowerups();
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		cameraObject = GameObject.FindWithTag("MainCamera");
		listener = cameraObject.GetComponent<AudioListener>();
		playerScript = player.GetComponent<Player>();
		AS = GetComponent<AudioSource>();
		lpFilter = GetComponent<AudioLowPassFilter>();

	}
	
	// Update is called once per frame
	void Update () {
		// CheckForWaveChange();
		if (player != null)
			CheckGameOver();
	}


	private void SpawnWave(int currentLevel) {
		// spawnManager.SpawnWave(currentLevel);
	}

	public void StartCutScene(int cutSceneIndex) {
		AS.volume = volumeMax;
		ResumeMainMusic();

		Debug.Log("START SCENE INDEX: " + cutSceneIndex);
		currentCutSceneIndex = cutSceneIndex;
		SceneManager.LoadScene(cutscenes[cutSceneIndex], LoadSceneMode.Additive);
		listener.enabled = false; // Disabling the main cameras audio listener so that we have exactly one listener
		PauseGame();
	}

	public void StopCutScene() {
		Debug.Log("END SCENE INDEX: " + currentCutSceneIndex);
		SceneManager.UnloadSceneAsync(cutscenes[currentCutSceneIndex]);
		listener.enabled = true;
		// SetEnemyCountToZero();
		UnPauseGame();
	}

	public void CheckGameOver() {
		if (SceneManager.GetActiveScene().name != "GameOverScreen" && !victory){
			if (playerScript.GetHealth() <= 0f && !loss){
				loss = true;
				oneShotAudioSource.PlayOneShot(deathSound);
				playerScript.DisableOnDeath();
				IEnumerator coroutine = GameOverCoRoutine(1.5f);
				StartCoroutine(coroutine);
			}
		}
	}

	// public void GoToGameOverScreen() {
	// 	SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
	// }

    private IEnumerator GameOverCoRoutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
			SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
            print("WaitAndPrint " + Time.time);
        }
    }

	public void Victory() {
		Debug.Log("YOU WIN");
		if (SceneManager.GetActiveScene().name != "VictoryScene" && !loss) {
			victory = true;
			SceneManager.LoadScene("VictoryScene", LoadSceneMode.Single);
		}

	}

	public void PauseGame() {
		paused = true;
		Time.timeScale = 0;	
	}

	public void UnPauseGame() {
		paused = false;
		Time.timeScale = 1;
	}

	public void PlayShopMusic() {
		changeToShopMusic = true;
	}

	public void ResumeMainMusic() {
		changeToShopMusic = false;
	}

	public void PlayMainMusic() {
		AS.clip = mainTheme;
		AS.Play();
	}

	public void PlayMidBossMusic() {
		AS.clip = midBossTheme;
		AS.Play();
	}

	public void PlayFinalBossMusic() {
		AS.clip = finalBossTheme;
		AS.Play();
	}

	// public void PlayErrorNoise() {
	// 	if(changeToShopMusic) {
	// 		shopTheme.PlayOneShot(errorPurchaseNoise);
	// 	} else {
	// 		AS.PlayOneShot(errorPurchaseNoise);
	// 	}
	// }

	public void enableLowPassFilter() {
		lpFilter.enabled = true;
	}

	public void disableLowPassFilter() {
		lpFilter.enabled = false;
	}

	public void LevelUp() {
		if (levelUpMenu) {
			// Generate 3 level up choices;
			Powerup[] powerupList = new Powerup[3];
			for (int i = 0; i < 3; i++) {
				int randomSelection = Random.Range(0, powerupObjects.Length - 0);
				powerupList[i] = powerupObjects[randomSelection].GetComponent<Powerup>();
			}

			levelUpMenu.SetActive(true);
			LevelUpPopup popUp = levelUpMenu.GetComponent<LevelUpPopup>();
			enableLowPassFilter();
			// Disable constant laser sounds while this pop up is here:
			GameObject[] laserObjects = GameObject.FindGameObjectsWithTag("Laser");
			foreach(GameObject laser in laserObjects) {
				AudioSource audio = laser.GetComponent<AudioSource>();
				audio.Stop();
			}
			popUp.PopUp(powerupList);
		} else {
			Debug.Log("No level up menu set in scene, cant open menu...");
		}
	}

	// public void DecreaseEnemyCount() {
	// 	if (enemyCount > 0)
	// 		enemyCount--;
	// }

	// public void DecreaseBossCount() {
	// 	if (bossCount > 0)
	// 		bossCount--;
	// }

	// //Used By cutscenes to indicate they are done
	// public void SetEnemyCountToZero() {
	// 	enemyCount = 0;
	// }
	
	//Utility Methods
	
	// -1 indicates the integer is not in the array
	private int FindInArray(int[] arr, int find) {
		for (int i = 0; i < arr.Length; i++) {
			if (find == arr[i])
				return i;
		}
		return -1;
	}

	// 1 - intro screen
	// 2 - start of gameplay
	public void Reset() {
		Debug.Log("RESET");
		victory = false;
		loss = false;
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	public void LoadScene(int sceneIndex) {
		Debug.Log("SCENE INDEX " + sceneIndex);
		if (sceneIndex == 0) {
			SceneManager.LoadScene(sceneIndex);
			Destroy(gameObject);
		}
		// else if (sceneIndex == 2) {
		// 	Reset();
		// }
		else {
			SceneManager.LoadScene(sceneIndex);
		}
	}

	public bool IsPaused () {
		return paused;
	}

	// Retrieves game objects and adds them to the inactive pool
	public void LoadPowerups() {
		foreach (GameObject g in powerupObjects) {
			Powerup powerup = g.GetComponent<Powerup>();
			if (powerup != null) {
				if (!powerup.active)	inactivePowerups.Add(powerup);
				else activePowerups.Add(powerup);
			}
		}
	}

	public void SetPowerupToActive(Powerup powerup) {
		powerup.SetPowerupActive();
		inactivePowerups.Remove(powerup);
		if(!activePowerups.Contains(powerup)) {
			activePowerups.Add(powerup);
		}
	}

	public void SetPowerupToInactive(Powerup powerup) {
		powerup.SetPowerupInActive();
		activePowerups.Remove(powerup);
		if(!inactivePowerups.Contains(powerup)) {
			inactivePowerups.Add(powerup);
		}
	}
}
