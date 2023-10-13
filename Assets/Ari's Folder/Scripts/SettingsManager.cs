using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField] public GameObject KMSButton;
    [SerializeField] public GameObject settingsMenu;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
       if (currentScene.name == "MainMenu")
        {
            KMSButton.SetActive(false);
        }
    }

    public void OnButtonKMS()
    {
        SceneManager.LoadScene("Lobby");

    }
}
