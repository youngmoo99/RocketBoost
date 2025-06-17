using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{   

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true; // 충돌 가능 여부

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame) //키보드 L키를 누르면
        {
            ReloadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable; // c키를 누를 때 마다 충돌을 끄고 켜기 가능
        }
    }

    void OnCollisionEnter(Collision other)
    {   
        if(!isControllable || !isCollidable) { return; }  

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
        isControllable = false; //다른 충돌 사운드 멈추기
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX); //폭발 사운드
        crashParticles.Play(); //충돌 파티클 
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
        isControllable = false; //다른 충돌 사운드 멈추기
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX); //성공 사운드
        successParticles.Play(); //성공 파티클
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
