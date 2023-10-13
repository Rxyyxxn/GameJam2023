using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Objects
{
    //for debug
    public string name;

    public GameObject Prefab;
    [Range(0, 100)] public int chance = 100;

    [HideInInspector] public double weight;
}
public class Spawner : MonoBehaviour
{
    [SerializeField] public Objects[] objects;
    public Tilemap tilemap;

    private double accumulatedWeights;
    private System.Random rand = new System.Random();

    public List<Vector3> availablePlaces;

    public int spawnAmount;

    private void Awake()
    {
        CalculateWeights();
    }
    void Start()
    {
        availablePlaces = new List<Vector3>();

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    availablePlaces.Add(place);
                }
            }
        }
        foreach (var item in availablePlaces)
        {
            Debug.Log(item);
        }
        //Test
        for (int i = 0; i < spawnAmount; i++)
        {
            int randomPos = Random.Range(0, availablePlaces.Count - 1);
            RandomSpawn(new Vector3(availablePlaces[randomPos].x, availablePlaces[randomPos].y + 0.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RandomSpawn(Vector3 tilePosition)
    {
        Objects randomObject = objects[GetRandomEnemyIndex()];
        Instantiate(randomObject.Prefab, tilePosition, Quaternion.identity, transform);
    }

    private int GetRandomEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].weight >= r)
            {
                return i;
            }
        }
        return 0;
    }

    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (Objects obj in objects)
        {
            accumulatedWeights += obj.chance;
            obj.weight = accumulatedWeights;
        }

    }

}
