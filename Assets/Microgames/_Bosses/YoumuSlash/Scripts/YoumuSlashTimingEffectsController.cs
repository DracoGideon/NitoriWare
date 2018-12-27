﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class YoumuSlashTimingEffectsController : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private float pitchMult = 1f;
    [SerializeField]
    private float timeScaleMult = 1f;
    [SerializeField]
    private float volumeMult = 1f;
    
    bool failed = false;
    bool ended = false;
    float initialTimeScale;
    float initialVolume;
    
	void Start ()
    {
        YoumuSlashPlayerController.onFail += onFail;
        YoumuSlashTimingController.onFinalNote += onFinalNote;
        initialTimeScale = Time.timeScale;
        initialVolume = musicSource.volume;
	}

    void onFinalNote()
    {
        ended = true;
    }
	
	void onFail()
    {
        failed = true;
	}

    private void LateUpdate()
    {
        if (failed)
            musicSource.pitch = Time.timeScale * pitchMult;
        if (ended)
            Time.timeScale = timeScaleMult * initialTimeScale; 
        musicSource.volume = volumeMult * initialVolume;
    }
}
