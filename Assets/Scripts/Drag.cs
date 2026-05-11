using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("isDragging", true);
  
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        animator.SetBool("isDragging", false);
    }

    private void OnMouseEnter()
    {
        animator.SetBool("IsHovering", true);
    }

    private void OnMouseExit()
    {
        animator.SetBool("IsHovering", false);
    }

}
