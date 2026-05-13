using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement pm;
    public float original;
    private bool bouncing,slipping;
    public KillingScene Killers;

    public float originalDecelleration;
    public float originalAcceleration;

    [SerializeField]
    public float changing_velocity;
    void Start()
    {

       Killers.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pm!=null)
        Debug.Log("Decelleration Speed"+ pm.MoveStats.GroundDeceleration);
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
        pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (bouncing)
        {
            setBouncy(rb);
        }
        else if (slipping)
        {
            setSlippery(rb);
        }
        else
        {
            Killers.enabled = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (slipping)
        {
            {
                pm.MoveStats.GroundDeceleration = 20;
                pm.MoveStats.GroundAcceleration = 5;
            }
        }
    }
   
   
    


    private void setBouncy(Rigidbody2D rb)
    {
        
        Debug.Log("SetBouncy");
        
        pm.JumpPad(changing_velocity);
        Killers.enabled = false;
    }
    private void setSlippery(Rigidbody2D rb)
    {
        Debug.Log("SetSlippery");
        //might need to edit the movestats ground decellerations
        setDecell();
        //Setting it to become slirppery
        pm.MoveStats.GroundDeceleration = 1;
        pm.MoveStats.GroundAcceleration= 10;
        Killers.enabled = false;

    }
    private void setDecell()
    {
        originalDecelleration = pm.MoveStats.GroundDeceleration;
    }

    
}
