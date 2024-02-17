using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private static Image Healthbar;
    public float currentHealth;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        Healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.Find("Player");
        PlayerStats player_Stats = _player.GetComponent<PlayerStats>();
        currentHealth = player_Stats.playerHealth;
        Healthbar.fillAmount = currentHealth;

        //Color greenHealth = new Color(0.6f, 1, 0.6f, 1);

        //if (currentHealth >= 0.3f)
        //{
        //    Healthbar.color = greenHealth;
        //}
        //else
        //{
        //    Healthbar.color = Color.red;
        //}
    }
}
