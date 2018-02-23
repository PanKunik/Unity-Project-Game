using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;

    public void PlayGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        FindObjectOfType<AudioManager>().FadeOut("MenuBackground");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;

            if( operation.isDone )
                FindObjectOfType<AudioManager>().FadeIn("GameBackground");

            yield return null;
        }
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
