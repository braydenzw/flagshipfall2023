using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private GameObject left;
    private GameObject top;
    private GameObject right;
    private GameObject bottom;

    public GameObject player;

    void Start()
    {
        left = GameObject.FindWithTag("leftDoor");
        top = GameObject.FindWithTag("topDoor");
        right = GameObject.FindWithTag("rightDoor");
        bottom = GameObject.FindWithTag("bottomDoor");

        assignDoors();
        positionPlayer();
    }

    private void assignDoors()
    {
        Level curr = GameManager.curr;

        // if the current level has a valid reference
            // make the trigger point to that reference
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
        // this isn't working for some reason...
        // player keeps reverting back to 0,0,0
        Vector3 pos = player.transform.position;

        // change position based on where the entry point was
        if (GameManager.fromDoor == "leftDoor")
        {
            pos = new Vector3(left.transform.position.x + 0.3f, left.transform.position.y - 0.2f, player.transform.position.z);  
        } else if (GameManager.fromDoor == "topDoor")
        {
            pos = new Vector3(top.transform.position.x - 0.3f, top.transform.position.y - 0.2f, player.transform.position.z);
        } else if (GameManager.fromDoor == "rightDoor")
        {
            pos = new Vector3(right.transform.position.x - 0.3f, right.transform.position.y + 0.2f, player.transform.position.z);
        } else if (GameManager.fromDoor == "bottomDoor")
        {
            pos = new Vector3(bottom.transform.position.x + 0.3f, bottom.transform.position.y + 0.2f, player.transform.position.z);
        }

        player.transform.position = pos;
    }
}
