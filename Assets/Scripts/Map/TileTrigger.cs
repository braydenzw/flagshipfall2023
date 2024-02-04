using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileTrigger : MonoBehaviour
{
    public CompositeCollider2D col;
    public int caseNum;
    public string scene;
    public GameObject popup;

    private bool end;

    private void Start()
    {
        col = this.GetComponent<CompositeCollider2D>();
        popup.SetActive(false);
        end = false;
    }

    private void Update()
    {
        // if prompt active and key event, can load next scene
        if(Input.GetKeyDown(KeyCode.Return) && popup.activeSelf)
        {
            end = true;
            SceneManager.LoadScene(scene);
        }
    }

    // activates when player enters trigger collider
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // may or may not need switch function...
        switch (caseNum)
        {
            case 0:
                if (!popup.activeSelf)
                {
                    popup.SetActive(true);
                }
                break;
            default:
                Debug.Log("Error: Case not recognized");
                break;
        }
    }

    // activates when player exits trigger collider
    public void OnTriggerExit2D(Collider2D collision)
    {
        // prevent loadscene call which causes error
        if (end)
        {
            return;
        }

        switch (caseNum)
        {
            case 0:
                if (popup.activeSelf)
                {
                    popup.SetActive(false);
                }
                break;
            default:
                Debug.Log("Error: Case not recognized");
                break;
        }
    }
}
