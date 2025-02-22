using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundController : MonoBehaviour
{
    public float noteSpeed;
    private float startpos, currentpos;
    private float length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        noteSpeed = noteSpeed / 60f;
        noteSpeed = noteSpeed * 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(noteSpeed * Time.deltaTime, 0f, 0f);
        currentpos = transform.position.x;
        if (currentpos < startpos - length)
        {
            transform.position = new Vector3(startpos, transform.position.y, transform.position.z);
        }
    }
}
