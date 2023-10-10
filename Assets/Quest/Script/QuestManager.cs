using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    public List<Image> questImages;
    public QuestDetailsOverlay questDetailsOverlay; // Reference to the QuestDetailsOverlay script

    void Start()
    {
        AssignRandomQuests();
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
        int questCount = quests.Count;
        for (int i = 0; i < questCount - 1; i++)
        {
            // Generate a random index between i and questCount - 1
            int randomIndex = Random.Range(i, questCount);

            // Swap quests[i] and quests[randomIndex]
            Quest temp = quests[i];
            quests[i] = quests[randomIndex];
            quests[randomIndex] = temp;
        }
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
            string selectedQuestTitle = quests[questIndex].questDescription;
            string selectedQuestDescription = quests[questIndex].description;
            questDetailsOverlay.ShowQuestDetails(selectedQuestTitle, selectedQuestDescription);
        }
    }
}
