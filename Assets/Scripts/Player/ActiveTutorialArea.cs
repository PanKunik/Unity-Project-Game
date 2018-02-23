using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveTutorialArea : MonoBehaviour {

    public GameObject TutorialArea;

    public string text;

    public void CloseHint()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            TutorialArea.SetActive(true);
            TextMeshProUGUI textPro = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
            textPro.text = text;
            Destroy(gameObject);
        }
    }
}
