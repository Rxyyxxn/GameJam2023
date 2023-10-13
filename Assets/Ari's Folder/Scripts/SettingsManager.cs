using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField] public GameObject KMSButton;
    [SerializeField] public GameObject settingsMenu;
    public static SettingsManager instance;
    public GameObject settingsPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        settingsPanel.SetActive(false);
    }
    private void Update()
    {
       if (currentScene.name == "MainMenu")
        {
            KMSButton.SetActive(false);
        }
       if (currentScene.name == "MainMap")
        {
            KMSButton.SetActive(true);
        }
       if (settingsPanel.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
       else
        {
            Time.timeScale = 1.0f;

        }
    }

    public void OnButtonKMS()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1.0f;


    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
}
