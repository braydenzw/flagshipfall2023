using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float playerHealth = 1.0f;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 1.0f;
        _uiManager= GameObject.Find("Player Stats Overlay").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // manual health changes for testing
        if (Input.GetKey("space"))
        {
            playerHealth -= 0.001f;
        }
    }

    

}
