using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float cooldown = 5f;
    private float timer;

    public Tilemap spawnArea;
    public float range = 2f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // spawning on some interval
        if(timer > cooldown)
        {
            spawnEnemy();
            timer = 0f;
        }

        if(timer <= cooldown)
        {
            timer += Time.deltaTime;
        }
    }

    private void spawnEnemy()
    {
        // get the tilemap as 2d array
        BoundsInt b = spawnArea.cellBounds;
        // initialize some position within the tilemap array
        int x = Random.Range(b.min.x, b.max.x);
        int y = Random.Range(b.min.y, b.max.y);
        Vector3Int pos = new Vector3Int(x, y, 0);

        // if the spawn position is a valid tile in the spawnArea tilemap
            // AND is not too close to player, instantiate new enemy
        while (!spawnArea.HasTile(pos) || Vector3.Distance(spawnArea.CellToWorld(pos), player.transform.position) < range)
        {
            // get new spawnpoint until conditions are met
            pos.x = Random.Range(b.min.x, b.max.x);
            pos.y = Random.Range(b.min.y, b.max.y);
        }

        // position as location within actual game bounds
            // converting tilemap location to real position vector
        Instantiate(enemy, spawnArea.CellToWorld(pos), Quaternion.identity);
    }
}
