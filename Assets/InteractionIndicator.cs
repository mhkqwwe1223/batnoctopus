using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIndicator : MonoBehaviour
{
    [Header("느낌표 설정")]
    [SerializeField] private GameObject exclamationMarkPrefab; // 느낌표 이미지 프리팹
    [SerializeField] private Vector2 indicatorOffset = new Vector2(0, 1.5f); // 캐릭터 위쪽 오프셋
    
    private GameObject currentIndicator;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = transform;
        
        // 디버깅: 스크립트 설정 확인
        Debug.Log($"[InteractionIndicator] 스크립트 시작 - 오브젝트: {gameObject.name}");
        
        if (exclamationMarkPrefab == null)
        {
            Debug.LogError($"[InteractionIndicator] 느낌표 프리팹이 할당되지 않았습니다!");
        }
        else
        {
            Debug.Log($"[InteractionIndicator] 느낌표 프리팹 할당됨: {exclamationMarkPrefab.name}");
        }
        
        // Collider 확인
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError($"[InteractionIndicator] {gameObject.name}에 Collider2D가 없습니다!");
        }
        else
        {
            Debug.Log($"[InteractionIndicator] Collider2D 확인: {col.GetType().Name}, IsTrigger: {col.isTrigger}");
            if (!col.isTrigger)
            {
                Debug.LogWarning($"[InteractionIndicator] Collider2D의 IsTrigger가 체크되지 않았습니다!");
            }
        }
        
        // Rigidbody2D 확인
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning($"[InteractionIndicator] Rigidbody2D가 없습니다. 충돌 감지가 안될 수 있습니다!");
        }
        else
        {
            Debug.Log($"[InteractionIndicator] Rigidbody2D 확인: BodyType={rb.bodyType}");
        }
    }

    void Update()
    {
        // 느낌표가 활성화되어 있으면 매 프레임마다 캐릭터를 따라다니게 함
        if (currentIndicator != null && currentIndicator.activeSelf)
        {
            UpdateIndicatorPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[OnTriggerEnter2D] 충돌 감지! 오브젝트: {collision.gameObject.name}, 태그: {collision.tag}");
        
        // 상호작용 가능한 오브젝트와 충돌 시
        if (collision.CompareTag("Interactable"))
        {
            Debug.Log($"[OnTriggerEnter2D] Interactable 오브젝트 감지! 느낌표 표시");
            ShowIndicator();
        }
        else
        {
            Debug.LogWarning($"[OnTriggerEnter2D] {collision.gameObject.name}의 태그가 'Interactable'이 아닙니다. 현재 태그: {collision.tag}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"[OnTriggerExit2D] 충돌 종료! 오브젝트: {collision.gameObject.name}");
        
        // 상호작용 가능한 오브젝트에서 벗어날 시
        if (collision.CompareTag("Interactable"))
        {
            Debug.Log($"[OnTriggerExit2D] Interactable 오브젝트에서 벗어남. 느낌표 숨김");
            HideIndicator();
        }
    }

    private void ShowIndicator()
    {
        Debug.Log($"[ShowIndicator] 느낌표 표시 시도");
        
        if (currentIndicator == null && exclamationMarkPrefab != null)
        {
            currentIndicator = Instantiate(exclamationMarkPrefab);
            UpdateIndicatorPosition();
            
            Debug.Log($"[ShowIndicator] 느낌표 생성 완료! 위치: {currentIndicator.transform.position}");
            
            // 추가 디버깅: 느낌표 상태 확인
            SpriteRenderer sr = currentIndicator.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Debug.Log($"[ShowIndicator] SpriteRenderer - Sprite: {sr.sprite?.name}, Enabled: {sr.enabled}, SortingLayer: {sr.sortingLayerName}, OrderInLayer: {sr.sortingOrder}");
                
                // Sorting Layer 자동 설정
                sr.sortingLayerName = "Default";
                sr.sortingOrder = 1000; // 최상위로 설정
                Debug.Log($"[ShowIndicator] SortingOrder를 1000으로 설정");
            }
            else
            {
                Debug.LogError($"[ShowIndicator] 느낌표 프리팹에 SpriteRenderer가 없습니다!");
            }
            
            // Canvas 확인 (UI 이미지인 경우)
            Canvas canvas = currentIndicator.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingOrder = 1000;
                Debug.Log($"[ShowIndicator] Canvas sortingOrder를 1000으로 설정");
            }
            
            // 스케일 확인
            Debug.Log($"[ShowIndicator] 느낌표 스케일: {currentIndicator.transform.localScale}");
            
            // 활성화 상태 확인
            if (!currentIndicator.activeSelf)
            {
                currentIndicator.SetActive(true);
                Debug.LogWarning($"[ShowIndicator] 느낌표가 비활성화 상태였습니다. 활성화 완료");
            }
        }
        else if (currentIndicator != null)
        {
            Debug.LogWarning($"[ShowIndicator] 느낌표가 이미 표시되어 있습니다.");
        }
        else if (exclamationMarkPrefab == null)
        {
            Debug.LogError($"[ShowIndicator] 느낌표 프리팹이 null입니다!");
        }
    }

    private void HideIndicator()
    {
        Debug.Log($"[HideIndicator] 느낌표 숨김 시도");
        
        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
            currentIndicator = null;
            Debug.Log($"[HideIndicator] 느낌표 제거 완료");
        }
        else
        {
            Debug.LogWarning($"[HideIndicator] 제거할 느낌표가 없습니다.");
        }
    }

    private void UpdateIndicatorPosition()
    {
        if (currentIndicator == null) return;
        
        // 캐릭터가 바라보는 방향 고려
        Vector3 position = playerTransform.position;
        
        // 캐릭터의 스케일로 방향 판단 (일반적인 2D 게임 방식)
        float direction = playerTransform.localScale.x > 0 ? 1 : -1;
        
        position.x += indicatorOffset.x * direction;
        position.y += indicatorOffset.y;
        position.z = -1; // 카메라보다 앞으로 (2D에서 중요!)
        
        // ExclamationAnimator가 있으면 타겟 위치 업데이트
        ExclamationAnimator animator = currentIndicator.GetComponent<ExclamationAnimator>();
        if (animator != null)
        {
            animator.UpdateTargetPosition(position);
            //Debug.Log($"[UpdateIndicatorPosition] 타겟 위치 업데이트: {position}");
        }
        else
        {
            currentIndicator.transform.position = position;
            Debug.LogWarning($"[UpdateIndicatorPosition] ExclamationAnimator가 없습니다!");
        }
    }

    void OnDestroy()
    {
        // 캐릭터가 파괴될 때 느낌표도 제거
        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }
    }
}