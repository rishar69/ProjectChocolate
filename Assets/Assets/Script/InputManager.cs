using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public KeyCode buttonOne = KeyCode.A;
    public KeyCode buttonTwo = KeyCode.D;

    // Event that sends the KeyCode of the pressed button
    public event Action<KeyCode> OnButtonPressed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(buttonOne))
        {
            OnButtonPressed?.Invoke(buttonOne); // Send buttonOne keycode
        }

        if (Input.GetKeyDown(buttonTwo))
        {
            OnButtonPressed?.Invoke(buttonTwo); // Send buttonTwo keycode
        }
    }
}

