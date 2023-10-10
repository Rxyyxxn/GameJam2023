using UnityEngine;
using TMPro;

public class QuestItem : MonoBehaviour
{
    public TextMeshProUGUI questDescriptionText;

    public void SetQuestDescription(string description)
    {
        questDescriptionText.text = description;
    }
}
