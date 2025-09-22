using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player;           // �÷��̾� Transform
    public float smoothSpeed = 5f;     // ī�޶� ������� �ӵ�

    [Header("ī�޶� X �̵� ����")]
    public float minX = -10f;
    public float maxX = 10f;

    private float initialY;
    private float initialZ;

    void Start()
    {
        // ������ �� Y, Z ��ġ ����
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // �÷��̾� X�� ����
        float targetX = Mathf.Clamp(player.position.x, minX, maxX);

        Vector3 targetPosition = new Vector3(targetX, initialY, initialZ);

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
