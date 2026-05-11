using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;


    public bool shotTaken1 = false;
    public bool shotTaken2 = false;
    public bool shotTaken3 = false;

    bool visible1 = false;
    bool visible2 = false;
    bool visible3 = false;

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
        if (collision.CompareTag("Lens1"))
        {
            object1.SetActive(true);
            visible1 = true;
        }
        if (collision.CompareTag("Lens2"))
        {
            object2.SetActive(true);
            visible2 = true;
        }
        if (collision.CompareTag("Lens3"))
        {
            object3.SetActive(true);
            visible3 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lens1") && !shotTaken1)
        {
            object1.SetActive(false);
            visible1 = false;
        }
        if (collision.CompareTag("Lens2"))
        {
            if (!shotTaken2)
                object2.SetActive(false);

            visible2 = false;
        }
        if (collision.CompareTag("Lens3"))
        {
            if (!shotTaken3)
                object3.SetActive(false);

            visible3 = false;
        }
    }

    public void Snap()
    {
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


}
