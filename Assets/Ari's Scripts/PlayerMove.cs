using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector2 oldPosition;
    private int tileNo;
    private void Start()
    {
        tileNo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = oldPosition;

        if (Input.GetKeyDown(KeyCode.S))
        {
            oldPosition.x += -0.5f;
            oldPosition.y += -0.25f;
            Debug.Log(oldPosition);
            tileNo -= 1;
            Debug.Log(tileNo);

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            oldPosition.x += 0.5f;
            oldPosition.y += 0.25f;
            Debug.Log(oldPosition);
            tileNo += 1;
            Debug.Log(tileNo);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            oldPosition.x += -0.5f;
            oldPosition.y += 0.25f;
            Debug.Log(oldPosition);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            oldPosition.x += 0.5f;
            oldPosition.y += -0.25f;
            Debug.Log(oldPosition);

        }
    }
}
