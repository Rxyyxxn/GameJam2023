using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Dice[] availableDices;
    public string[] description;
    public TextMeshProUGUI descrip, Coins;
    public Sprite[] diceimages;
    public Image diceimage;
    public static ShopManager instance;
    private Player player = new Player();
    public int currdice;

    public Button buyButton;
    public GameObject[] diceObjects;
    private int unlockedDiceIndex = 0;

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

    public void close()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameObject.SetActive(false);

        UpdateUI();
        buyButton.onClick.AddListener(BuyDice);
    }

    private void Update()
    {
        Coins.text = player.coins.ToString();
    }

    public void D6()
    {
        diceimage.sprite = diceimages[0];
        descrip.text = description[0];
        currdice = 6;
    }

    public void D8()
    {
        diceimage.sprite = diceimages[2];
        descrip.text = description[2];
        currdice = 8;
    }

    public void D10()
    {
        diceimage.sprite = diceimages[4];
        descrip.text = description[4];
        currdice = 10;
    }

    public void D12()
    {
        diceimage.sprite = diceimages[6];
        descrip.text = description[6];
        currdice = 12;
    }

    public void D20()
    {
        diceimage.sprite = diceimages[8];
        descrip.text = description[8];
        currdice = 20;
    }

    public void VarButton()
    {
        Variant(currdice);
    }

    public void DefButton()
    {
        Default(currdice);
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
                diceimage.sprite = diceimages[3];
                descrip.text = description[3];
                break;

            case 10:
                diceimage.sprite = diceimages[5];
                descrip.text = description[5];
                break;

            case 12:
                diceimage.sprite = diceimages[7];
                descrip.text = description[7];
                break;

            case 20:
                diceimage.sprite = diceimages[8];
                descrip.text = description[8];
                break;

            default:
                break;
        } 
    }

    public void Default(int DiceNum)
    {
        switch (DiceNum)
        {
            case 6:
                diceimage.sprite = diceimages[0];
                descrip.text = description[0];
                break;

            case 8:
                diceimage.sprite = diceimages[2];
                descrip.text = description[2];
                break;

            case 10:
                diceimage.sprite = diceimages[4];
                descrip.text = description[4];
                break;

            case 12:
                diceimage.sprite = diceimages[6];
                descrip.text = description[6];
                break;

            case 20:
                diceimage.sprite = diceimages[8];
                descrip.text = description[8];
                break;

            default:
                break;
        }
    }

    private void UpdateUI()
    {
        Coins.text = "Coins: " + player.coins.ToString();
        //buyButton.interactable = (unlockedDiceIndex < diceObjects.Length) && (player.coins >= availableDices[unlockedDiceIndex].price);
    }

    private void BuyDice()
    {
        if (unlockedDiceIndex < diceObjects.Length && player.coins >= availableDices[unlockedDiceIndex].price)
        {
            // Deduct coins and unlock the next dice
            player.coins -= availableDices[unlockedDiceIndex].price;
            availableDices[unlockedDiceIndex].IsBought = true;
            unlockedDiceIndex++;

            // Set the unlocked dice active and update UI
            diceObjects[unlockedDiceIndex - 1].SetActive(true);
            UpdateUI();
        }
    }
}
