using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    private SpriteRenderer Sprite;


    public bool lens1 = false;
    public bool lens2 = false;
    public bool lens3 = false;

    public bool bouncyProp;
    public bool slidyProp;
    public bool teleportPads;

    public bool visible;
    public bool snapped;

    private void Awake()
    {
        Sprite=GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
        snapped = false;
        visible = false;
    }
    private void Update()
    {
        if (InputManager.SnapWasPressed)
            {
            Debug.Log("Step1 Snap was Pressed");
            Snap(); 
            }
    }

    //emables item on entry
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Lens1"))
        {
            Sprite.enabled = true;
            Sprite.color = Color.red;
            lens1 = true;

        }
        else if (collision.CompareTag("Lens2"))
        {
            Sprite.enabled = true;
            Sprite.color = Color.blue;
            lens2 = true;
           
        }
        else if (collision.CompareTag("Lens3"))
        {
            Sprite.enabled = true;
            Sprite.color = Color.green;
            lens3 = true;

           
        }
        visible = true;
    }


    //disbales the thing on exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!snapped)
        {
            Sprite.enabled=false;
            visible = false;
        }
        
    }

    
    public void Snap()
    {
        Debug.Log("Entering Snapped");
        if (visible)
        {
            snapped= true;
        }
    }

    public void ChangeProperties()
    {
        
    }


}
