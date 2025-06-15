using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float levelLoadDelay = 2f;
    void OnCollisionEnter(Collision other)
    {   
        string tags = other.gameObject.tag;
        switch (tags)
        {
            case "Friendly":
                Debug.Log("Friendly!!");
                break;
            case "Fuel":
                Debug.Log("Ouch!!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {   
        GetComponent<Movement>().enabled = false; // 움직임 멈춤
        Invoke("ReloadLevel",levelLoadDelay); //2초 후에 ReloadLevel 메소드 호출
    }

    void ReloadLevel()
    {   
        int currentScene = SceneManager.GetActiveScene().buildIndex; // 현재 씬 인덱스를 가져옴
        SceneManager.LoadScene(currentScene);
    }

    void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false; //움직임 멈춤
        Invoke("ReloadNextLevel",levelLoadDelay); //2초후에 ReloadNextLevel 메소드 호출
    }
    void ReloadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; // 현재 씬 인덱스를 가져옴
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if(currentScene == sceneCount - 1) //마지막 씬이면 0번째 씬으로 이동
        {
            currentScene = 0;
        } 
        else //다음 씬 이동
        {
            currentScene++;
        }
        
        SceneManager.LoadScene(currentScene);   
    }


    

}
