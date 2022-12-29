using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    float reloadDelay = 1f, finishDelay = 2f;
    Boolean levelIsFinished = false;
    AudioSource rocketThrustSound, rocketRotationSound, successSound, crashSound;

    void Start(){
        rocketThrustSound = GameObject.Find("ThrustSound").GetComponentInChildren<AudioSource>();
        rocketRotationSound = GameObject.Find("RotationSound").GetComponentInChildren<AudioSource>();
        successSound = GameObject.Find("SuccessSound").GetComponentInChildren<AudioSource>();
        crashSound = GameObject.Find("CrashSound").GetComponentInChildren<AudioSource>();
    }
    void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Finish":
                SuccessSequence();
                break;
            case "Respawn":
                break;    
            default:
                if(!levelIsFinished) CrashSequence();
                break;    
        }
    }

    void SuccessSequence()
    {   
        levelIsFinished = true;
        successSound.Play();
        GetComponent<Movement>().enabled = false;
        StopSound();
        Invoke("LoadNextLevel", finishDelay);
    }

    void CrashSequence(){
        if(!levelIsFinished) crashSound.Play();
        levelIsFinished = true;
        GetComponent<Movement>().enabled = false;
        StopSound();
        Invoke("ReloadLevel", reloadDelay);
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(0);
        }
        else{
            SceneManager.LoadScene(nextSceneIndex);
        }  
    }

    void StopSound(){
        rocketThrustSound.volume = 0;
        rocketRotationSound.volume = 0;
    }
}
