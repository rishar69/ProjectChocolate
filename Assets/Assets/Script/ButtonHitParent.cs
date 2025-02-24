using UnityEngine;
using System.Collections;

public class ButtonHitParent : MonoBehaviour
{
    private ButtonHit[] childButtons;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private bool isFirstButtonPressed;
    private KeyCode firstButton;
    private float firstPressTime;
    private bool pendingSingleAction;

    public GameObject player;
    public GameObject midPoint;
    public float fallSpeed = 6f;
    public float timingWindow = 0.065f;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 0;
        playerAnimator = player.GetComponent<Animator>();
        childButtons = GetComponentsInChildren<ButtonHit>();
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
            pendingSingleAction = true;

            StartCoroutine(ExecuteSingleActionAfterDelay(firstButton));
        }

        if (isFirstButtonPressed && Input.GetKeyDown((firstButton == hitButton1) ? hitButton2 : hitButton1))
        {
            if (Time.time - firstPressTime <= timingWindow)
            {
                pendingSingleAction = false;
                TriggerBothButtonAction();
                isFirstButtonPressed = false;
            }
        }
        else if (Time.time - firstPressTime > timingWindow)
        {
            isFirstButtonPressed = false;
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
        AudioManager.SFXManager?.PlaySound2D("Hit");
        Debug.Log("Both buttons pressed together!");
        playerAnimator.SetTrigger("attack2");

        foreach (ButtonHit button in childButtons)
        {
            button.SetPressedState(true);
        }

        fallSpeed = 0;
        MovePlayer(new Vector3(player.transform.position.x, midPoint.transform.position.y, 0f));

        StartCoroutine(ResetButtonsAfterDelay());
        StartCoroutine(ResetFallSpeed());
    }

    void TriggerSingleButtonAction(KeyCode button)
    {
        Debug.Log(button + " pressed alone!");
        AudioManager.SFXManager?.PlaySound2D("Hit");

        // Randomly choose between attack1 and attack2
        string attackTrigger = Random.value > 0.5f ? "attack1" : "attack2";
        playerAnimator.SetTrigger(attackTrigger);

        foreach (ButtonHit buttonScript in childButtons)
        {
            if (buttonScript.hitButton == button)
            {
                buttonScript.SetPressedState(true);
                MovePlayer(new Vector3(player.transform.position.x, buttonScript.transform.position.y, 0f));
            }
        }

        fallSpeed = 0;

        StartCoroutine(ResetButtonsAfterDelay());
        StartCoroutine(ResetFallSpeed());
    }

    private void MovePlayer(Vector3 target)
    {
        player.transform.position = target;
    }

    IEnumerator ResetFallSpeed()
    {
        yield return new WaitForSeconds(0.6f);
        fallSpeed = 10f;
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
