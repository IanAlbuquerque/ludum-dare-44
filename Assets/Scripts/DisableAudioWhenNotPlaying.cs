﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAudioWhenNotPlaying : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.isPlayingActualLevel || GameController.Instance.isTestingCreationLevel) {
            audio.enabled = true;
        } else {
            audio.enabled = false;
        }
    }
}
