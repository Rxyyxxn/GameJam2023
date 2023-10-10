using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //PC
    private Vector2 oldPosition;
    //Mobile
    private Vector2 startPos;
    private Vector2 endPos;

    private int tileNo;
    private void Start()
    {
        tileNo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //PC Movement 
        {
            gameObject.transform.position = oldPosition;

            if (Input.GetKeyDown(KeyCode.S))
            {
                Down();

            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Up();

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Left();

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Right();

            }
        }
        //Mobile Movement
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endPos = Input.GetTouch(0).position;
                
                if (endPos.x > startPos.x && endPos.y > startPos.y)
                {
                    Up();
                }
                if (endPos.x < startPos.x && endPos.y < startPos.y)
                {
                    Down();
                }
                if (endPos.x < startPos.x && endPos.y > startPos.y)
                {
                    Left();
                }
                if (endPos.x > startPos.x && endPos.y < startPos.y)
                {
                    Right();
                }
                
            }
        }

    }
    public void Down()
    {
        oldPosition.x += -0.5f;
        oldPosition.y += -0.25f;
        Debug.Log(oldPosition);
        tileNo -= 1;
    }

    public void Up()
    {
        oldPosition.x += 0.5f;
        oldPosition.y += 0.25f;
        Debug.Log(oldPosition);
        tileNo += 1;
    }

    public void Left()
    {
        oldPosition.x += -0.5f;
        oldPosition.y += 0.25f;
    }

    public void Right()
    {
        oldPosition.x += 0.5f;
        oldPosition.y += -0.25f;
    }
}
