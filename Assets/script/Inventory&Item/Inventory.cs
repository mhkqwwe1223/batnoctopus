// Inventory.cs - 개선된 버전
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    [Header("회전 애니메이션 설정")]
    [SerializeField] private float rotationDuration = 0.3f; // 회전 애니메이션 시간
    [SerializeField] private AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // 애니메이션 커브

    private int selectedSlotIndex = 0; // 현재 선택된 슬롯 인덱스 (항상 중앙 슬롯이 선택됨)
    private bool isRotating = false; // 회전 중인지 확인

    void Awake()
    {
        // GameState에 저장된 아이템 목록을 현재 인벤토리 목록에 추가합니다.
        if (GameState.Instance != null)
        {
            foreach (var item in GameState.Instance.inventoryItems)
            {
                if (!items.Contains(item)) // 중복 추가 방지
                {
                    items.Add(item);
                }
            }
        }

        FreshSlot();       // UI 슬롯을 새로고침하여 아이템을 표시합니다.
        UpdateSelection(); // 초기 선택 상태를 설정합니다.
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }

    public void RotateSlots()
    {
        Debug.Log($"RotateSlots 호출됨! 현재 아이템 개수: {items.Count}");

        // 회전 중이면 무시
        if (isRotating) return;

        // 아이템이 2개 이상일 때만 회전
        if (items.Count > 1)
        {
            Debug.Log("아이템 회전 시작!");
            StartCoroutine(AnimateItemRotation());
        }
        else
        {
            Debug.Log("아이템이 1개 이하여서 회전하지 않습니다.");
        }
    }

    private IEnumerator AnimateItemRotation()
    {
        isRotating = true;

        // 아이템 리스트를 회전 (왼쪽으로 한 칸)
        Item firstItem = items[0];
        items.RemoveAt(0);
        items.Add(firstItem);

        // 애니메이션 효과를 위한 시각적 전환
        float elapsed = 0f;

        // 페이드 아웃
        while (elapsed < rotationDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / (rotationDuration / 2));

            // 모든 슬롯의 알파값 조정
            foreach (var slot in slots)
            {
                if (slot.item != null)
                {
                    var image = slot.GetComponent<UnityEngine.UI.Image>();
                    if (image != null)
                    {
                        var color = image.color;
                        color.a = alpha;
                        image.color = color;
                    }
                }
            }

            yield return null;
        }

        // 슬롯 갱신
        FreshSlot();

        // 페이드 인
        elapsed = 0f;
        while (elapsed < rotationDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = elapsed / (rotationDuration / 2);

            // 모든 슬롯의 알파값 조정
            foreach (var slot in slots)
            {
                if (slot.item != null)
                {
                    var image = slot.GetComponent<UnityEngine.UI.Image>();
                    if (image != null)
                    {
                        var color = image.color;
                        color.a = alpha;
                        image.color = color;
                    }
                }
            }

            yield return null;
        }

        // 알파값 완전 복구
        foreach (var slot in slots)
        {
            if (slot.item != null)
            {
                var image = slot.GetComponent<UnityEngine.UI.Image>();
                if (image != null)
                {
                    var color = image.color;
                    color.a = 1f;
                    image.color = color;
                }
            }
        }

        UpdateSelection();
        isRotating = false;

        Debug.Log($"아이템 회전 완료!");
    }

    private void UpdateSelection()
    {
        // 모든 슬롯의 선택 상태 초기화
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].isSelected = false;
        }

        // 첫 번째 슬롯(또는 중앙 슬롯)을 항상 선택된 상태로
        if (slots.Length > 0)
        {
            // 첫 번째 슬롯 선택:
            slots[0].isSelected = true;
            selectedSlotIndex = 0;
            Debug.Log($"슬롯 {selectedSlotIndex}이(가) 선택되었습니다.");
        }
    }

    // 현재 선택된 아이템 반환
    public Item GetSelectedItem()
    {
        if (selectedSlotIndex < items.Count)
        {
            return items[selectedSlotIndex];
        }
        return null;
    }

    // 선택된 아이템의 인덱스 반환
    public int GetSelectedSlotIndex()
    {
        return selectedSlotIndex;
    }

    // 선택된 슬롯에 아이템이 있는지 확인
    public bool HasSelectedItem()
    {
        return selectedSlotIndex < items.Count && items[selectedSlotIndex] != null;
    }

    // 회전 중인지 확인
    public bool IsRotating()
    {
        return isRotating;
    }
}