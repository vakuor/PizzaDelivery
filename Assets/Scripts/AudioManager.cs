using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton Init  
    public static AudioManager instance = null;
    private void Awake() {
        if (instance == null) {
	    instance = this;
	} else if(instance != this){
	    Destroy(gameObject);
        return;
	}
	    //DontDestroyOnLoad(gameObject);
	    InitializeManager();
    }
    private void InitializeManager(){ 
        /* TODO: */
     }
    #endregion

    private AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(0.5f);
    }
}
