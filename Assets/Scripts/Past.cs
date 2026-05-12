using UnityEngine;

public class Past : Drag
{
    public GameObject pastBG;
    public GameObject futureBG;
    protected override void Update()
    {
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("isDragging", true);
            Debug.Log("Future Lens Touched");
            pastBG.SetActive(true);
            futureBG.SetActive(false);
        }
    }
}
