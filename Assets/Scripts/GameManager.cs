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
        // object should be constant throughout session
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        createMap();
        connectDoors();
        ensureEnd();

        printMap();

        // assign start
        curr = map[0, 2];
        fromDoor = "bottomDoor";

        DontDestroyOnLoad(gameObject);
    }

    // so other scripts can access these vars
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
        // recursively checks that the end can be reached
        List<Level> l = new List<Level>();
        bool pathExists = checkDoor(map[0, 2], l);
        while (!pathExists)
        {
            // greedy implementation (sorry)
                // if no path, we will just repeat the ENTIRE
                // process until there is one

            // better way to do this would be to create a path
                // if no path exists, but I don't feel like doing
                // that rn
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

        // add to visited list
        l.Add(curr);
        Level left = curr.getLeft();
        Level up = curr.getUp();
        Level right = curr.getRight();
        Level down = curr.getDown();

        // check valid paths recurisvely
        if(left != null && !l.Contains(left) && checkDoor(left, l))
        {
            return true;
        } else if (up != null && !l.Contains(up) && checkDoor(up, l))
        {
            return true;
        } else if (right != null && !l.Contains(right) && checkDoor(right, l))
        {
            return true;
        } else if (down != null && !l.Contains(down) && checkDoor(down, l))
        {
            return true;
        }

        // no valid paths -> return false up recursion stack
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

            // just check if there are valid paths
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

        // create 2d array of random rooms
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

        // make sure start is a valid room
        if (map[0, 2] == null)
        {
            map[0, 2] = new Level(levelScenes[Random.Range(0, levelScenes.Length)]);
        }

        // make sure end is a valid room
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
