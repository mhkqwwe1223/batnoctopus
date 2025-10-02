using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;       // 배 속도
    public GameObject player;          // 플레이어 (BoatRide에서 할당)

    private SpriteRenderer playerSprite;

    void Start()
    {
        if (player != null)
            playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = 0f;

        // A,D 키 입력만 받음
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector2 moveDirection = new Vector2(moveX, 0f);

        // 배 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 좌우 반전: 배 이동 방향에 맞춰 플레이어 반전
        if (playerSprite != null)
        {
            if (moveX < 0) playerSprite.flipX = true;
            else if (moveX > 0) playerSprite.flipX = false;
        }
    }
}
