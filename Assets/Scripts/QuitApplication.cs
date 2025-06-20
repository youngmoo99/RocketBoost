using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed) //esc키 누르면 애플리케이션 종료
        {   
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}
