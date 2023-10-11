using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMainMap : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            OnButtonChangeScene();
        }
    }
    public void OnButtonChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
