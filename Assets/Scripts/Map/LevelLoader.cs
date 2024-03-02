using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private GameObject left;
    private GameObject top;
    private GameObject right;
    private GameObject bottom;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        left = GameObject.FindWithTag("leftDoor");
        top = GameObject.FindWithTag("topDoor");
        right = GameObject.FindWithTag("rightDoor");
        bottom = GameObject.FindWithTag("bottomDoor");

        // assign doors their loaders
        assignDoors();
        positionPlayer();
    }

    private void assignDoors()
    {
        Level curr = GameManager.curr;

        if(curr.getLeft() != null)
        {
            left.GetComponent<DoorLoader>().nextRoom = curr.getLeft();
        }
        else
        {
            left.SetActive(false);
        }

        if(curr.getUp() != null)
        {
            top.GetComponent<DoorLoader>().nextRoom = curr.getUp();
        }
        else
        {
            top.SetActive(false);
        }

        if (curr.getRight() != null)
        {
            right.GetComponent<DoorLoader>().nextRoom = curr.getRight();
        }
        else
        {
            right.SetActive(false);
        }

        if (curr.getDown() != null)
        {
            bottom.GetComponent<DoorLoader>().nextRoom = curr.getDown();
        }
        else
        {
            bottom.SetActive(false);
        }
    }

    private void positionPlayer()
    {
        // change position based on
        if(GameManager.fromDoor == "leftDoor")
        {
            player.position = left.transform.position + new Vector3(0.6f, -0.4f, 0f);  
        } else if (GameManager.fromDoor == "topDoor")
        {
            player.position = top.transform.position + new Vector3(-0.6f, -0.4f, 0f);
        } else if (GameManager.fromDoor == "rightDoor")
        {
            player.position = right.transform.position + new Vector3(-0.6f, 0.4f, 0f);
        } else if (GameManager.fromDoor == "bottomDoor")
        {
            player.position = bottom.transform.position + new Vector3(0.6f, 0.4f, 0f);
        }
    }
}
