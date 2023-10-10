using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject invUI;
    //PC
    private Vector2 oldPosition;
    //Mobile
    private Vector2 startPos;
    private Vector2 endPos;
    [SerializeField] public GameObject raycastOBJ;


    private void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (invUI.activeSelf)
            {
                for (int i = 0; i < InventorySystem.current.inventory.Count; i++)
                {
                    if (InventorySystem.current.inventory[i].data.displayName == "Carrot0")
                    {
                        InventorySystem.current.Remove(InventorySystem.current.inventory[i].data);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (invUI.activeSelf)
            {
                invUI.SetActive(false);
            }
            else
            {
                invUI.SetActive(true);
            }
        }

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
        Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y - 0.25f);

        Vector3Int temp = tilemap.WorldToCell(nextPos);
        if (tilemap.HasTile(temp))
        {
            oldPosition.x += -0.5f;
            oldPosition.y += -0.25f;
        }
        else
        {

        }
    }

    public void Up()
    {
        Vector2 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y + 0.25f);
        Vector3Int temp = tilemap.WorldToCell(nextPos);
        if (tilemap.HasTile(temp))
        {
            oldPosition.x += 0.5f;
            oldPosition.y += 0.25f;
        }
        else
        {
        }
    }

    public void Left()
    {
        Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y + 0.25f);
        Vector3Int temp = tilemap.WorldToCell(nextPos);
        if (tilemap.HasTile(temp))
        {
            oldPosition.x += -0.5f;
            oldPosition.y += 0.25f;
        }
        else
        {
        }
    }

    public void Right()
    {
        Vector2 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y - 0.25f);
        Vector3Int temp = tilemap.WorldToCell(nextPos);
        if (tilemap.HasTile(temp))
        {
            oldPosition.x += 0.5f;
            oldPosition.y += -0.25f;
        }
        else
        {
        };
    }
}
