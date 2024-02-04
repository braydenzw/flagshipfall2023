using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        // camera follows player but maintains z posiiton
        Vector3 pos = player.transform.position;
        pos = new Vector3(pos.x, pos.y, this.transform.position.z);

        this.transform.position = pos;
    }
}
