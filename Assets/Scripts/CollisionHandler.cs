using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Finish":
                Debug.Log("You've reached the end");
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
        SceneManager.LoadScene(0);
    }
}
