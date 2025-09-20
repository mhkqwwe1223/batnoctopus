using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player;           // 플레이어 Transform
    public float smoothSpeed = 5f;     // 카메라 따라오는 속도

    [Header("카메라 X 이동 범위")]
    public float minX = -10f;
    public float maxX = 10f;

    private float initialY;
    private float initialZ;

    void Start()
    {
        // 시작할 때 Y, Z 위치 고정
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 플레이어 X만 따라감
        float targetX = Mathf.Clamp(player.position.x, minX, maxX);

        Vector3 targetPosition = new Vector3(targetX, initialY, initialZ);

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
