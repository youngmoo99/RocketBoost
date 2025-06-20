using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; //벡터 이동 값
    [SerializeField] float speed; // 속도
    Vector3 startPosition; //시작 지점
    Vector3 endPosition; //끝 지점
    float movementFactor; 
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {   
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor); // start 부터 end까지 왔다갔다하기
    }
}
