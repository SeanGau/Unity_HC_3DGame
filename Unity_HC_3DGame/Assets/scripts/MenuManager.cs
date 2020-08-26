using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("載入畫面")]
    public GameObject panel;
    [Tooltip("載入進度")]
    public Text textLoading;
    [Tooltip("載入bar")]
    public Image barLoading;
    [Tooltip("提示文字")]
    public Text textReady;


    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        panel.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync("遊戲場景");
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            textLoading.text = "載入進度：" + (ao.progress/0.9f * 100).ToString("F2") + "%";
            barLoading.fillAmount = ao.progress / 0.9f;
            yield return null;

            if(ao.progress == 0.9f)
            {
                textReady.gameObject.SetActive(true);
                if (Input.anyKeyDown)
                    ao.allowSceneActivation = true;
            }
        }
    }
}
