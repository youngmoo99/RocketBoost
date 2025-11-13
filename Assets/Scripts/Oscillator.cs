using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; // 왕복 이동량(방향+거리)
    [SerializeField] float speed; // 왕복 속도(주파수 계수)
    Vector3 startPosition; //시작 지점
    Vector3 endPosition; //끝 지점
    float movementFactor; // 0~1 사이 보간 인자
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {      
        // PingPong(t,1): 0→1→0… 반복
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        
        // 시작~끝 사이를 선형 보간하여 왕복 이동
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor); 
    }
}
