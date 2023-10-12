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
    private int OriginalAtkDmg;
    public int HP;
    public int goldCount;
    int steps;

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
        OriginalAtkDmg = attakDmg;
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
                    em.all_enemyData[i].EnemyHP -= attakDmg;
                    attakDmg = OriginalAtkDmg;
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

            oldPosition.x += -0.5f;
            oldPosition.y += -0.25f;
            RollDice(0,6);
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
                    em.all_enemyData[i].EnemyHP -= attakDmg;
                    attakDmg = OriginalAtkDmg;
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

            oldPosition.x += 0.5f;
            oldPosition.y += 0.25f;
            RollDice(0,6);
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
                    em.all_enemyData[i].EnemyHP -= attakDmg;
                    attakDmg = OriginalAtkDmg;
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

            oldPosition.x += -0.5f;
            oldPosition.y += 0.25f;
            RollDice(0,6);
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
                    em.all_enemyData[i].EnemyHP -= attakDmg;
                    attakDmg = OriginalAtkDmg;
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

            oldPosition.x += 0.5f;
            oldPosition.y += -0.25f;
            RollDice(0,6);
        }
        else
        {
        };
    }

    public Vector3Int GetPlayerTilePos()
    {
        return tilemap.WorldToCell(transform.position);
    }

    public void RollDice(int dicetype, int MaxDieNum)
    {
        steps++;
        //basic die
        if (dicetype == 0)
        {
            if (steps == 1)
            {
                int dieNumber = Random.Range(1, MaxDieNum + 1);
                Debug.Log("die number : " + dieNumber);
                DefaultDiceBoost(dieNumber, MaxDieNum);
                steps = 0;
            }
        }
        //variant die
        else
        {
            if (steps == 10)
            {

            }
        }
        return;
    }

    public void DefaultDiceBoost(int dienum, int MaxDieNum)
    {
        attakDmg = OriginalAtkDmg;
        switch (MaxDieNum)
        {
            case 6:
                if (dienum <= 3)
                {
                    attakDmg += 1;
                }
                else if (dienum == 6)
                {
                    attakDmg += 3;
                }
                else
                {
                    attakDmg += 2;
                }
                break;
            case 8:
                if (dienum <= 3)
                {
                    attakDmg += 1;
                }
                else if (dienum >= 7)
                {
                    attakDmg += 3;
                }
                else
                {
                    attakDmg += 2;
                }
                break;
            case 10:
                if (dienum <= 3)
                {
                    attakDmg += 1;
                }
                else if (dienum >= 8)
                {
                    attakDmg += 3;
                }
                else
                {
                    attakDmg += 2;
                }
                break;
            case 12:
                if (dienum <= 3)
                {
                    attakDmg += 1;
                }
                else if (dienum >= 9)
                {
                    attakDmg += 3;
                }
                else
                {
                    attakDmg += 2;
                }
                break;
            default:
                break;
        }
    }
}
