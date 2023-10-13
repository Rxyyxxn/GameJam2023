using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    private Player player=new Player();
    public TextMeshProUGUI coins_Text;
    public GameObject settings;
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
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player.coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coins_Text.text = player.coins.ToString();
        
    }

    public void OnSettingsButton()
    {
        SettingsManager.instance.gameObject.SetActive(true);
    }
}
