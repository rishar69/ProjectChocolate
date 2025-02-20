using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject Up;
    [SerializeField] public GameObject Down;
    private Animator anim;
    private KeyCode buttonW = KeyCode.W;
    private KeyCode buttonS = KeyCode.S;
    public Vector3 ReduceX;
    public Vector3 OriginalPosition;
    // Timing window (dalam detik)
    public float timingWindow = 0.065f;

    // Waktu tombol pertama ditekan
    private float firstButtonPressTime;

    // Tombol pertama yang ditekan
    private KeyCode firstButtonPressed;

    // Status apakah tombol pertama sudah ditekan
    private bool isFirstButtonPressed = false;
    public bool isHitting = false;
    public bool isUp = false;
    public bool isDown = false;
    public bool isDouble = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        OriginalPosition = transform.position;
        ReduceX = new Vector3(Up.transform.position.x * 0.25f, 0, 0);
    }
    void Update()
    {
        HittingControll();
        TwoBuuttonTiming();
    }
    void TwoBuuttonTiming()
    {
        // Cek jika tombol W atau S ditekan untuk pertama kali
        if (isFirstButtonPressed == false && (Input.GetKeyDown(buttonW) || Input.GetKeyDown(buttonS)))
        {
            // Simpan tombol pertama yang ditekan
            firstButtonPressed = Input.GetKeyDown(buttonW) ? buttonW : buttonS;
            firstButtonPressTime = Time.time;
            isFirstButtonPressed = true;
            // Debug.Log("Tombol pertama ditekan: " + firstButtonPressed);
        }

        // Cek jika tombol kedua ditekan dalam timing window
        if (isFirstButtonPressed)
        {
            // Tentukan tombol kedua yang harus ditekan
            KeyCode secondButton = (firstButtonPressed == buttonW) ? buttonS : buttonW;

            if (Input.GetKeyDown(secondButton))
            {
                float secondButtonPressTime = Time.time;
                float timeDifference = secondButtonPressTime - firstButtonPressTime;

                if (timeDifference <= timingWindow)
                {
                    isDouble = true;
                    transform.position = new Vector3(Up.transform.position.x + ReduceX.x, Up.transform.position.y + Down.transform.position.y, transform.position.z);
                    anim.SetTrigger("attack");
                    StartCoroutine(MoveBackAfterDelay(0.15f));
                    // Debug.Log("Tombol kedua ditekan dalam timing window.");
                }
                else
                {
                    // Debug.Log("Tombol kedua ditekan di luar timing window.");
                }

                // Reset status
                isFirstButtonPressed = false;
            }

            // Reset jika timing window terlewat
            if (Time.time - firstButtonPressTime > timingWindow)
            {
                // Debug.Log("Timing window terlewat.");
                if (firstButtonPressed == buttonW)
                {
                    isUp = true;
                    transform.position = Up.transform.position + ReduceX;
                    anim.SetTrigger("attack");
                    StartCoroutine(MoveBackAfterDelay(0.15f));
                    // Debug.Log("Tombol kedua ditekan di luar timing window.");
                }
                else if (firstButtonPressed == buttonS)
                {
                    isDown = true;
                    transform.position = Down.transform.position + ReduceX;
                    anim.SetTrigger("attack");
                    StartCoroutine(MoveBackAfterDelay(0.15f));
                    // Debug.Log("Tombol kedua ditekan di luar timing window.");
                }
                isFirstButtonPressed = false;
            }
        }
    }
    void HittingControll() { if (Input.GetKeyDown(buttonW) || Input.GetKeyDown(buttonS)) { isHitting = true; Delay(0.001f); } }
    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = OriginalPosition;
        isUp = false;
        isDown = false;
        // Wait until the attack animation is finished
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        transform.position = OriginalPosition;
        isUp = false;
        isDown = false;
        isDouble = false;
    }
    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHitting = false;
    }
}

