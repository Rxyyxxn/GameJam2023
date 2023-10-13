using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerAri : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject playerPrefab;
    public GameObject teleportPoint;
    public Transform playerSpawnPoint;

    Animator panelAnimator;
    bool isUp;

    private void Start()
    {

       InstantiatePlayer();

    }

    public void InstantiatePlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint);
    }

    private void Update()
    {
        
    }
}
