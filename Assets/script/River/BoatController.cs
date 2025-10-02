using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;       // �� �ӵ�
    public GameObject player;          // �÷��̾� (BoatRide���� �Ҵ�)

    private SpriteRenderer playerSprite;

    void Start()
    {
        if (player != null)
            playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = 0f;

        // A,D Ű �Է¸� ����
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector2 moveDirection = new Vector2(moveX, 0f);

        // �� �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // �¿� ����: �� �̵� ���⿡ ���� �÷��̾� ����
        if (playerSprite != null)
        {
            if (moveX < 0) playerSprite.flipX = true;
            else if (moveX > 0) playerSprite.flipX = false;
        }
    }
}
