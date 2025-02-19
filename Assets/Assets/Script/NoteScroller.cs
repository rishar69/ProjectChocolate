using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    public float noteSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noteSpeed = noteSpeed / 60;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(noteSpeed*Time.deltaTime,0f, 0f);
    }
}
