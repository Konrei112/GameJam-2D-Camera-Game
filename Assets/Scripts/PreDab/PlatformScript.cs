using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement pm;

    private bool bouncing,slipping;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setProperties(int lens)
    {
        Debug.Log("LENS NUMBER " + lens);
        
            if (lens == 1)
            {
                Debug.Log("STEP 1000: Turning Bouncy");
               bouncing = true;
                slipping = false;
            }

            else if (lens == 2)
            {
                Debug.Log("STEP 2000: TURNING   Slippery");
                bouncing =false;
                slipping = true;
            }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb= collision.gameObject.GetComponent<Rigidbody2D>();
        pm= collision.gameObject.GetComponent<PlayerMovement>();
        if (bouncing)
        {
            setBouncy(rb);
        }
        else if (slipping)
        {
            setSlippery(rb);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        
    }



    private void setBouncy(Rigidbody2D rb)
    {
        
        Debug.Log("SetBouncy");
        
        pm.JumpPad();
    }
    private void setSlippery(Rigidbody2D rb)
    {
        Debug.Log("SetSlippery");
        //might need to edit the movestats ground decellerations



    }
}
