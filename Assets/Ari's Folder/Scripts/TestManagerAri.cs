using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerAri : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    Animator panelAnimator;
    bool isUp;

    private void Start()
    {
       panelAnimator = controlPanel.GetComponent<Animator>();
       panelAnimator.SetBool("IsUp?",false);
       isUp = false;
       InstantiatePlayer();
    }
    public void OnKeyPressed()
    {
        if (!isUp)
        {
            panelAnimator.SetBool("IsUp?", true);
            isUp = true;
        }
        else
        {
            panelAnimator.SetBool("IsUp?", false);
            isUp = false;
        }
    }

    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint);
    }
}
