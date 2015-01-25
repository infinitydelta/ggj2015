using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    public float lifeTime = 11.0f;
    public string loadLevel = "None";

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (elapsedTime >= lifeTime)
        {
            if (loadLevel != "None")
                Application.LoadLevel(loadLevel);
            Destroy(gameObject);
        }
    }
}
