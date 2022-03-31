using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class IntroSpriteFlow : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public Sprite[] slides;
	public AudioClip[] voiceLines;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private int slideIndex = 0;
	private SpriteRenderer spriteRenderer;

	public AudioSource audioSource;
	public AudioClip jingle;

	public AudioSource voiceAudioSource;

	public bool justLoadNextSceneIndex = false;
	public int levelToLoadIndex;
	public int slideToStopMusicAndJingle = 0;

	private GameManager gameManager;

	public GameObject blackOutSquare;

	public bool choiceSlidesEnabled;

	// Debug variables
	public bool forceChoice = false;
	public int forceChoiceInt = 0;

	private Sprite[][] choiceSpriteArray = new Sprite[3][];
	private AudioClip[][] choiceVoiceArray  = new AudioClip[3][];

	public Sprite[] spriteChoice0;
	public Sprite[] spriteChoice1;
	public Sprite[] spriteChoice2;

	public AudioClip[] voiceChoice0;
	public AudioClip[] voiceChoice1;
	public AudioClip[] voiceChoice2;

	// private bool runningThroughChoiceSlide = false;
	private int currentChoice;
	
	//Normally we go back and check the last scene, but for the final ending choice we need to be able to go back more 
	public int lastChoiceOffset = 1;

	public float initialInputLock = 0.5f;

	public bool isFirstSceneReset = false;
	void Start ()
	{
		if (isFirstSceneReset) {
			GameState.ResetAllChoices();
		}

		GameState.CurrentScene = SceneManager.GetActiveScene().buildIndex;
		//Screen.SetResolution (1400, 900, true);
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (GameObject.FindWithTag("GameController") != null)
			gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

		if (choiceSlidesEnabled) {
			choiceSpriteArray[0] = spriteChoice0;
			choiceSpriteArray[1] = spriteChoice1;
			choiceSpriteArray[2] = spriteChoice2;
			choiceVoiceArray[0] = voiceChoice0;
			choiceVoiceArray[1] = voiceChoice1;
			choiceVoiceArray[2] = voiceChoice2;

			if (!forceChoice) {
				currentChoice = GameState.GetChoice(GameState.CurrentScene - lastChoiceOffset);
			} else {
				currentChoice = forceChoiceInt;
			}

			// runningThroughChoiceSlide = true;
			spriteRenderer.sprite = choiceSpriteArray[currentChoice][slideIndex];
			if(choiceVoiceArray[currentChoice].Length > slideIndex && choiceVoiceArray[currentChoice][slideIndex] != null) {
				voiceAudioSource.PlayOneShot(choiceVoiceArray[currentChoice][slideIndex]);
			}
		} else {
			if(voiceLines.Length > slideIndex && voiceLines[slideIndex] != null) {
				voiceAudioSource.PlayOneShot(voiceLines[slideIndex]);
			}
		}
		

	}

	void Awake ()
	{
		blackOutSquare.SetActive(true);
	}
	
	
	void Update ()
	{
		initialInputLock -= Time.deltaTime;

		foreach (Touch touch in Input.touches) {
			slideIndex++;
		}

		if (Input.anyKeyDown && initialInputLock <= 0) {//.GetKeyDown(KeyCode.RightArrow)) {
			voiceAudioSource.Stop();
			if(choiceSlidesEnabled) {
				slideIndex++;
				if(slideIndex < choiceSpriteArray[currentChoice].Length){
					if(choiceVoiceArray[currentChoice].Length > slideIndex && choiceVoiceArray[currentChoice][slideIndex] != null) {
						voiceAudioSource.PlayOneShot(choiceVoiceArray[currentChoice][slideIndex]);
					}
				}
				if(slideIndex >= choiceSpriteArray[currentChoice].Length) {
					choiceSlidesEnabled = false;
					slideIndex = 0;
					if(voiceLines.Length > slideIndex && voiceLines[slideIndex] != null) {
						voiceAudioSource.PlayOneShot(voiceLines[slideIndex]);
					}
				}
			} else {
				if(slideIndex < slides.Length){
					slideIndex++;
					//Stop music and play jingle;
					if (slideIndex == slideToStopMusicAndJingle && audioSource != null) {
						GameObject[] objs = GameObject.FindGameObjectsWithTag("GlobalMusic");
						foreach(var music in objs) {
							Destroy(music);
						}
						Debug.Log ("JINGLE");
						audioSource.Stop();
						audioSource.PlayOneShot (jingle);
					}

					if(voiceLines.Length > slideIndex && voiceLines[slideIndex] != null) {
						voiceAudioSource.PlayOneShot(voiceLines[slideIndex]);
					}
				}
			}
			//Application.LoadLevel();
		}

		// If the scene is starting...
		if (sceneStarting) {
			// ... call the StartScene function.
			StartScene ();
		}

		//Sprite image navigation
		if (choiceSlidesEnabled) {
			spriteRenderer.sprite = choiceSpriteArray[currentChoice][slideIndex];
		} else {
			if (slideIndex == slides.Length) {
				EndScene ();
			} else {
				spriteRenderer.sprite = slides[slideIndex];
			}
		}

	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		if(Time.deltaTime < 0.1f) {
			blackOutSquare.GetComponent<Image>().color = Color.Lerp(blackOutSquare.GetComponent<Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
			// GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
		} else {
			blackOutSquare.GetComponent<Image>().color = Color.Lerp(blackOutSquare.GetComponent<Image>().color, Color.black, fadeSpeed * Time.deltaTime);
			// GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.black, fadeSpeed * Time.deltaTime);
		}
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		float prevAlpha = blackOutSquare.GetComponent<Image>().color.a;

		blackOutSquare.GetComponent<Image>().color = Color.Lerp(blackOutSquare.GetComponent<Image>().color, Color.black, fadeSpeed * Time.deltaTime);

		float currAlpha = blackOutSquare.GetComponent<Image>().color.a;

		if(currAlpha - prevAlpha < 0.05f) {
			Color mask = blackOutSquare.GetComponent<Image>().color;
			blackOutSquare.GetComponent<Image>().color = new Color(mask.r, mask.g, mask.b, mask.a +0.05f);
		}
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(blackOutSquare.GetComponent<Image>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			blackOutSquare.GetComponent<Image>().color = Color.clear;
			blackOutSquare.GetComponent<Image>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}

		// TODO: check and play audio from first slide

	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		blackOutSquare.GetComponent<Image>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(blackOutSquare.GetComponent<Image>().color.a >= 0.95f) {
			// ... reload the level.
			//Application.LoadLevel(levelToLoad);
			// if (gameManager != null)
			// 	gameManager.LoadScene(levelToLoadIndex);
			// else
			if(justLoadNextSceneIndex) {
				LoadingScreenManager.LoadScene(GameState.CurrentScene + 1);
			} else {
				LoadingScreenManager.LoadScene(levelToLoadIndex);
			}
			
		}
	}
}