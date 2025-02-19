using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] public Transform Up;
    [SerializeField] public Transform Down;
    public bool isUp = false;
    public bool isDown = false;
    public bool isDouble = false;
    public Vector3 OriginalPosition;
    bool JustdoubleTap = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OriginalPosition = transform.position;
    }

    private IEnumerator MoveBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = OriginalPosition;
        if (JustdoubleTap == false)
        {
            JustdoubleTap = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 newPosition = transform.position;
        // newPosition.x += 0.1f;
        // transform.position = newPosition;
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isUp = true;
            isDown = false;
            isDouble = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isUp = false;
            isDown = true;
            isDouble = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))
        {
            isUp = false;
            isDown = false;
            isDouble = true;
        }
    }
}
