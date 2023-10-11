using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image[] diceimages;
    public Image diceimage;

    public void D6()
    {
        diceimage = diceimages[1];
    }
}
