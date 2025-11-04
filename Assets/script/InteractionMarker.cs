using UnityEngine;

public class InteractionMarker : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private Vector3 offsetFromTarget = new Vector3(0, 1.5f, 0); // 대상 위 오프셋
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobAmount = 0.2f; // World 좌표 기준

    private Transform target;
    private float bobTimer = 0f;
    private Vector3 initialOffset;

    void Start()
    {
        initialOffset = offsetFromTarget;
    }

    void Update()
    {
        if (target != null)
        {
            // 위아래로 흔들림 효과
            bobTimer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;

            // 월드 좌표에서 직접 위치 설정
            transform.position = target.position + initialOffset + new Vector3(0, bobOffset, 0);

            // 카메라를 향하도록 회전 (빌보드 효과)
            if (Camera.main != null)
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}