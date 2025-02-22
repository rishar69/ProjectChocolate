using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public KeyCode hitButton1 = KeyCode.A;
    public KeyCode hitButton2 = KeyCode.D;

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

    public bool IsButton1Pressed()
    {
        return Input.GetKey(hitButton1);
    }

    public bool IsButton2Pressed()
    {
        return Input.GetKey(hitButton2);
    }

    public bool AreBothButtonsPressed()
    {
        return IsButton1Pressed() && IsButton2Pressed();
    }
}
