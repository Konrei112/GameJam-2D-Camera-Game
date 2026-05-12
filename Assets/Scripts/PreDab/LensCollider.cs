using JetBrains.Annotations;
using UnityEngine;

public class LensCollider : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer Sprite;
    
  



    public bool lens1 = false;
    public bool lens2 = false;
    public bool lens3 = false;

    public bool bouncyProp;
    public bool slidyProp;
    public bool teleportPads;

    public bool visible;
    public bool snapped;

    public int lenss;

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
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

        Debug.Log("Got Triggered");

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
        
        visible = true;
    }


    //disbales the thing on exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit Trigger");
        if (!snapped)
        {
            Sprite.enabled = false;
            visible = false;
            lens1 = false; 
            lens2 = false; 
            lens3 = false; 
        }

    }
    public void Snap()
    {
        Debug.Log("Entering Snapped");
        if (visible)
        {
            snapped = true;
            platformEnable();
        }
    }

    public void platformEnable()
    {
        Debug.Log("STEP # 1| Enabling Platform");
        PlatformChecker pc= GetComponentInParent<PlatformChecker>();
       
        if (lens1) lenss = 1;
        else if(lens2) lenss = 2;
        
            pc.platformEnabler(lenss);
    }
}
