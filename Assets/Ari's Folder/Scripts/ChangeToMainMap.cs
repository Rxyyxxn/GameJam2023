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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && PlayerMove.instance.endPos.x > PlayerMove.instance.startPos.x && PlayerMove.instance.endPos.y > PlayerMove.instance.startPos.y)
        {
            OnButtonChangeScene();
        }
    }
    public void OnButtonChangeScene()
    {
        PlayerMove.instance.unactiveInvUI = true;
        SceneManager.LoadScene(sceneName);
    }
}
