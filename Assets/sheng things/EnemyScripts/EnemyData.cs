using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps; 

public class EnemyData : MonoBehaviour
{
    public int EnemyHP;
    public int EnemyAtk;
    public int EnemyTile;
    private EnemyState enemyState;
    private Direction enemyDirection;
    private Direction RayCastDir;

    public Tilemap tmap;

    public Vector3Int enemyVec3;

    public Slider slider;

    public GameObject playerGO;

    float movetimer = 0.0f;

    public EnemyManager em;

    bool iteminfront = false;

    bool Moved = false;

    bool movable = true;

    public float attackPauseTimer;
    float timer = 0.0f;

    public AudioClip enemyAtkSound;
    public AudioClip enemyDieSound;
    private AudioSource currentSound;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        AttackPause
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
        enemyDirection = Direction.Up;
        enemyVec3 = tmap.WorldToCell(transform.position);
        transform.position = tmap.CellToWorld(enemyVec3);
        currentSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = EnemyHP;

        if (EnemyHP <= 0)
        {
            Death();
        }

        enemyVec3 = tmap.WorldToCell(transform.position);

        //Debug.Log("enemy "+enemyVec3);

        //DetectionRange(playerGO.GetComponent<PlayerMove>().GetPlayerTilePos());

        //if player in enemy detection range
        //run raycast
        for (int i = 0; i < 4; i++)
        {
            Vector2 dir;

            switch (i)
            {
                case 0:
                    dir = new Vector2(2.25f,1f);
                    RayCastDir = Direction.Up;
                    break;
                case 1:
                    dir = new Vector2(-1.75f, -1f) * 1.25f;
                    RayCastDir = Direction.Down;
                    break;
                case 2:
                    dir = new Vector2(-5f, 2.25f) / 2.35f;
                    RayCastDir = Direction.Left;
                    break;
                case 3:
                    dir = new Vector2(5f, -2.75f) / 2.35f;
                    RayCastDir = Direction.Right;
                    break;
                default:
                    dir = new Vector2(2.25f, 1f);
                    RayCastDir = Direction.Up;
                    break;
            }

            
            Debug.DrawRay(transform.position, transform.TransformDirection(dir), Color.red);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(dir), 10f);

            //if (hit)
            //{
            //    Debug.Log("Hit " + hit.collider.name);
            //}
        }
        //raycast end
        
        switch (enemyState)
        {
            case EnemyState.Idle:
                DetectionRange(playerGO.GetComponent<PlayerMove>().GetPlayerTilePos());
                break;
            case EnemyState.Chase:
                //pathfind to player
                //ChaseTarget(playerGO.GetComponent<PlayerMove>().GetPlayerTilePos());
                break;
            case EnemyState.Attack:
                if (
                    enemyVec3.x == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().x && enemyVec3.y == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().y - 1 
                    ||
                    enemyVec3.x == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().x && enemyVec3.y == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().y + 1
                    ||
                    enemyVec3.y == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().y && enemyVec3.x == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().x + 1
                    ||
                    enemyVec3.y == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().y && enemyVec3.x == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos().x - 1
                    )
                {
                    currentSound.clip = enemyAtkSound;
                    currentSound.Play();
                    playerGO.GetComponent<PlayerMove>().HP -= EnemyAtk;
                    enemyState = EnemyState.AttackPause;
                }
                else
                {
                    enemyState = EnemyState.Idle;
                }
                break;
            case EnemyState.AttackPause:
                timer += Time.deltaTime;
                if (timer > attackPauseTimer)
                {
                    timer = 0.0f;
                    enemyState = EnemyState.Attack;
                }
                break;
            default:
                break;
        }
        
    }

    void Death()
    {
        currentSound.clip = enemyDieSound;
        currentSound.Play();
        playerGO.GetComponent<PlayerMove>().goldCount += 10;
        Destroy(gameObject);
    }

    public void DetectionRange(Vector3Int pos)
    {
        int x = enemyVec3.x;
        int y = enemyVec3.y;
        int highestx = x + 4;
        int lowestx = x - 4;
        int highesty = y + 4;
        int lowesty = y - 4;

        if ((pos.x >= lowestx && pos.x <= highestx) && (pos.y >= lowesty && pos.y <= highesty))
        {
            if (movable)
            {
                ChaseTarget(pos);
            }
        }
        else
        {
            return;
        }

    }

    public void ChaseTarget(Vector3Int pos)
    {
        
        int xDiff = enemyVec3.x - pos.x;
        int yDiff = enemyVec3.y - pos.y;
        

        if (xDiff < 0)
        {
            xDiff *= -1;
        }

        if (yDiff < 0)
        {
            yDiff *= -1;
        }

        //if player is right infront of enemy
        if ((yDiff == 0 && xDiff == 1) || (yDiff == 1 && xDiff == 0))
        {
            enemyState = EnemyState.AttackPause;
            return;
        }
        else if (yDiff != 0)
        {
            //moveY
            yDiff = enemyVec3.y - pos.y;
            if (yDiff < 0)
            {
                //move up
                movetimer += Time.deltaTime;
                if (movetimer > 1f)
                {
                    Vector3 nextPos = new Vector3(transform.position.x - 0.5f, transform.position.y + .25f, transform.position.z);
                    Vector3Int nextTilePos = tmap.WorldToCell(nextPos);
                    for (int i = 0; i < em.all_ItemData.Length; i++)
                    {
                        if (em.all_ItemData[i].itemVec3 == nextTilePos)
                        {
                            if (!Moved)
                            {
                                Moved = true;

                                StartCoroutine("walkAround", 1);
                            }

                            return;
                        }
                    }
                    //enemyVec3.y++;
                    transform.position = nextPos;

                    movetimer = .0f;
                    return;
                }
            }
            else if (yDiff > 0)
            {
                //move down
                movetimer += Time.deltaTime;
                if (movetimer > 1f)
                {
                    Vector3 nextPos = new Vector3(transform.position.x + 0.5f, transform.position.y - .25f, transform.position.z);
                    Vector3Int nextTilePos = tmap.WorldToCell(nextPos);
                    for (int i = 0; i < em.all_ItemData.Length; i++)
                    {
                        if (em.all_ItemData[i].itemVec3 == nextTilePos)
                        {
                            if (!Moved)
                            {
                                Moved = true;

                                StartCoroutine("walkAround", 0);
                            }
                            return;
                        }
                    }
                    //enemyVec3.y++;
                    transform.position = nextPos;
                    movetimer = .0f;    
                    return;
                }
            }
        }
        else if (yDiff == 0)
        {
            xDiff = enemyVec3.x - pos.x;
            if (xDiff < 0)
            {
                //move right
                movetimer += Time.deltaTime;
                if (movetimer > 1f)
                {
                    Vector3 nextPos = new Vector3(transform.position.x + 0.5f, transform.position.y + .25f, transform.position.z);
                    Vector3Int nextTilePos = tmap.WorldToCell(nextPos);
                    for (int i = 0; i < em.all_ItemData.Length; i++)
                    {
                        if (em.all_ItemData[i].itemVec3 == nextTilePos)
                        {
                            //StartCoroutine("walkAround", false);
                            if (!Moved)
                            {
                                Moved = true;
                                

                                StartCoroutine("walkAround" , 1);
                            }
                            return;
                        }
                    }
                    //enemyVec3.y++;
                    transform.position = nextPos;
                    movetimer = .0f;
                    return;
                }
            }
            else if (xDiff > 0)
            {
                //move left
                movetimer += Time.deltaTime;
                if (movetimer > 1f)
                {
                    Vector3 nextPos = new Vector3(transform.position.x - 0.5f, transform.position.y - .25f, transform.position.z);
                    Vector3Int nextTilePos = tmap.WorldToCell(nextPos);
                    for (int i = 0; i < em.all_ItemData.Length; i++)
                    {
                        if (em.all_ItemData[i].itemVec3 == nextTilePos)
                        {
                            if (!Moved)
                            {
                                Moved = true;
                                

                                StartCoroutine("walkAround" , 0);
                            }
                            return;
                        }
                    }
                    transform.position = nextPos;
                    movetimer = .0f;
                    return;
                }
            }
        }
        //Debug.Log("xdiff : " + xDiff + " ,ydiff " + yDiff);
    }

    IEnumerator walkAround(int p)
    {
        Vector3 nextPos;
        Vector3Int nextTilePos;
        movable = false;

        if (p == 1)
        {
            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x + 0.5f, transform.position.y - .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;

            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x + 0.5f, transform.position.y + .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;


            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x + 0.5f, transform.position.y + .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;

            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x - 0.5f, transform.position.y + .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;
        }

        else
        {
            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x - 0.5f, transform.position.y + .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;

            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x - 0.5f, transform.position.y - .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;


            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x - 0.5f, transform.position.y - .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;

            yield return new WaitForSeconds(1);

            nextPos = new Vector3(transform.position.x + 0.5f, transform.position.y - .25f, transform.position.z);
            nextTilePos = tmap.WorldToCell(nextPos);
            if (nextTilePos == playerGO.GetComponent<PlayerMove>().GetPlayerTilePos())
            {
                Moved = false;
                movable = true;
                yield break;
            }
            transform.position = nextPos;
        }

        Moved = false;
        movable = true;
    }
}
