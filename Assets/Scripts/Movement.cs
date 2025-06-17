using UnityEngine;
using UnityEngine.InputSystem; // namespace 

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation; 
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    //오브젝트 활성화 필요 onEnable 
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed()) //스페이스바 눌렀을때
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); //상대적인 힘을 추가하는 메서드 (x,y,z축)
        if (!audioSource.isPlaying) // 오디오 한번만 재생
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessRotation() 
    {
        float rotationInput = rotation.ReadValue<float>(); //누른 방향키가 양수인지 음수인지 확인
        if(rotationInput < 0) //왼쪽 방향키
        {
            RotateRight();
        }
        else if(rotationInput > 0) //오른쪽 방향키
        {
            RotateLeft();
        }
        else  //아무것도 하지않앗을때 왼쪽 오른쪽 엔진 파티클 비활성화
        {
            StopRotate();
        }
    }

    void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightEngineParticles.isPlaying) // 왼쪽으로 꺾엇을때 오른쪽 엔진 파티클 활성화
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Play();
        }
    }
    void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftEngineParticles.isPlaying) // 오른쪽으로 꺾었을때 왼쪽 엔진 파티클 활성화
        {
            rightEngineParticles.Stop();
            leftEngineParticles.Play();
        }
    }
    void StopRotate()
    {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //물리시스템 일시적 정지
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
