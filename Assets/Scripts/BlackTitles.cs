using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackTitles : MonoBehaviour {

    AudioManager audioMix;
    GameObject BlackImage;
	// Use this for initialization
	void Start ()
    {
        audioMix = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        BlackImage = gameObject;
        StartCoroutine(FadeOut());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeOut()
    {
        Image imgBlack = BlackImage.GetComponent<Image>();
        float alpha = 1f;

        while (alpha > 0f)
        {
            float timeDelta = Time.deltaTime;

            if (alpha - timeDelta > 0f)
                alpha -= timeDelta;
            else
                alpha = 0;

            imgBlack.color = new Color(0f, 0f, 0f, alpha);

            yield return new WaitForSeconds(timeDelta);
        }

        yield return new WaitForSeconds(3f);

        while (alpha < 1f)
        {
            float timeDelta = Time.deltaTime;

            if (alpha + timeDelta > 1f)
                alpha = 1;
            else
                alpha += timeDelta;

            imgBlack.color = new Color(0f, 0f, 0f, alpha);
            yield return new WaitForSeconds(timeDelta);
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
        audioMix.FadeOut("WinBackground");
        audioMix.FadeIn("MenuBackground");

        yield return null;
    }
}
