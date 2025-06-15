using UnityEngine;
using UnityEngine.InputSystem; // namespace 

public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;
   [SerializeField] InputAction rotation; 
   [SerializeField] float thrustStrength = 100f;
   [SerializeField] float rotationStrength = 100f;
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
        if (thrust.IsPressed()) //방향키를 눌렀을때
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime); //상대적인 힘을 추가하는 메서드 (x,y,z축)
            if (!audioSource.isPlaying) // 오디오 한번만 재생
            {
                audioSource.Play();
            }   
        }
        else 
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation() 
    {
        float rotationInput = rotation.ReadValue<float>(); //누른 방향키가 양수인지 음수인지 확인
        if(rotationInput < 0) //왼쪽 방향키
        {   
            ApplyRotation(rotationStrength);
        }
        else if(rotationInput > 0) //오른쪽 방향키
        {
            ApplyRotation(-rotationStrength);
        }
      
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //물리시스템 일시적 정지
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
