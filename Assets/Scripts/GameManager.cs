using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[] levelScenes;
    private Level[,] map;

    public static string fromDoor;
    public static Vector2Int end;
    public static Level curr;

    public float mapEmptyRatio = 0.4f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        createMap();
        connectDoors();
        ensureEnd();

        printMap();
        curr = map[0, 2];
        fromDoor = "bottomDoor";

        DontDestroyOnLoad(gameObject);
    }

    public static Level getRoom()
    {
        return curr;
    }

    public static bool isEnd()
    {
        return curr.isEnd;
    }

    private void printMap()
    {
        string str = end.ToString();
        for (int i = 0; i < 25; i++)
        {
            if (i % 5 == 0)
            {
                str += '\n';
            }

            if (map[i / 5, i % 5] == null)
            {
                str += "|\t| ";
            }
            else if (i / 5 == end.x && i % 5 == end.y)
            {
                str += "| END. | ";
            }
            else if (i / 5 == 0 && i % 5 == 2)
            {
                str += "| STRT | ";
            }
            else
            {
                str += "| ROOM | ";
            }
        }

        Debug.Log(str);
    }

    private void ensureEnd()
    {
        List<Level> l = new List<Level>();
        bool pathExists = checkDoor(map[0, 2], l);
        while (!pathExists)
        {
            // greedy implementation (sorry)
            createMap();
            connectDoors();
            l = new List<Level>();
            pathExists = checkDoor(map[0, 2], l);
        }
    }

    private bool checkDoor(Level curr, List<Level> l)
    {
        // doesn't backtrack
        if(curr == map[end.x, end.y])
        {
            return true;
        }

        l.Add(curr);
        Level left = curr.getLeft();
        Level up = curr.getUp();
        Level right = curr.getRight();
        Level down = curr.getDown();

        if(left != null && !l.Contains(left))
        {
            return checkDoor(left, l);
        } else if (up != null && !l.Contains(up))
        {
            return checkDoor(up, l);
        } else if (right != null && !l.Contains(right))
        {
            return checkDoor(right, l);
        } else if (down != null && !l.Contains(down))
        {
            return checkDoor(down, l);
        }

        return false;
    }


    private void connectDoors()
    {
        int x, y;
        for(int i = 0; i < 25; i++)
        {
            x = i % 5;
            y = i / 5;

            if (map[y, x] == null)
            {
                continue;
            }

            // initializes all null
            Level[] doors = new Level[4];

            if(x > 0)
            {
                doors[0] = map[y, x - 1];
            }

            if(y < 4)
            {
                doors[1] = map[y + 1, x];
            }

            if(x < 4)
            {
                doors[2] = map[y, x + 1];
            }

            if(y > 0)
            {
                doors[3] = map[y - 1, x];
            }

            map[y, x].setDoors(doors);
        }
    }

    private void createMap()
    {
        map = new Level[5, 5];

        for (int i = 0; i < 25; i++)
        {
            if (Random.value > mapEmptyRatio)
            {   
                map[i / 5, i % 5] = new Level(levelScenes[Random.Range(0, levelScenes.Length)]);
            }
            else
            {
                map[i / 5, i % 5] = null;
            }
        }

        if (map[0, 2] == null)
        {
            map[0, 2] = new Level(levelScenes[Random.Range(0, levelScenes.Length)]);
        }

        end = new Vector2Int(Random.Range(2, 5), Random.Range(0, 5));

        if (map[end.x, end.y] == null)
        {
            map[end.x, end.y] = new Level(levelScenes[Random.Range(0, levelScenes.Length)]);
        }

        map[end.x, end.y].isEnd = true;
    }
}

public class Level
{
    public string name;
    // left, up, right, down
    private Level[] doors;
    public bool isEnd;

    public Level(string name)
    {
        this.name = name;
        doors = new Level[4];
        isEnd = false;
    }

    public void setDoors(Level[] doors)
    {
        this.doors = doors;
    }

    public Level getLeft()
    {
        return doors[0];
    }
    public Level getUp()
    {
        return doors[1];
    }
    public Level getRight()
    {
        return doors[2];
    }
    public Level getDown()
    {
        return doors[3];
    }
}
