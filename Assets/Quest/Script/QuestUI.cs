using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public GameObject QuestInfo;
    private void Start()
    {
        QuestInfo.SetActive(false);
    }
    public void OpenQuest()
    {
        QuestInfo.SetActive(true);
    }
    public void CloseQuest()
    {
        QuestInfo.SetActive(false);
    }
}
