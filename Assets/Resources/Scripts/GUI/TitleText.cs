using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour {
    public string[] myTitles;
    private UnityEngine.UI.Text myText;
    private float myAlpha = 0f;
    private float startTime;
    private float elapsedTime;
    public float invisibleTime = 0.0f;
    public float fadeInTime = 2.0f;
    public float stayTime = 6.0f;
    public float fadeOutTime = 8.0f;
	// Use this for initialization
	void Start () {
        myText = GetComponent<UnityEngine.UI.Text>();
        myText.text = myTitles[Random.Range(0, myTitles.Length - 1)];
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime = Time.time - startTime;
        if (elapsedTime < invisibleTime)
        {
            myAlpha = 0.0f;
            myText.color = new Color(1.0f, 1.0f, 1.0f, myAlpha);
        }
        else if (elapsedTime < fadeInTime)
        {
            myAlpha = ((elapsedTime - invisibleTime) / (fadeInTime - invisibleTime));
            myText.color = new Color(1.0f, 1.0f, 1.0f, myAlpha);
        }
        else if (elapsedTime < stayTime)
        {
            myAlpha = 1.0f;
            myText.color = new Color(1.0f, 1.0f, 1.0f, myAlpha);
        }
        else if (elapsedTime < fadeOutTime)
        {
            myAlpha = 1.0f - ((elapsedTime - stayTime) / (fadeOutTime - stayTime));
            myText.color = new Color(1.0f, 1.0f, 1.0f, myAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
