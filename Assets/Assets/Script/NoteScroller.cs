using UnityEditor.Overlays;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    public float noteSpeed;

    public static NoteScroller Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noteSpeed = noteSpeed / 60f;
        noteSpeed = noteSpeed * 2;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(noteSpeed*Time.deltaTime,0f, 0f);
    }
}
