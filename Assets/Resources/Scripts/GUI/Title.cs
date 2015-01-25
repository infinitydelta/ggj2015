using System;
using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    public float lifeTime = 11.0f;
    public bool reload = false;


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
            if (reload)
                Application.LoadLevel(Application.loadedLevel);
            Destroy(gameObject);
        }
    }

    public void setupChildren(float totLifeTime, bool reloadLevel, float overlayInvisTime, float overlayFadeInTime,
        float overlayStayTime, float overlayFadeOutTime, String text, float textInvisTime, float textFadeInTime,
        float textStayTime, float textFadeOutTime)
    {
        lifeTime = totLifeTime;
        reload = reloadLevel;
        TitleBackground background = GetComponent<TitleBackground>();
        background.invisibleTime = overlayInvisTime;
        background.fadeInTime = overlayFadeInTime;
        background.stayTime = overlayStayTime;
        background.fadeOutTime = overlayFadeOutTime;
        UnityEngine.UI.Text textComp = GetComponent<UnityEngine.UI.Text>();
        textComp.text = text;
        TitleText tText = GetComponent<TitleText>();
        tText.invisibleTime = textInvisTime;
        tText.fadeInTime = textFadeInTime;
        tText.stayTime = textStayTime;
        tText.fadeOutTime = textFadeOutTime;

    }
}
