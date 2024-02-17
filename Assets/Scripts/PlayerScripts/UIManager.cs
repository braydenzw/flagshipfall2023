using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI health;

    // Start is called before the first frame update
    void Start()
    {
        health.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        health.text = health.ToString();
    }
}
