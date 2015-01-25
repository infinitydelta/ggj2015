using UnityEngine;
using System.Collections;

public class TitleBackground : MonoBehaviour
{
    private UnityEngine.UI.Image myImage;
    private float myAlpha = 0f;
    private float startTime;
    private float elapsedTime;
    public float invisibleTime = 0.0f;
    public float fadeInTime = 0.0f;
    public float stayTime = 8.0f;
    public float fadeOutTime = 11.0f;
    // Use this for initialization
    void Start()
    {
        myImage = GetComponent<UnityEngine.UI.Image>();
        startTime = Time.time;
        if (invisibleTime > 0)
        {
            myImage.color = new Color(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        if (elapsedTime < invisibleTime)
        {
            
        }
        else if (elapsedTime < fadeInTime)
        {
            myAlpha = ((elapsedTime - invisibleTime )/ (fadeInTime-invisibleTime));
            myImage.color = new Color(0.0f, 0.0f, 0.0f, myAlpha);
        }
        else if (elapsedTime < stayTime)
        {
            myAlpha = 1.0f;
            myImage.color = new Color(0.0f, 0.0f, 0.0f, myAlpha);
        }
        else if (elapsedTime < fadeOutTime)
        {
            myAlpha = 1.0f - ((elapsedTime - stayTime) / (fadeOutTime - stayTime));
            myImage.color = new Color(0.0f, 0.0f, 0.0f, myAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
