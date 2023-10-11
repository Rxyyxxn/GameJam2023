using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestDetailsOverlay : MonoBehaviour,IPointerUpHandler, IPointerDownHandler
{
    public TextMeshProUGUI titleTextInOverlay;
    public TextMeshProUGUI descriptionText;
    public Slider completionSlider;
    private Quest currentQuest;
    // Reference to the QuestManager script to access the quests list
    public QuestManager questManager;

    private bool isPointerDown=false;

    private void Start()
    {
        // Add a listener to the slider's onValueChanged event
        completionSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {    
        // Check if the pointer is down and the slider is not at the maximum value
        if (isPointerDown == false && completionSlider.value < completionSlider.maxValue)
        {
            // Increment the slider value as the player holds down the slider
            completionSlider.value -= Time.deltaTime; // You can adjust the speed here
        }
        
    }


    public void OnSliderValueChanged(float value)
    {
        // Check if the slider is not at the maximum value
        // Called when the slider value changes
        // You can use this function to update UI or perform actions as the player slides the slider
        // In this example, we are not doing anything specific, but you can add your logic here

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // When the pointer is down, set the flag to true
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        isPointerDown = false;
    }
    public void ShowQuestDetails(string questTitle, string questDescription)
    {
        titleTextInOverlay.text = questTitle;
        descriptionText.text = questDescription; // Set the description text
        gameObject.SetActive(true); // Show the overlay
    }

    public void HideQuestDetails()
    {
        gameObject.SetActive(false); // Hide the overlay
    }
}
