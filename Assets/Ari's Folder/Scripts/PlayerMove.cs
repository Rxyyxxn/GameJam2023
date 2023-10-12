using Unity.VisualScripting;
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
    private Vector3Int playerPosOffset;
    [SerializeField] public GameObject raycastOBJ;
    public static PlayerMove instance;
    public Vector3Int nextPlayerTilePos;


    public EnemyManager em;

    bool up, down, left, right;

    public int attakDmg;
    public int HP;
    public int goldCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        invUI = GameObject.Find("InventoryUI");

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();
        tilemap.CellToWorld(tilemap.WorldToCell(transform.position));
        playerPosOffset = new Vector3Int(-1, -1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //Initialise Tilemap
        if(tilemap!=null)
        {

            Vector3Int pos = tilemap.LocalToCell(transform.position) + playerPosOffset;
        }
        else
        {
            tilemap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();

        }

        gameObject.transform.position = oldPosition;

        nextPlayerTilePos = tilemap.WorldToCell(oldPosition);

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (invUI.activeSelf)
            {
                //for (int i = 0; i < InventorySystem.current.inventory.Count; i++)
                //{
                //    if (InventorySystem.current.inventory[i].data.displayName == "Carrot0")
                //    {
                //        InventorySystem.current.Remove(InventorySystem.current.inventory[i].data);
                //    }
                //}

                Debug.Log(InventorySystem.current.ItemCount("Carrot0"));
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            invUI.SetActive(!invUI.activeSelf);
        }

        //PC Movement 
        {
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
                    up = true;
                }
                if (endPos.x < startPos.x && endPos.y < startPos.y)
                {
                    Down();
                    down = true;
                }
                if (endPos.x < startPos.x && endPos.y > startPos.y)
                {
                    Left();
                    left = true;
                }
                if (endPos.x > startPos.x && endPos.y < startPos.y)
                {
                    Right();
                    right = true;
                }
                
            }
            if (up)
            {
                Vector3 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y + 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos);

                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition);
                if (temp == ifenfenf)
                {
                    up = false;
                }

            }
            if (down)
            {
                Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y - 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos);

                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition);
                if (temp == ifenfenf)
                {
                    down = false;
                }

            }
            if (left)
            {
                Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y + 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos);

                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition);
                if (temp == ifenfenf)
                {
                    left = false;
                }

            }
            if (right)
            {
                Vector3 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y - 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos);


                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition);
                if (temp == ifenfenf)
                {
                    right = false;
                }

            }
        }

    }
    public void Down()
    {
        Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y - 0.25f);

        Vector3Int temp = tilemap.WorldToCell(nextPos);
        if (em != null)
        {
            for (int i = 0; i < em.all_enemyData.Length; i++)
            {
                if (em.all_enemyData[i].enemyVec3 == temp)
                {
                    em.all_enemyData[i].EnemyHP--;
                    return;
                }
            }
        }


        if (tilemap.HasTile(temp))
        {
            //oldPosition = Vector3.Slerp(oldPosition, nextPos, 2f * Time.deltaTime);
            //oldPosition.x += -0.5f;
            //oldPosition.y += -0.25f;
            down = true;

        }
    }

    public void Up()
    {
        Vector2 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y + 0.25f);
        Vector3Int temp = tilemap.WorldToCell(nextPos);

        if (em != null)
        {
            for (int i = 0; i < em.all_enemyData.Length; i++)
            {
                if (em.all_enemyData[i].enemyVec3 == temp)
                {
                    em.all_enemyData[i].EnemyHP--;
                    return;
                }
            }
        }

        if (tilemap.HasTile(temp))
        {
            //oldPosition = Vector3.Slerp(oldPosition, nextPos, 2f * Time.deltaTime);

            //oldPosition.x += 0.5f;
            //oldPosition.y += 0.25f;
            up = true;

        }
        else
        {

        }
    }

    public void Left()
    {
        Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y + 0.25f);
        Vector3Int temp = tilemap.WorldToCell(nextPos);

        if (em != null)
        {
            for (int i = 0; i < em.all_enemyData.Length; i++)
            {
                if (em.all_enemyData[i].enemyVec3 == temp)
                {
                    em.all_enemyData[i].EnemyHP--;
                    return;
                }
            }
        }

        if (tilemap.HasTile(temp))
        {
            //oldPosition = Vector3.Slerp(oldPosition, nextPos, 2f * Time.deltaTime);

            //oldPosition.x += -0.5f;
            //oldPosition.y += 0.25f;
            left = true;

        }
        else
        {

        }
    }

    public void Right()
    {
        Vector2 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y - 0.25f);
        Vector3Int temp = tilemap.WorldToCell(nextPos);

        if (em != null)
        {
            for (int i = 0; i < em.all_enemyData.Length; i++)
            {
                if (em.all_enemyData[i].enemyVec3 == temp)
                {
                    em.all_enemyData[i].EnemyHP--;
                    return;
                }
            }
        }

        if (tilemap.HasTile(temp))
        {
            //oldPosition = Vector3.Slerp(oldPosition, nextPos, 2f * Time.deltaTime);

            //oldPosition.x += 0.5f;
            //oldPosition.y += -0.25f;
            right = true;

        }
        else
        {
        };
    }

    public Vector3Int GetPlayerTilePos()
    {
        return tilemap.WorldToCell(transform.position);
    }
}
