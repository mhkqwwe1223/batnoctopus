// InventoryController.cs
using System.Collections;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("인벤토리 설정")]
    [SerializeField] private GameObject inventoryUI; // 인벤토리 UI 프리팹 또는 게임오브젝트
    [SerializeField] private Transform player; // 플레이어 Transform
    [SerializeField] private Vector3 offsetFromPlayer = new Vector3(0, 2f, 0); // 플레이어로부터의 오프셋 (머리 위)

    [Header("입력 설정")]
    [SerializeField] private float holdTime = 2f; // 스페이스바를 눌러야 하는 시간

    private bool isHolding = false;
    private float holdTimer = 0f;
    private bool inventoryVisible = false;
    private Camera mainCamera;
    private Inventory inventory; // 인벤토리 참조 추가

    void Start()
    {
        mainCamera = Camera.main;

        // 인벤토리 컴포넌트 찾기 - 여러 방법으로 시도
        if (inventoryUI != null)
        {
            inventory = inventoryUI.GetComponent<Inventory>();
            if (inventory == null)
            {
                inventory = inventoryUI.GetComponentInChildren<Inventory>();
            }
        }

        // 디버그 로그 추가
        if (inventory != null)
        {
            Debug.Log("Inventory 컴포넌트를 찾았습니다!");
        }
        else
        {
            Debug.LogError("Inventory 컴포넌트를 찾을 수 없습니다! inventoryUI 오브젝트나 그 자식에 Inventory 스크립트가 있는지 확인하세요.");
        }

        // 시작 시 인벤토리 숨김
        if (inventoryUI != null)
            inventoryUI.SetActive(false);
    }

    void Update()
    {
        HandleSpacebarInput();
        HandleInventoryInput(); // 인벤토리 입력 처리 추가

        // 인벤토리가 보이는 상태라면 플레이어를 따라다니게 함
        if (inventoryVisible && inventoryUI != null && player != null)
        {
            UpdateInventoryPosition();
        }
    }

    void HandleSpacebarInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHolding = true;
            holdTimer = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && isHolding)
        {
            holdTimer += Time.deltaTime;

            // 2초 이상 누르면 인벤토리 토글
            if (holdTimer >= holdTime && !inventoryVisible)
            {
                ShowInventory();
                isHolding = false; // 한 번만 실행되도록
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHolding = false;
            holdTimer = 0f;

            // 스페이스바를 떼면 인벤토리 숨김
            if (inventoryVisible)
            {
                HideInventory();
            }
        }
    }

    void HandleInventoryInput()
    {
        // 인벤토리가 보이는 상태에서만 슬롯 회전
        if (inventoryVisible && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E키가 눌렸습니다 (인벤토리 열림 상태)!");
            if (inventory != null)
            {
                if (!inventory.IsRotating())
                {
                    inventory.RotateSlots();
                }
            }
        }
        // 인벤토리가 닫혀있어도 아이템 줍기 가능
        else if (!inventoryVisible && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E키가 눌렸습니다 (아이템 줍기 시도)!");
            TryPickupItem();
        }
    }


    void ShowInventory()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(true);
            inventoryVisible = true;
            UpdateInventoryPosition();
        }
    }

    void HideInventory()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
            inventoryVisible = false;
        }
    }

    void UpdateInventoryPosition()
    {
        if (player != null && inventoryUI != null)
        {
            // 플레이어 위치에 오프셋을 더한 월드 좌표
            Vector3 worldPosition = player.position + offsetFromPlayer;

            // 월드 좌표를 스크린 좌표로 변환
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // 인벤토리 UI 위치 업데이트
            inventoryUI.transform.position = screenPosition;
        }
    }


    // 플레이어 주변 아이템 줍기 시도
    void TryPickupItem()
    {
        if (inventory == null || player == null) return;

        // 주변 모든 ItemPickup 탐색
        ItemPickup[] pickups = FindObjectsOfType<ItemPickup>();
        foreach (var pickup in pickups)
        {
            if (pickup.TryPickup(player, inventory))
            {
                break; // 한 번 줍고 종료
            }
        }
    }
}