using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    //public UnityEngine.UI.Text fpsText; // Reference to the UI text component
    public TextMeshProUGUI fpsText;

    void Update()
    {
        float fps = 1f / Time.deltaTime; // Calculate frames per second
        fpsText.text = "FPS: " + Mathf.Round(fps); // Update the UI text with FPS value
    }
}
