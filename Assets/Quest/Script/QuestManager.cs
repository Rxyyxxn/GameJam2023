using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    private int currentQuestIndex; // Variable to store the current quest index

    public List<Quest> quests;

    private InventorySystem inventorySystem;

    public GameObject ItemText;
    float timer;
    bool itemactive = false;
    void Start()
    {
        ItemText.SetActive(false);
        AssignRandomQuests();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        inventorySystem = InventorySystem.current;
        Debug.Log(timer);
        if (//)
        {
            // If enough materials then complete the quest and reload now one, reset slider
            if (inventorySystem.ItemCount(quests[currentQuestIndex].itemname) >= quests[currentQuestIndex].requiredItems)
            {
                //minus item from stack
                inventorySystem.Remove(inventorySystem.GetItemDataThrough_ID(quests[currentQuestIndex].QuestIndex));

                CompleteQuest(currentQuestIndex); // Get quest index reference
            }
            // Else if not enough material, text appear showing not enough material and slider goes back to 0
            else if (inventorySystem.ItemCount(quests[currentQuestIndex].itemname)< quests[currentQuestIndex].requiredItems)
            {
                ItemText.SetActive(true);
                itemactive = true;     
            }
        }

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

    void AssignRandomQuests()
    {
        ShuffleQuests();

        quests[quest].
        
    }

    void ShuffleQuests()
    {
        // Shuffle your quests as before
    }

    public void CompleteQuest(int questIndex)
    {
        if (questIndex >= 0 && questIndex < quests.Count)
        {
            int reward = quests[questIndex].reward;
            // Implement logic to reward the player based on the quest completed (e.g., add currency, experience points, etc.)

            quests[questIndex] = GetRandomQuest();
            
        }
    }

    public void CompleteButton()
    {
        if(inventorySystem.ItemCount(quests[currentQuestIndex].itemname)>= quests[currentQuestIndex].requiredItems)
        {
            //minus item from stack
            inventorySystem.Remove(inventorySystem.GetItemDataThrough_ID(quests[currentQuestIndex].QuestIndex));

            CompleteQuest(currentQuestIndex); // Get quest index reference
        }
    }

    Quest GetRandomQuest()
    {
        return quests[Random.Range(0, quests.Count)];
    }
}
