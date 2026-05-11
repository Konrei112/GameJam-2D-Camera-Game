using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector director;
    private BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (col.CompareTag("End"))
        {
            director.Play();
            col.enabled = false;
        }
    }
    void Update()
    {
        
    }
}
