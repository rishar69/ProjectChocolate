using UnityEngine;

public class NoteObject2 : MonoBehaviour
{
    public bool canHit = false;
    public bool Hit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canHit)
        {
            if (Hit)
            {
                Destroy(gameObject);
            }
        }
    }
}
