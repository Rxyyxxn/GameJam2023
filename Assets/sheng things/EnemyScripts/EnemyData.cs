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

    public Tilemap tmap;

    public Vector3Int enemyVec3;

    public Slider slider;

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

        Debug.Log(enemyVec3);

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
}
