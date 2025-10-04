using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room1transport : MonoBehaviour
{
    public Transform spawnPoint2; // 스폰 포인트 설정
    [SerializeField] float newMinX;
    [SerializeField] float newMaxX;
    bool isPlayerNear = false;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if(isPlayerNear && Input.GetKeyDown(KeyCode.E)){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawnPoint2.position;

            CameraFollowX cam = Camera.main.GetComponent<CameraFollowX>();
            cam.minX = newMinX;
            cam.maxX = newMaxX;
        }
    }
}
