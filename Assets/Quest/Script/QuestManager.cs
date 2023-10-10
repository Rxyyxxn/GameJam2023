using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuestManager : MonoBehaviour
{
    private int currentQuestIndex; // Variable to store the current quest index

    public List<Quest> quests;
    public List<Image> questImages;
    public QuestDetailsOverlay questDetailsOverlay; // Reference to the QuestDetailsOverlay script

    void Start()
    {
        AssignRandomQuests();
    }

    private void Update()
    {
        if (questDetailsOverlay.completionSlider.value == questDetailsOverlay.completionSlider.maxValue)
        {
            // If enough materials then complete the quest and reload now one, reset slider
            if (questDetailsOverlay.ItemCount >= 1)
            {
                Debug.Log("Completed");
                questDetailsOverlay.ItemCount--;
                CompleteQuest(currentQuestIndex); // Get quest index reference
                questDetailsOverlay.HideQuestDetails();
                questDetailsOverlay.completionSlider.value = 0;
            }
            // Else if not enough material, text appear showing not enough material and slider goes back to 0
            else if(questDetailsOverlay.ItemCount < 1)
            {
                Debug.Log("Not Complete");
                questDetailsOverlay.completionSlider.value = 0;
            }
        }
    }

    void AssignRandomQuests()
    {
        ShuffleQuests();

        for (int i = 0; i < questImages.Count; i++)
        {
            if (i < quests.Count)
            {
                questImages[i].GetComponentInChildren<TextMeshProUGUI>().text = quests[i].questDescription;
            }
            else
            {
                questImages[i].gameObject.SetActive(false);
            }
        }
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
            questImages[questIndex].GetComponentInChildren<TextMeshProUGUI>().text = quests[questIndex].questDescription;
        }
    }

    Quest GetRandomQuest()
    {
        return quests[Random.Range(0, quests.Count)];
    }

    public void OnQuestImageClick(int questIndex)
    {
        if (questIndex >= 0 && questIndex < quests.Count)
        {
            currentQuestIndex = questIndex; // Store the current quest index
            string selectedQuestTitle = quests[questIndex].questDescription;
            string selectedQuestDescription = quests[questIndex].description;
            questDetailsOverlay.ShowQuestDetails(selectedQuestTitle, selectedQuestDescription);
        }
    }
}
