using UnityEngine;
using System.Collections;

public class ButtonHitParent : MonoBehaviour
{
    private ButtonHit[] childButtons;
    private Animator playerAnimator;

    public GameObject player;
    public GameObject midPoint;
    public float fallSpeed = 6f;

    private Rigidbody2D playerRb;
    private bool isFirstButtonPressed = false;
    private KeyCode firstButton;
    private float firstPressTime;
    public float timingWindow = 0.065f; // Time window for detecting double press
    private bool pendingSingleAction = false; // Flag to delay single action execution

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 0;

        playerAnimator = player.GetComponent<Animator>();
        childButtons = GetComponentsInChildren<ButtonHit>(); // Get all child ButtonHit scripts
    }

    void FixedUpdate()
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -fallSpeed);
    }

    void Update()
    {
        DetectButtonPress();
    }

    void DetectButtonPress()
    {
        KeyCode hitButton1 = InputManager.Instance.hitButton1;
        KeyCode hitButton2 = InputManager.Instance.hitButton2;

        if (!isFirstButtonPressed && (Input.GetKeyDown(hitButton1) || Input.GetKeyDown(hitButton2)))
        {
            firstButton = Input.GetKeyDown(hitButton1) ? hitButton1 : hitButton2;
            firstPressTime = Time.time;
            isFirstButtonPressed = true;
            pendingSingleAction = true; // Mark single action as pending

            StartCoroutine(ExecuteSingleActionAfterDelay(firstButton));
        }

        if (isFirstButtonPressed)
        {
            KeyCode secondButton = (firstButton == hitButton1) ? hitButton2 : hitButton1;

            if (Input.GetKeyDown(secondButton))
            {
                float timeDiff = Time.time - firstPressTime;
                if (timeDiff <= timingWindow)
                {
                    pendingSingleAction = false; // Cancel single action
                    TriggerBothButtonAction();
                    isFirstButtonPressed = false;
                    return;
                }
            }

            if (Time.time - firstPressTime > timingWindow)
            {
                isFirstButtonPressed = false;
            }
        }
    }

    IEnumerator ExecuteSingleActionAfterDelay(KeyCode button)
    {
        yield return new WaitForSeconds(timingWindow);

        if (pendingSingleAction)
        {
            TriggerSingleButtonAction(button);
            pendingSingleAction = false;
        }
    }

    void TriggerBothButtonAction()
    {
        if(AudioManager.SFXManager !=  null)
        {
        AudioManager.SFXManager.PlaySound2D("Hit");
        }

        Debug.Log("Both buttons pressed together!");
        playerAnimator.SetTrigger("bothPressed");

        foreach (ButtonHit button in childButtons)
        {
            button.SetPressedState(true);
        }

        Vector3 targetPosition = new Vector3(player.transform.position.x, midPoint.transform.position.y +1, 0f);
        MovePlayer(targetPosition);
        fallSpeed = 0;

        StartCoroutine(ResetButtonsAfterDelay());

        StartCoroutine(testing());
    }

    void TriggerSingleButtonAction(KeyCode button)
    {
        Debug.Log(button + " pressed alone!");
        playerAnimator.SetTrigger("pressed");
        
        foreach (ButtonHit buttonScript in childButtons)
        {
            if (buttonScript.hitButton == button)
            {
                buttonScript.SetPressedState(true);

                Vector3 targetPosition = new Vector3(player.transform.position.x, buttonScript.transform.position.y + 1, 0f);
                MovePlayer(targetPosition);
            }
        }
        fallSpeed = 0;

        StartCoroutine(ResetButtonsAfterDelay());
        StartCoroutine(testing());

    }

    private void MovePlayer(Vector3 target)
    {
        player.transform.position = target;
    }

    IEnumerator testing()
    {
        yield return new WaitForSeconds(0.4f);
        fallSpeed = 10f ;
    }

    IEnumerator ResetButtonsAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (ButtonHit button in childButtons)
        {
            button.SetPressedState(false);
        }
    }
}
