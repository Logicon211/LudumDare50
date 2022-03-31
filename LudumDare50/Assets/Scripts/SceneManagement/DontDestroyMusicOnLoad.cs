using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusicOnLoad : MonoBehaviour
{
    public bool destroyOtherMusicPlayers = false;
    void Awake() 
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("GlobalMusic");

        if (destroyOtherMusicPlayers) {
            foreach (var musicPlayer in objs) {
                if (musicPlayer != this.gameObject) {
                    Destroy(musicPlayer);
                }
            }
        } else {
            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
