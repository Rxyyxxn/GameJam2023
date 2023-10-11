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
        Attack
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
        enemyDirection = Direction.Up;
        enemyVec3 = tmap.WorldToCell(transform.position);
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

        Debug.Log("enemy "+enemyVec3);

        if (DetectionRange(playerGO.GetComponent<PlayerMove>().GetPlayerTilePos()))
        {
            Debug.Log("ISEEU");
        }

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
                break;
            case EnemyState.Chase:
                //pathfind to player
                break;
            case EnemyState.Attack:

                switch (enemyDirection)
                {
                    case Direction.Up:
                        break;
                    case Direction.Down:
                        break;
                    case Direction.Left:
                        break;
                    case Direction.Right:
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public bool DetectionRange(Vector3 pos)
    {
        int x = enemyVec3.x;
        int y = enemyVec3.y;
        int highestx = x + 4;
        int lowestx = x - 4;
        int highesty = y + 4;
        int lowesty = y - 4;

        if ((pos.x >= lowestx && pos.x <= highestx) && (pos.y >= lowesty && pos.y <= highesty))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
