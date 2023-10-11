using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestUI : MonoBehaviour
{
    private GameObject QuestInfo;
    private void Start()
    {
        if(QuestInfo!=null)
        {
            QuestInfo = GameObject.Find("QuestInfo");
            QuestInfo.SetActive(false);
        }
        else
        {

        }
    }
    public void OpenQuest()
    {
        QuestInfo.SetActive(true);
    }

    public void CloseQuest()
    {
        QuestInfo.SetActive(false);
    }
    public void GotoQuestScene()
    {
        SceneManager.LoadScene("QuestSystem");
    }
}
