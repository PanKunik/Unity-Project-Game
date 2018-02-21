using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour {

    Image Background;
    Sprite[] Wallpapers;

    int wallpaperCount;
    int randomNumber;
   
    public float timeBeetwenChanges = 15f;
    float baseChangeTime;

    public float fadeTime = 3f;
    float fadeIn, fadeOut;
    bool calledOut = false;
    bool calledIn = false;


    void Start () {
        baseChangeTime = timeBeetwenChanges;
        fadeIn = fadeOut = fadeTime;

        Background = GameObject.Find("Background").GetComponent<Image>();
        Wallpapers = Resources.LoadAll<Sprite>("Backgrounds");

        foreach (object ob in Wallpapers)
        {
            wallpaperCount++;
        }

        randomNumber = (int)Random.Range(0, wallpaperCount);
        Background.sprite = Wallpapers[randomNumber];
	}
	
	void Update () {
        timeBeetwenChanges -= Time.deltaTime;

        if( timeBeetwenChanges <= 0f )
        {
            if (!calledOut)
            {
                FadeOut(fadeTime);
                calledOut = true;
            }

            fadeOut -= Time.deltaTime;

            if (fadeOut <= 0f)
            {
                if (!calledIn)
                {
                    ChangeBack();
                    FadeIn(fadeTime);
                    calledIn = true;
                }

                fadeIn -= Time.deltaTime;
                if( fadeIn <= 0f )
                {
                    fadeIn = fadeOut = fadeTime;
                    timeBeetwenChanges = baseChangeTime;
                    calledIn = calledOut = false;
                }
            }
        }
	}

    public void FadeOut(float fadeTime)
    {
        Background.CrossFadeColor(new Color(0f, 0f, 0f), fadeTime, true, false);
    }

    public void FadeIn(float fadeTime)
    {
        Background.CrossFadeColor(new Color(1f, 1f, 1f), fadeTime, true, false);
    }

    public void ChangeBack()
    {
        randomNumber = Random.Range(0, wallpaperCount);
        while (Background.sprite == Wallpapers[randomNumber])
            randomNumber = Random.Range(0, wallpaperCount);

        Background.sprite = Wallpapers[randomNumber];
    }

}
