using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;                 // 이동 속도

    [Header("애니메이션 (선택사항)")]
    public Animator animator;

    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        UpdateAnimations();
    }

    void FixedUpdate() // ← 물리 업데이트 주기마다 실행
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void HandleInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        moveDirection = new Vector2(moveX, moveY).normalized;

        // 좌우 방향 전환 시 스프라이트 뒤집기
        if (moveX < 0) spriteRenderer.flipX = true;
        else if (moveX > 0) spriteRenderer.flipX = false;
    }

    void Move()
    {
        if (moveDirection == Vector2.zero) return;

        Vector2 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        Vector2 targetPos = rb.position + movement;
        rb.MovePosition(targetPos);
    }

    void UpdateAnimations()
    {
        if (animator == null) return;

        animator.SetBool("IsMoving", moveDirection != Vector2.zero);
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
    }
}
