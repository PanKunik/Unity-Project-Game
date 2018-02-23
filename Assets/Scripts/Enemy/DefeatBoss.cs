using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatBoss : MonoBehaviour {

    GameObject Fog;
    public GameObject TutorialArea;
    Animator blackPanelAnim;
    Light dirLight;

    AudioManager audioMix;

    public bool defeated = false;

	// Use this for initialization
	void Start () {
        Fog = GameObject.Find("Fog");
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
        dirLight.enabled = false;
        blackPanelAnim = GameObject.Find("BlackPanel").GetComponent<Animator>();
        // audioMix = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Defeat()
    {
        defeated = true;
        dirLight.enabled = true;
        Fog.SetActive(false);
        dirLight.intensity = 0.6f;
        dirLight.color = new Color(0.99f, 0.835f, 0.498f);

        TutorialArea.SetActive(true);
        TextMeshProUGUI textPro = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
        textPro.text = "You defeated King of Wolves! Great job! The evil has left our Lands. Sun is shining and birds are tweeting again.";

        FindObjectOfType<AudioManager>().FadeOut("GameBackground");
        FindObjectOfType<AudioManager>().FadeIn("WinBackground");

    }

    public void LoadEndScreen()
    {
        if (defeated == true)
        {
            StartCoroutine(WaitForTime(3f));
        }
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);

        blackPanelAnim.SetTrigger("Defeat");

        yield return new WaitForSeconds(4.2f);

        SceneManager.LoadScene(2);

        yield return null;
    }
}
