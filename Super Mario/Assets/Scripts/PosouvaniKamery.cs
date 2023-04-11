using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosouvaniKamery : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //Posouvání kamery pouze do prava s Mariem, kamera se nevrací už zpìt
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }
}
