using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public int EnemyHP;
    public int EnemyAtk;
    public int EnemyTile;

    private EnemyState enemyState;
    private Direction enemyDirection;

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
    }

    // Update is called once per frame
    void Update()
    {
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
}
