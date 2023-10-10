using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDetailsOverlay : MonoBehaviour
{
    public TextMeshProUGUI titleTextInOverlay;
    public TextMeshProUGUI descriptionText;
    public Slider completionSlider;
    private Quest currentQuest;
    // Reference to the QuestManager script to access the quests list
    public QuestManager questManager;

    public void ShowQuestDetails(string questTitle, string questDescription)
    {
        titleTextInOverlay.text = questTitle;
        descriptionText.text = questDescription; // Set the description text
        gameObject.SetActive(true); // Show the overlay
    }

    public void OnSliderValueChanged(float value)
    {
        // Called when the slider value changes
        // You can use this function to update UI or perform actions as the player slides the slider
        // In this example, we are not doing anything specific, but you can add your logic here
    }

    public void CompleteQuest()
    {
        // Called when the player completes the quest by sliding the slider fully
        if (currentQuest != null && completionSlider.value >= completionSlider.maxValue)
        {
            // Quest completed successfully
            // Implement logic to reward the player based on the quest completed
            // ...

            // Randomize a new quest
            //ShowQuestDetails(currentQuest);
        }
        else
        {
            // Quest not completed, reset the slider
            completionSlider.value = 0;
        }
    }

    public void HideQuestDetails()
    {
        gameObject.SetActive(false); // Hide the overlay
    }
}
