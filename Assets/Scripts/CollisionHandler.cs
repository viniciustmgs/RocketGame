using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Finish":
                LoadNextLevel();
                break;
            case "Respawn":
                break;    
            default:
                ReloadLevel();
                Debug.Log("You Lost");
                break;    
        }
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
}
