using UnityEngine;
using UnityEngine.InputSystem; // namespace 

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust; // 추진 (space바)
    [SerializeField] InputAction rotation; // 좌/우 회전
    [SerializeField] float thrustStrength = 100f; // 추진력
    [SerializeField] float rotationStrength = 100f; // 회전 속도
    [SerializeField] AudioClip mainEngineSFX; // 메인 엔진 사운드
    [SerializeField] ParticleSystem mainEngineParticles; // 하부 화염
    [SerializeField] ParticleSystem rightEngineParticles; // 우측 보조추진 (왼쪽 선회)
    [SerializeField] ParticleSystem leftEngineParticles; // 좌측 보조추진 (오른쪽 선회)

    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    //오브젝트 활성화 필요 onEnable 
    // 스크립트 활성 시 InputAction 활성화
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

    // 추진 입력 처리
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
        // 로컬 업 방향(로켓 전면 기준)으로 힘을 가함
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

    // 좌/우 회전 처리
    void ProcessRotation() 
    {
        float rotationInput = rotation.ReadValue<float>(); //누른 방향키가 양수인지 음수인지 확인
        if(rotationInput < 0) //왼쪽 방향키
        {
            RotateRight(); // 왼쪽 키 → 기체는 오른쪽으로 회전 벡터(시계)
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
        if (!rightEngineParticles.isPlaying) // 왼쪽으로 기수를 꺾을 때 오른쪽 분사
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Play();
        }
    }
    void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftEngineParticles.isPlaying) // 오른쪽으로 기수를 꺾을 때 왼쪽 분사
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

    // 물리 회전 간섭 방지 후 수동 회전
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // 물리 회전 잠시 비활성화(토크 간섭 제거)
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
