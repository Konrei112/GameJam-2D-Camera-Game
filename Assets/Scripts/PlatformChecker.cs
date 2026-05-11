using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    private SpriteRenderer Sprite;


    public bool shotTaken1 = false;
    public bool shotTaken2 = false;
    public bool shotTaken3 = false;

    public bool bouncyProp;
    public bool slidyProp;
    public bool teleportPads;

    bool visible1 = false;
    bool visible2 = false;
    bool visible3 = false;
    private void Awake()
    {
        Sprite=GetComponent<SpriteRenderer>();
        Sprite.enabled = false;
    }
    private void Update()
    {
        if (InputManager.SnapWasPressed)
            {
            Debug.Log("Step1 Snap was Pressed");
            Snap(); 
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Lens1"))
        {
            Sprite.enabled = true;
            Sprite.color = Color.red;
          
        }
        if (collision.CompareTag("Lens2"))
        {
            Sprite.enabled = true;
            Sprite.color = Color.blue;
           
        }
        if (collision.CompareTag("Lens3"))
        {
            Sprite.enabled = true;
            Sprite.color = Color.green;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lens1"))
        {
            if (!shotTaken1)
                Sprite.enabled =false;
           
        }
        if (collision.CompareTag("Lens2"))
        {
            if (!shotTaken2)
                Sprite.enabled = false;

            visible2 = false;
        }
        if (collision.CompareTag("Lens3"))
        {
            if (!shotTaken3)
                Sprite.enabled = false;

            visible3 = false;
        }
        
    }

    public void Snap()
    {
        Debug.Log("EnteringSnapped");
        //This enables it to stay after exitting trigger 2D
        if (visible1 && !shotTaken1)
        {
            shotTaken1 = true;
            Debug.Log("Lens1 snapped");
        }

        if (visible2 && !shotTaken2)
        {
            shotTaken2 = true;
            Debug.Log("Lens2 snapped");
        }

        if (visible3 && !shotTaken3)
        {
            shotTaken3 = true;
            Debug.Log("Lens3 snapped");
        }
    }

    public void ChangeProperties()
    {
        
    }


}
