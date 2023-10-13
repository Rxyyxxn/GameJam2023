using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject tapToStartText;
    Image image;

    private void Awake()
    {
        image = tapToStartText.GetComponent<Image>();
    }
    public void OnButtonChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        image.CrossFadeAlpha(0.5f, 5, false);
    }
}
