using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitlesAlpha : MonoBehaviour {

    public TextMeshProUGUI textMeshPro;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(FadeIn(5f));

	}

    IEnumerator FadeIn(float time)
    {
        float timeCopy = time;
        while( time > 0f )
        {
            float timeDelta = Time.deltaTime;
            time -= timeDelta;
            yield return new WaitForSeconds(timeDelta);
        }

        yield return new WaitForSeconds(2f);


        while (timeCopy > 0f)
        {
            float timeDelta = Time.deltaTime;
            timeCopy -= timeDelta;
            yield return new WaitForSeconds(timeDelta);
        }

        SceneManager.LoadScene(2);

        yield return null;
    }
}
