using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject invUI;
    public GameObject invenBar;
    public Transform invenBarTr;
    //PC
    private Vector2 oldPosition;
    //Mobile
    public Vector2 startPos;
    public Vector2 endPos;
    private Vector3Int playerPosOffset;
    [SerializeField] public GameObject raycastOBJ;
    public static PlayerMove instance;
    public Vector3Int nextPlayerTilePos;

    Animator animator;


    public EnemyManager em;

    bool up, down, left, right;

    public int attakDmg;
    private int OriginalAtkDmg;
    public int HP;
    public int goldCount;
    int steps;

    public AudioSource currentSound;
    public AudioClip playerMove;
    public AudioClip playerAtk;
    public AudioClip playerCollectItem;
    public AudioClip playerEnd;
    Animator diceAnim;

    public GameObject dice;


    public bool unactiveInvUI = false;

    private bool InvenUIopen = false;

    private float openUItimer = 0.0f;

    private bool theOtherWay = false;

    float minX = 3f;
    float maxX = 27f;

    bool runOnce = false;
    public Button closeButton;

    GameObject UIConnector;

    public GameObject shopGuide;
    public GameObject questGuide;
    public GameObject shopCanvas;
    public GameObject questCanvas;

    bool comebackfromscene4 = false;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();
        tilemap.CellToWorld(tilemap.WorldToCell(transform.position));
        invUI = GameObject.FindGameObjectWithTag("InventoryUI");
        invenBar = GameObject.FindGameObjectWithTag("InventoryBar");
        invenBarTr = invenBar.transform;
        em = GameObject.FindObjectOfType<EnemyManager>();
        playerPosOffset = new Vector3Int(-1, -1, 0);
        OriginalAtkDmg = attakDmg;
        animator = gameObject.GetComponent<Animator>();
        currentSound = GetComponent<AudioSource>();
        //invUI.transform.Translate(175, 0, 0);
        UIConnector = GameObject.FindGameObjectWithTag("OpenBtn");
        closeButton = GameObject.FindGameObjectWithTag("CloseBtn").GetComponent<Button>();
        Button CloseButton = closeButton.GetComponent<Button>();
        CloseButton.onClick.AddListener(OpenInvenUI);
        //UIConnector.transform.position = new Vector3(265, 200, UIConnector.transform.position.z);
        UIConnector.transform.localPosition = new Vector3(27, 0 , UIConnector.transform.localPosition.z);
        dice = GameObject.FindGameObjectWithTag("Dice");
        diceAnim = dice.GetComponent<Animator>();
        shopGuide = GameObject.FindGameObjectWithTag("Shop");
        questGuide = GameObject.FindGameObjectWithTag("Quest");
        shopCanvas = GameObject.FindGameObjectWithTag("ShopCanvas");
        questCanvas = GameObject.FindGameObjectWithTag("QuestCanvas");
    }



    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            comebackfromscene4 = true;
            tilemap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();
            em = GameObject.FindObjectOfType<EnemyManager>();
            UIConnector = GameObject.FindGameObjectWithTag("OpenBtn");
            closeButton = GameObject.FindGameObjectWithTag("CloseBtn").GetComponent<Button>();
            Button CloseButton = closeButton.GetComponent<Button>();
            CloseButton.onClick.AddListener(OpenInvenUI);
            invUI = GameObject.FindGameObjectWithTag("InventoryUI");
            invenBar = GameObject.FindGameObjectWithTag("InventoryBar");
            invenBarTr = invenBar.transform;
            minX = 30f;
            maxX = 210f;
            if (!runOnce)
            {
                UIConnector.transform.localPosition = new Vector3(210, 0, UIConnector.transform.localPosition.z);
                runOnce = true;
            }
        }

        if (comebackfromscene4 && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            comebackfromscene4 = false;
            minX = 3f;
            maxX = 27f;
            Start();
        }


            //Debug.Log(GetPlayerTilePos());

            //Initialise Tilemap
            if (tilemap!=null)
        {

            Vector3Int pos = tilemap.LocalToCell(transform.position) + playerPosOffset;
        }
        else
        {
            tilemap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();

        }
        if (dice == null)
        {
            dice = GameObject.FindGameObjectWithTag("Dice");
            diceAnim = dice.GetComponent<Animator>();
        }

        gameObject.transform.position = oldPosition;

        nextPlayerTilePos = tilemap.WorldToCell(oldPosition);

        if (Input.GetKeyDown(KeyCode.T))
        {
            OpenInvenUI();
        }

        //open or close inventory
        if (InvenUIopen)
        {
            Debug.Log("eg");
            if (!theOtherWay)
            {
                if (UIConnector.transform.localPosition.x > minX)
                {
                    UIConnector.transform.Translate(-1, 0, 0);
                }
                else
                {
                    InvenUIopen = false;
                    theOtherWay = true;
                }
            }
            else
            {
                if (UIConnector.transform.localPosition.x < maxX)
                {
                    UIConnector.transform.Translate(1, 0, 0);
                }
                else
                {
                    InvenUIopen = false;
                    theOtherWay = false;
                }
            }
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
            if (!up && !down && !left && !right)
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
            if (up)
            {
                Vector3 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y + 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos) - playerPosOffset;

                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition) - playerPosOffset;
                animator.SetBool("IsJumping", true);

                if (temp == ifenfenf)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetTrigger("IsReroll");


                    up = false;
                }

            }
            if (down)
            {
                Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y - 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos) - playerPosOffset;

                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition) - playerPosOffset;
                animator.SetBool("IsJumping", true);

                if (temp == ifenfenf)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetTrigger("IsReroll");


                    down = false;
                }

            }
            if (left)
            {
                Vector2 nextPos = new Vector2(oldPosition.x - 0.5f, oldPosition.y + 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos)-playerPosOffset;
                Debug.Log(temp);
                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition)-playerPosOffset;
                animator.SetBool("IsJumping", true);

                if (temp == ifenfenf)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetTrigger("IsReroll");


                    left = false;
                }

            }
            if (right)
            {
                Vector3 nextPos = new Vector2(oldPosition.x + 0.5f, oldPosition.y - 0.25f);
                Vector3Int temp = tilemap.WorldToCell(nextPos) - playerPosOffset;


                oldPosition = Vector3.Lerp(oldPosition, nextPos, 2f * Time.deltaTime);

                Vector3Int ifenfenf = tilemap.WorldToCell(oldPosition) - playerPosOffset;
                animator.SetBool("IsJumping", true);

                if (temp == ifenfenf)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetTrigger("IsReroll");


                    right = false;
                }

            }
        }

    }

    public void OpenInvenUI()
    {
        InvenUIopen = true;
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
                    currentSound.clip = playerAtk;
                    animator.SetTrigger("IsAttacking");

                    currentSound.Play();
                    return;
                }
            }
        }


        if (tilemap.HasTile(temp))
        {
            down = true;
            //oldPosition.x += -0.5f;
            //oldPosition.y +=- 0.25f;
            RollDice(0,6);
            currentSound.clip = playerMove;
            currentSound.Play();
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
                    Debug.Log("temp " + temp);
                    em.all_enemyData[i].EnemyHP -= attakDmg;
                    attakDmg = OriginalAtkDmg;
                    currentSound.clip = playerAtk;
                    currentSound.Play();
                    animator.SetTrigger("IsAttacking");
                    return;
                }
            }
        }

        if (tilemap.HasTile(temp))
        {
            up = true;
            //oldPosition.x += 0.5f;
            //oldPosition.y += 0.25f;
            RollDice(0,6);
            currentSound.clip = playerMove;
            currentSound.Play();
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
                    currentSound.clip = playerAtk;
                    animator.SetTrigger("IsAttacking");
                    currentSound.Play();
                    return;
                }
            }
        }

        if (tilemap.HasTile(temp))
        {
            left = true;
            //oldPosition.x += -0.5f;
            //oldPosition.y += 0.25f;
            RollDice(0,6);
            currentSound.clip = playerMove;
            currentSound.Play();
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
                    currentSound.clip = playerAtk;
                    animator.SetTrigger("IsAttacking");

                    currentSound.Play();
                    return;
                }
            }
        }

        if (tilemap.HasTile(temp))
        {
            right = true;
            //oldPosition.x += 0.5f;
            //oldPosition.y += -0.25f;
            RollDice(0,6);
            currentSound.clip = playerMove;
            currentSound.Play();
        }
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
                if (dieNumber == 1)
                {
                    diceAnim.SetTrigger("IfRoll1");
                }
                else if (dieNumber == 2)
                {
                    diceAnim.SetTrigger("IfRoll2");

                }
                else if (dieNumber == 3)
                {
                    diceAnim.SetTrigger("IfRoll3");

                }
                else if (dieNumber == 4)
                {
                    diceAnim.SetTrigger("IfRoll4");

                }
                else if (dieNumber == 5)
                {
                    diceAnim.SetTrigger("IfRoll5");

                }
                else if (dieNumber == 6)
                {
                    diceAnim.SetTrigger("IfRoll6");

                }
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
