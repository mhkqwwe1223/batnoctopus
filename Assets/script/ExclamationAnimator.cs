using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationAnimator : MonoBehaviour
{
    [Header("애니메이션 설정")]
    [SerializeField] private float popDuration = 0.3f; // 팝업 지속시간
    [SerializeField] private float bounceHeight = 0.2f; // 통통 튀는 높이
    [SerializeField] private float bounceSpeed = 3f; // 통통 튀는 속도
    [SerializeField] private AnimationCurve popCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // 팝업 커브
    
    private Vector3 originalScale;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;
    private bool isPopping = true;
    private float bounceOffset = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;
        
        // 시작할 때 스케일 0으로
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        // 팝업 애니메이션
        if (isPopping)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / popDuration);
            
            // 스케일 애니메이션 (0에서 1로 팝업)
            float scaleValue = popCurve.Evaluate(progress);
            transform.localScale = originalScale * scaleValue;
            
            // 약간의 오버슈팅 효과
            if (progress > 0.7f && progress < 1f)
            {
                float overshoot = Mathf.Sin((progress - 0.7f) * Mathf.PI * 3.33f) * 0.2f;
                transform.localScale = originalScale * (scaleValue + overshoot);
            }
            
            if (progress >= 1f)
            {
                isPopping = false;
                transform.localScale = originalScale;
            }
            
            // 팝업 중에도 타겟 위치로 이동
            transform.position = targetPosition;
        }
        else
        {
            // 팝업 후 통통 튀는 애니메이션
            bounceOffset += Time.deltaTime * bounceSpeed;
            float bounce = Mathf.Sin(bounceOffset) * bounceHeight;
            
            Vector3 pos = targetPosition;
            pos.y += Mathf.Abs(bounce); // 위아래로 통통
            transform.position = pos;
            
            // 스케일도 살짝 변화
            float scaleVariation = 1f + (Mathf.Sin(bounceOffset * 2f) * 0.05f);
            transform.localScale = originalScale * scaleVariation;
        }
    }
    
    // InteractionIndicator에서 위치 업데이트할 때 호출
    public void UpdateTargetPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }
}