using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLoader : MonoBehaviour
{
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
                GameManager.fromDoor = this.tag;
                GameManager.curr = nextRoom;

                SceneManager.LoadScene(nextRoom.name);
            }
        }
    }
}
