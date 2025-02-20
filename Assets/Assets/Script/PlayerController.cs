using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject Up;
    [SerializeField] public GameObject Down;
    public KeyCode buttonW = KeyCode.W;
    public KeyCode buttonS = KeyCode.S;
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
    public bool isUp = false;
    public bool isDown = false;
    public bool isDouble = false;

    void Start()
    {
        OriginalPosition = transform.position;
        ReduceX = new Vector3(0.50f, 0, 0);
    }

    void Update()
    {
        // Cek jika tombol A atau D ditekan untuk pertama kali
        if (!isFirstButtonPressed && (Input.GetKeyDown(buttonW) || Input.GetKeyDown(buttonS)))
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
                    transform.position = new Vector3(Up.transform.position.x, Up.transform.position.y + Down.transform.position.y, transform.position.z);
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
                    transform.position = Up.transform.position - ReduceX;
                    StartCoroutine(MoveBackAfterDelay(0.15f));
                    // Debug.Log("Tombol kedua ditekan di luar timing window.");
                }
                else if (firstButtonPressed == buttonS)
                {
                    isDown = true;
                    transform.position = Down.transform.position - ReduceX;
                    StartCoroutine(MoveBackAfterDelay(0.15f));
                    // Debug.Log("Tombol kedua ditekan di luar timing window.");
                }
                isFirstButtonPressed = false;
            }
        }
    }
    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = OriginalPosition;
        isUp = false;
        isDown = false;
        isDouble = false;
    }
}

