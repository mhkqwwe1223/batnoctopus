using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tptoforest1 : MonoBehaviour
{
    public Transform forest1; // 스폰 포인트 설정
    [SerializeField] float newMinX;
    [SerializeField] float newMaxX;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            player.transform.position = forest1.position;

            CameraFollowX cam = Camera.main.GetComponent<CameraFollowX>();
            cam.minX = newMinX;
            cam.maxX = newMaxX;
        }
    }
}
