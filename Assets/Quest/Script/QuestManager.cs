using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class QuestManager : MonoBehaviour
{

    public List<Quest> quests;
    private InventorySystem inventorySystem;
    public GameObject ItemText,Questcanvas,sign,Clicktosign;
    public Player player = new Player();
    private int currentQuestIndex, prevquestindex; // Variable to store the current quest index
    float timer,signTimer;
    bool itemactive = false;
    public TextMeshProUGUI Title, description;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        AssignRandomQuests();
    }

    void Start()
    {
        ItemText.SetActive(false);
        Questcanvas.SetActive(true);
        AssignRandomQuests();
    }

    private void Update()
    {
        inventorySystem = InventorySystem.current;

        //Debug.Log(timer);
        if (itemactive == true && timer <= 1.5)
        {
            timer += Time.deltaTime;
        }
        else
        {
            ItemText.SetActive(false);
            itemactive = false;
            timer = 0;
        }
    }

    public void close()
    {
        Questcanvas.SetActive(false);
    }

    void AssignRandomQuests()
    {     
        while(currentQuestIndex==prevquestindex)
        {
            currentQuestIndex = Random.Range(0, quests.Count);
        }

        Title.text = quests[currentQuestIndex].Title;
        description.text = quests[currentQuestIndex].description;
        prevquestindex = currentQuestIndex;

        Clicktosign.SetActive(true);
        sign.SetActive(false);
    }

    public void CompleteButton()
    {

        if (inventorySystem.ItemCount(quests[currentQuestIndex].itemname) >= quests[currentQuestIndex].requiredItems)
        {
            //minus item from stack
            inventorySystem.Remove(inventorySystem.GetItemDataThrough_ID(quests[currentQuestIndex].QuestIndex));
            player.coins += quests[currentQuestIndex].reward;
            Clicktosign.SetActive(false);
            sign.SetActive(true);

            StartCoroutine(Wait());

        }
        else
        {
            sign.SetActive(false);
            Clicktosign.SetActive(true);
            ItemText.SetActive(true);
            itemactive = true;
        }
    }
}
