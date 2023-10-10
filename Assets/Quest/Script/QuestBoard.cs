using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestBoard : MonoBehaviour
{
    public GameObject questItemPrefab;
    public Transform questGrid;

    private List<string> quests = new List<string>();

    void Start()
    {
        InitializeQuests();
    }

    void InitializeQuests()
    {
        // Add initial quests to the board
        AddQuest("Quest 1: Defeat the monsters");
        AddQuest("Quest 2: Collect 10 items");
        AddQuest("Quest 3: Explore the dungeon");
    }

    void AddQuest(string questDescription)
    {
        quests.Add(questDescription);
        UpdateQuestGrid();
    }

    void UpdateQuestGrid()
    {
        // Clear existing quest items in the grid
        foreach (Transform child in questGrid)
        {
            Destroy(child.gameObject);
        }

        // Display quests in the grid
        foreach (string questDescription in quests)
        {
            GameObject questItemObject = Instantiate(questItemPrefab, questGrid);
            QuestItem questItem = questItemObject.GetComponent<QuestItem>();
            questItem.SetQuestDescription(questDescription);
        }
    }

    public void CompleteQuest(string questDescription)
    {
        quests.Remove(questDescription);
        UpdateQuestGrid();
        // Add logic here to reward the player for completing the quest
    }
}
