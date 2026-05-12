using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool bouncy, slippery;

    [SerializeField]
    public int bouncyMax;
    public int slipperyMax;
        

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setProperties(int lens)
    {
        Debug.Log("Whats Lens  " + lens);
        
            if (lens == 1)
            { setBouncy(); }

            else if (lens == 2)
            {
                setSlippery();
            }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entering Collision");
        rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (bouncy)
        {
            Debug.Log("Step # 50 Bouncing");
            Vector2 impulse = Vector2.up * 20;
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }
        else if (slippery)
        {
            
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        
    }



    private void setBouncy()
    {
        Debug.Log("Set Bouncy");
        bouncy = true;
        /*
        Debug.Log("SetBouncy");
        Vector2 impulse = Vector2.up * 10;
        rb.AddForce(impulse, ForceMode2D.Impulse);
        */
    }
    private void setSlippery()
    {
        Debug.Log("SetSlippery");
        slippery = true;
        //might need to edit the movestats ground decellerations



    }

    private void removeAll()
    {
        bouncy = false;
        slippery = false;
    }
}
