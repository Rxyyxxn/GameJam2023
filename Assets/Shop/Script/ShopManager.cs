using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public string[] description;
    public TextMeshProUGUI descrip;
    public Image[] diceimages;
    public Image diceimage;
    public static ShopManager instance;

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

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void D6()
    {
        diceimage = diceimages[0];
        descrip.text = description[0];
    }

    public void D8()
    {
        diceimage = diceimages[1];
        descrip.text = description[1];
    }

    public void D10()
    {
        diceimage = diceimages[2];
        descrip.text = description[2];
    }
}
