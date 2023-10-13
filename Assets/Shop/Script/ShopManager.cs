using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Dice[] availableDices;
    public string[] description;
    public TextMeshProUGUI descrip, Coins, equipText, priceText;
    public Sprite[] diceimages;
    public Image diceimage;
    public static ShopManager instance;
    private Player player = new Player();
    public int currdice,previousdice;
    public GameObject buyButton,equipButton;

    public AudioClip[] audioClipArray;
    private AudioSource source;

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

        source.clip = audioClipArray[1];
        source.PlayOneShot(source.clip);
    }

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

        player.coins = 100;
        gameObject.SetActive(false);
        availableDices[0].IsEquipped=true;
        previousdice = 0;
        currdice = 0;
    }

    private void Update()
    {
        Coins.text = player.coins.ToString();
        UpdateUI(currdice);
    }

    public void D6()
    {
        diceimage.sprite = diceimages[0];
        descrip.text = description[0];
        previousdice = currdice;
        currdice = 0;
    }

    public void D8()
    {
        diceimage.sprite = diceimages[2];
        descrip.text = description[2];
        previousdice = currdice;
        currdice = 2;
    }

    public void D10()
    {
        diceimage.sprite = diceimages[4];
        descrip.text = description[4];
        previousdice = currdice;
        currdice = 4;
    }

    public void D12()
    {
        diceimage.sprite = diceimages[6];
        descrip.text = description[6];
        previousdice = currdice;
        currdice = 6;
    }

    public void D20()
    {
        diceimage.sprite = diceimages[8];
        descrip.text = description[8];
        previousdice = currdice;
        currdice = 8;
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
            case 0:
                diceimage.sprite = diceimages[1];
                descrip.text = description[1];
                previousdice = currdice;
                currdice = 1;
                break;

            case 2:
                diceimage.sprite = diceimages[3];
                descrip.text = description[3];
                previousdice = currdice;
                currdice = 3;
                break;

            case 4:
                diceimage.sprite = diceimages[5];
                descrip.text = description[5];
                previousdice = currdice;
                currdice = 5;
                break;

            case 6:
                diceimage.sprite = diceimages[7];
                descrip.text = description[7];
                previousdice = currdice;
                currdice = 7;
                break;

            case 8:
                diceimage.sprite = diceimages[8];
                descrip.text = description[8];
                previousdice = currdice;
                currdice = 9;
                break;

            default:
                break;
        } 
    }

    public void Default(int DiceNum)
    {
        switch (DiceNum)
        {
            case 1:
                diceimage.sprite = diceimages[0];
                descrip.text = description[0];
                previousdice = currdice;
                currdice = 0;
                break;

            case 3:
                diceimage.sprite = diceimages[2];
                descrip.text = description[2];
                previousdice = currdice;
                currdice = 2;
                break;

            case 5:
                diceimage.sprite = diceimages[4];
                descrip.text = description[4];
                previousdice = currdice;
                currdice = 4;
                break;

            case 7:
                diceimage.sprite = diceimages[6];
                descrip.text = description[6];
                previousdice = currdice;
                currdice = 6;
                break;

            case 9:
                diceimage.sprite = diceimages[8];
                descrip.text = description[8];
                previousdice = currdice;
                currdice = 8;
                break;

            default:
                break;
        }
    }

    public void ButtonBuy()
    {
        source.clip = audioClipArray[0];
        source.PlayOneShot(source.clip);

        Buy(currdice);
    }

    public void Buy(int DiceNum)
    {
        if(player.coins>= availableDices[DiceNum].price)
        {
            player.coins -= availableDices[DiceNum].price;
            availableDices[DiceNum].IsBought = true;
        }
    }

    public void ButtonEquip()
    {
        source.clip = audioClipArray[1];
        source.PlayOneShot(source.clip);

        Equip(currdice,previousdice);
    }

    public void Equip(int DiceNum,int PrevDice)
    {
        availableDices[DiceNum].IsEquipped = true;
        availableDices[PrevDice].IsEquipped = false;
    }



    public void UpdateUI(int DiceNum)
    {
        priceText.text = availableDices[DiceNum].price.ToString();
        if(availableDices[DiceNum].IsBought == true && availableDices[DiceNum].IsEquipped==true)
        {
            buyButton.SetActive(false);
            equipButton.SetActive(true);
            equipText.text = "equipped";
        }
        else if(availableDices[DiceNum].IsBought == true && availableDices[DiceNum].IsEquipped == false)
        {
            buyButton.SetActive(false);
            equipButton.SetActive(true);
            equipText.text = "equip";
        }
        else if (availableDices[DiceNum].IsBought == false)
        {
            buyButton.SetActive(true);
            equipButton.SetActive(false);
        }
    }
}
