using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaName : MonoBehaviour {

    GameObject TMProArea;
    public static Collider lastTrigger;

    // Use this for initialization
    void Start () {
        TMProArea = GameObject.Find("AreaNameText");
        TMProArea.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0f);
        // TMProArea.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            float alpha = 0f;
            StartCoroutine(FadeIn(alpha));
            TMProArea.GetComponent<TextMeshProUGUI>().text = gameObject.name;

            if (lastTrigger == null)
            {
                AreaName.lastTrigger = gameObject.GetComponent<Collider>();
                AreaName.lastTrigger.enabled = false;
            }
            else
            {
                AreaName.lastTrigger.enabled = true;
                AreaName.lastTrigger = gameObject.GetComponent<Collider>();
                AreaName.lastTrigger.enabled = false;
            }
        }
    }

    IEnumerator FadeIn(float alpha)
    {
        float time = 2f;

        while (alpha < 1f)
        {
            alpha += 0.05f;
            TMProArea.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, alpha);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(time);
        StartCoroutine(FadeOut(alpha));

        yield return null;
    }

    IEnumerator FadeOut(float alpha)
    {

        while (alpha > 0f)
        {
            alpha -= 0.05f;
            TMProArea.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, alpha);

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
