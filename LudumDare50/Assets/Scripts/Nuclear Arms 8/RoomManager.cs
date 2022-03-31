using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public float initialTime = 10f;
    public BackgroundSounds backgroundSounds;
    
    public AudioSource pickupAudioSource;

    private AudioSource audioSource;
    public AudioClip badPickupSound;
    public AudioClip okayPickupSound;
    public AudioClip goodPickupSound;


    private AudioLowPassFilter lowPassFilter;
    private AudioReverbFilter reverbFilter;
    // Start is called before the first frame update
    void Start()
    {
        GameState.CurrentScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        reverbFilter = GetComponent<AudioReverbFilter>();
        GameState.ResetChoice(GameState.CurrentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor() {
        // door.OpenDoor();
        if (backgroundSounds != null) {
            backgroundSounds.enabled = false;
        }
    }

    public float getInitialTime() {
        return initialTime;
    }

    public void PlayPickupSound(int pickupId) {
        // if(!audioSource.isPlaying) {
        audioSource.Stop();
        // }
        // lowPassFilter.enabled = false;
        // reverbFilter.enabled = false;
        if (pickupId == 0) {
            pickupAudioSource.PlayOneShot(badPickupSound);
        } else if(pickupId == 1) {
            pickupAudioSource.PlayOneShot(okayPickupSound);
        } else if(pickupId == 2) {
            pickupAudioSource.PlayOneShot(goodPickupSound);
        }
    }
}
