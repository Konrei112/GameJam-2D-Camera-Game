using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    private SpriteRenderer Sprite;
    [SerializeField]
    public GameObject LensObject;
    [SerializeField]
    public GameObject platform;
    private ParticleSystem particles;

    

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
        platform.SetActive(false);
        Sprite=LensObject.GetComponent<SpriteRenderer>();
        particles =GetComponent<ParticleSystem>();
        Sprite.enabled = false;
        snapped = false;
        visible = false;
        
    }
    public void platformEnabler(int lens)
    {
        Debug.Log("STEP # 2| Enabling Platform");
        particles.Stop();
        platform.SetActive(true);
        PlatformScript PS = platform.GetComponent<PlatformScript>();
        PS.setProperties(lens);

    }

    //emables item on entry
   

    
 

  


}
