using UnityEngine;
using System.Collections;
using System.Xml.Schema;

public class Door : MonoBehaviour
{
    bool opened = false;
    public float health = 300;
    private float maxHealth;
    private Transform bitBroken;
    private Transform veryBroken;
    private Transform completelyBroken;
    private Transform knob;
    private GameObject curEnabled;

    public AudioClip AudioClipBitBroken;
    public AudioClip AudioClipVeryBroken;
    public AudioClip AudioClipCompletelyBroken;
    private AudioSource myAudioSource;

	// Use this for initialization
	void Start ()
	{
	    knob = transform.FindChild("Cylinder");
	    maxHealth = health;
	    bitBroken = transform.FindChild("DoorBroke1");
	    veryBroken = transform.FindChild("DoorBroke2");
	    completelyBroken = transform.FindChild("DoorGibs");
        curEnabled = transform.FindChild("Cube.001").gameObject;
        myAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (health < 0)
	    {
	        GameObject.Find("WinHandler").GetComponent<WinConditions>().doorWin();
	        Instantiate(Resources.Load<GameObject>("Prefabs/GUI/WinScreens/EscapeWin"));
	        breakApart();
	    }
	    else if(health < maxHealth/3)
	    {
	        veryBroken.gameObject.SetActive(true);
            bitBroken.gameObject.SetActive(false);
	    }
        else if (health < (2*maxHealth/3))
        {
            bitBroken.gameObject.SetActive(true);
            curEnabled.SetActive(false);
        }
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<ObjectScript>()!=null)
        {
            health -= col.relativeVelocity.magnitude*col.gameObject.rigidbody.mass;
        }
        else if (health < maxHealth / 3)
        {
            myAudioSource.PlayOneShot(AudioClipVeryBroken);
        }
        else if (health < (2 * maxHealth / 3))
        {
            myAudioSource.PlayOneShot(AudioClipBitBroken);
        }
    }


    void breakApart()
    {
        //unfinished, add broken up mesh then break apart in script when thats in
        completelyBroken.gameObject.SetActive(true);
        veryBroken.gameObject.SetActive(false);
        myAudioSource.PlayOneShot(AudioClipCompletelyBroken);
        Destroy(knob.gameObject);
        Destroy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Key_door") && !opened)
        {
            opened = true;
            GetComponent<Animator>().SetTrigger("open");

        }
    }
}
