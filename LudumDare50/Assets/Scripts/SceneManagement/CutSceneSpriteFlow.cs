using UnityEngine;

using System.Collections;

public class CutSceneSpriteFlow : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public Sprite[] sprite;

	public float disableInputForSeconds = 1f;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private int sceneEnding = 0;
	private SpriteRenderer spriteRenderer;
	public UnityEngine.UI.Image gui;

	public AudioSource audioSource;
	public AudioClip jingle;

	public int levelToLoadIndex;
	public int slideToStopMusicAndJingle = 0;

	private GameManager gameManager;

	void Start ()
	{
		//Screen.SetResolution (1400, 900, true);

		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		// GetComponent<UnityEngine.UI.Image>().rectTransform = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	
	void Update ()
	{
		disableInputForSeconds -= 0.01f;
		foreach (Touch touch in Input.touches) {
			sceneEnding++;
		}

		if (Input.anyKeyDown && disableInputForSeconds <= 0f) {//.GetKeyDown(KeyCode.RightArrow)) {
			if(sceneEnding < sprite.Length){
				sceneEnding++;
				//Stop music and play jingle;
				if (sceneEnding == slideToStopMusicAndJingle && audioSource != null) {
					Debug.Log ("JINGLE");
					audioSource.Stop();
					audioSource.PlayOneShot (jingle);
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
		if (sceneEnding == sprite.Length) {
			EndScene ();
		} else {
			spriteRenderer.sprite = sprite[sceneEnding];
		}

	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		if(Time.deltaTime < 0.1f) {
			GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
		} else {
			GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.black, fadeSpeed * Time.deltaTime);
		}
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		float prevAlpha = GetComponent<UnityEngine.UI.Image>().color.a;

		GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.black, fadeSpeed * Time.deltaTime);

		float currAlpha = GetComponent<UnityEngine.UI.Image>().color.a;

		if(currAlpha - prevAlpha < 0.05f) {
			Color mask = GetComponent<UnityEngine.UI.Image>().color;
			GetComponent<UnityEngine.UI.Image>().color = new Color(mask.r, mask.g, mask.b, mask.a +0.05f);
		}
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(GetComponent<UnityEngine.UI.Image>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<UnityEngine.UI.Image>().color = Color.clear;
			GetComponent<UnityEngine.UI.Image>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		GetComponent<UnityEngine.UI.Image>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(GetComponent<UnityEngine.UI.Image>().color.a >= 0.95f) {
			// ... reload the level.
			//Application.LoadLevel(levelToLoad);
			//LoadingScreenManager.LoadScene(levelToLoadIndex);
			gameManager.StopCutScene();
		}
	}
}