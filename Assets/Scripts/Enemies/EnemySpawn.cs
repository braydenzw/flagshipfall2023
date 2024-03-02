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
        BoundsInt b = spawnArea.cellBounds;
        int x = Random.Range(b.min.x, b.max.x);
        int y = Random.Range(b.min.y, b.max.y);

        Vector3Int pos = new Vector3Int(x, y, 0);
        while (!spawnArea.HasTile(pos) || Vector3.Distance(spawnArea.CellToWorld(pos), player.transform.position) < range)
        {
            pos.x = Random.Range(b.min.x, b.max.x);
            pos.y = Random.Range(b.min.y, b.max.y);
        }

        Instantiate(enemy, spawnArea.CellToWorld(pos), Quaternion.identity);
        Debug.Log("spawned!");
    }
}
