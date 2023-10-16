using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerAri : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject playerPrefab;
    public GameObject UIPrefab;
    public GameObject teleportPoint;
    public Transform playerSpawnPoint;

    Animator panelAnimator;
    bool isUp;

    private void Start()
    {

       InstantiatePlayer();
      //  InstantiateUI();

    }

    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint);
    }

    public void InstantiateUI()
    {
       // Instantiate(UIPrefab);
    }

    private void Update()
    {
        
    }
}
