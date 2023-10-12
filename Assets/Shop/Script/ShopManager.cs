using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public string[] description;
    public TextMeshProUGUI descrip;
    public Sprite[] diceimages;
    public Image diceimage;
    public static ShopManager instance;

    public int currdice;

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
        diceimage.sprite = diceimages[0];
        descrip.text = description[0];
        currdice = 6;
    }

    public void D8()
    {
        Debug.Log("D8");
        diceimage.sprite = diceimages[1];
        descrip.text = description[1];
        currdice = 8;
    }

    public void D10()
    {
        diceimage.sprite = diceimages[2];
        descrip.text = description[2];
        currdice = 10;
    }

    public void D12()
    {
        diceimage.sprite = diceimages[2];
        descrip.text = description[2];
        currdice = 12;
    }

    public void D20()
    {
        diceimage.sprite = diceimages[2];
        descrip.text = description[2];
        currdice = 20;
    }

    public void VarButton()
    {
        Variant(currdice);
    }

    public void Variant(int DiceNum)
    {
        switch (DiceNum)
        {
            case 6:
                diceimage.sprite = diceimages[1];
                descrip.text = description[1];
                break;

            case 8:
                diceimage.sprite = diceimages[1];
                descrip.text = description[1];
                break;

            case 10:
                diceimage.sprite = diceimages[1];
                descrip.text = description[1];
                break;

            case 12:
                diceimage.sprite = diceimages[1];
                descrip.text = description[1];
                break;

            case 20:
                diceimage.sprite = diceimages[1];
                descrip.text = description[1];
                break;

            default:
                break;
        } 
    }
}
