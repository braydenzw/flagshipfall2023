using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLoader : MonoBehaviour
{
    // should be set by level loader/manager
    public Level nextRoom;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (nextRoom == null)
            {
                Debug.Log("ERROR: No next room found.");
            }
            else
            {
                // make sure to maintain GameManager vars
                GameManager.fromDoor = this.tag;
                GameManager.curr = nextRoom;

                // then load new room
                    // there might be a more efficent way to do this than
                    // scene loading...
                SceneManager.LoadScene(nextRoom.name);
            }
        }
    }
}
