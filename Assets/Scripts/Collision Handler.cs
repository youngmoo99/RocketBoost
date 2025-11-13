using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f; // 충돌/성공 후 씬 전환까지의 지연
    [SerializeField] AudioClip crashSFX; // 충돌 사운드
    [SerializeField] AudioClip successSFX; // 성공 사운드
    [SerializeField] ParticleSystem successParticles; // 성공 파티클
    [SerializeField] ParticleSystem crashParticles; // 충돌 파티클
    AudioSource audioSource;

    bool isControllable = true; // 입력/충돌 처리 가능 여부(연속 처리 방지)
    bool isCollidable = true; // 충돌 판정 on/off (디버그용)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // L: 다음 씬, C : 충돌 토글
        RespondToDebugKeys();
    }

    // 디버그 단축키 처리
    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame) //키보드 L키를 누르면
        {
            // 강제로 다음 씬 로드
            ReloadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame) // c키를 누를 때 마다 충돌을 끄고 켜기 가능
        {
            // 충돌 판정 토글
            isCollidable = !isCollidable;
        }
    }

    // 물리 충돌 시작 시 호출
    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }

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

    // 충돌 시 연출 및 재시작
    void StartCrashSequence()
    {
        isControllable = false; // 추가 입력/충돌 잠금
        audioSource.Stop(); // 중복 재생 방지
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false; // 추진/회전 정지
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene); // 현재 씬 재시작
    }

    // 목표 도달 시 연출 및 다음 스테이지
    void StartSuccessSequence()
    {
        isControllable = false; //다른 충돌 사운드 멈추기
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX); //성공 사운드
        successParticles.Play(); //성공 파티클
        GetComponent<Movement>().enabled = false; //움직임 멈춤
        Invoke("ReloadNextLevel", levelLoadDelay); //2초후에 ReloadNextLevel 메소드 호출
    }

    // 다음 씬 로드(마지막이면 0번으로 루프)
    void ReloadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; // 현재 씬 인덱스를 가져옴
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (currentScene == sceneCount - 1) //마지막 씬이면 0번째 씬으로 이동
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
