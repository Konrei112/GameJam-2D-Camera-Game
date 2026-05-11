using UnityEngine;

public class LensControls : MonoBehaviour
{
    [SerializeField] public GameObject Lens;
    [SerializeField] public int LensToggle; //If 0 then not enabled. If 1 Then Enabled
    [SerializeField] private PlayerMovement PlayerMov;
    private bool IsPressed;
    void Start()
    {
        LensToggle = 0;
        Lens.SetActive(false);
        PlayerMov=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        IsPressed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      LensToggles();
    }

    public void LensToggles()
    {
        if (InputManager.CamWasPressed)
        {
            //Controls are a bit buggy so check
            if (LensToggle == 0&&IsPressed==false)
            {
                Debug.Log("Lens Enabled");
                LensToggle++;
                PlayerMov.enabled = false;
                IsPressed = true;
                Lens.SetActive(true);
            }
            else if (LensToggle == 1&&IsPressed == true)
            {
                Debug.Log("Lens Disabled");
                LensToggle--;
                PlayerMov.enabled = true;
                IsPressed = false;
                Lens.SetActive(false);
            }
                



        }

    }
}
