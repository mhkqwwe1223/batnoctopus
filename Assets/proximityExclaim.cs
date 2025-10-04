// ProximityExclaim.cs  (Player에 부착)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximityExclaim : MonoBehaviour
{
    [Header("Icon")]
    [SerializeField] Sprite exclaimSprite;     // 느낌표 PNG (Sprite)
    [SerializeField] Vector2 pixelOffset = new Vector2(0f, 24f); // 위로 몇 픽셀 올릴지
    [SerializeField] float pixelsPerUnit = 32f; // 스프라이트 임포트 PPU와 맞추기

    [Header("Detect")]
    [SerializeField] float detectRadius = 1.6f;      // 탐지 반경
    [SerializeField] LayerMask interactableMask;     // Interactable 레이어 체크

    [Header("FX")]
    [SerializeField] float bobAmp = 0.07f;  // 통통 튀는 높이
    [SerializeField] float bobSpeed = 6f;

    SpriteRenderer icon;
    Transform tr;
    float t;

    void Awake()
    {
        tr = transform;

        // 아이콘 오브젝트 생성(플레이어의 자식)
        var go = new GameObject("ExclaimIcon");
        go.transform.SetParent(tr);
        icon = go.AddComponent<SpriteRenderer>();
        icon.sprite = exclaimSprite;
        icon.enabled = false;                       // 기본은 숨김
        icon.sortingLayerName = "UI";               // 없으면 Default로 두고 order만 올려도 됨
        icon.sortingOrder = 1000;                   // 항상 위로

        // 픽셀 오프셋을 월드 좌표로 변환해서 초기 위치 세팅
        Vector3 worldOffset = new Vector3(pixelOffset.x / pixelsPerUnit,
                                          pixelOffset.y / pixelsPerUnit, 0f);
        go.transform.localPosition = worldOffset;
    }

    void Update()
    {
        // 주변 상호작용 오브젝트 감지(가장 간단, 성능 가벼움)
        bool near = Physics2D.OverlapCircle(tr.position, detectRadius, interactableMask);

        if (near)
        {
            if (!icon.enabled) { icon.enabled = true; t = 0f; }

            // 머리 위에서 살짝 통통 튀기
            t += Time.deltaTime * bobSpeed;
            float y = (pixelOffset.y / pixelsPerUnit) + Mathf.Sin(t) * bobAmp;
            icon.transform.localPosition = new Vector3(pixelOffset.x / pixelsPerUnit, y, 0f);
        }
        else
        {
            if (icon.enabled) icon.enabled = false;
        }
    }

    // 에디터에서 반경 보이기
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? transform.position : transform.position, detectRadius);
    }
}

