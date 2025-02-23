using UnityEngine;

public class HordeBehavior : MonoBehaviour
{
    private KeyCode hitKey;
    public bool isUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isUp)
        {
            hitKey = KeyCode.A;
        }
        else
        {
            hitKey = KeyCode.D;
        }
        foreach (Transform note in transform)
        {
            NoteObject noteObject = note.GetComponent<NoteObject>();
            noteObject.hitKey = hitKey;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
