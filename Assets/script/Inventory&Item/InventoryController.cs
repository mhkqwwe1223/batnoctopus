// InventoryController.cs
using System.Collections;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("�κ��丮 ����")]
    [SerializeField] private GameObject inventoryUI; // �κ��丮 UI ������ �Ǵ� ���ӿ�����Ʈ
    [SerializeField] private Transform player; // �÷��̾� Transform
    [SerializeField] private Vector3 offsetFromPlayer = new Vector3(0, 2f, 0); // �÷��̾�κ����� ������ (�Ӹ� ��)

    [Header("�Է� ����")]
    [SerializeField] private float holdTime = 2f; // �����̽��ٸ� ������ �ϴ� �ð�

    private bool isHolding = false;
    private float holdTimer = 0f;
    private bool inventoryVisible = false;
    private Camera mainCamera;
    private Inventory inventory; // �κ��丮 ���� �߰�

    void Start()
    {
        mainCamera = Camera.main;

        // �κ��丮 ������Ʈ ã�� - ���� ������� �õ�
        if (inventoryUI != null)
        {
            inventory = inventoryUI.GetComponent<Inventory>();
            if (inventory == null)
            {
                inventory = inventoryUI.GetComponentInChildren<Inventory>();
            }
        }

        // ����� �α� �߰�
        if (inventory != null)
        {
            Debug.Log("Inventory ������Ʈ�� ã�ҽ��ϴ�!");
        }
        else
        {
            Debug.LogError("Inventory ������Ʈ�� ã�� �� �����ϴ�! inventoryUI ������Ʈ�� �� �ڽĿ� Inventory ��ũ��Ʈ�� �ִ��� Ȯ���ϼ���.");
        }

        // ���� �� �κ��丮 ����
        if (inventoryUI != null)
            inventoryUI.SetActive(false);
    }

    void Update()
    {
        HandleSpacebarInput();
        HandleInventoryInput(); // �κ��丮 �Է� ó�� �߰�

        // �κ��丮�� ���̴� ���¶�� �÷��̾ ����ٴϰ� ��
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

            // 2�� �̻� ������ �κ��丮 ���
            if (holdTimer >= holdTime && !inventoryVisible)
            {
                ShowInventory();
                isHolding = false; // �� ���� ����ǵ���
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHolding = false;
            holdTimer = 0f;

            // �����̽��ٸ� ���� �κ��丮 ����
            if (inventoryVisible)
            {
                HideInventory();
            }
        }
    }

    void HandleInventoryInput()
    {
        // �κ��丮�� ���̴� ���¿����� ���� ȸ��
        if (inventoryVisible && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("EŰ�� ���Ƚ��ϴ� (�κ��丮 ���� ����)!");
            if (inventory != null)
            {
                if (!inventory.IsRotating())
                {
                    inventory.RotateSlots();
                }
            }
        }
        // �κ��丮�� �����־ ������ �ݱ� ����
        else if (!inventoryVisible && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("EŰ�� ���Ƚ��ϴ� (������ �ݱ� �õ�)!");
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
            // �÷��̾� ��ġ�� �������� ���� ���� ��ǥ
            Vector3 worldPosition = player.position + offsetFromPlayer;

            // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // �κ��丮 UI ��ġ ������Ʈ
            inventoryUI.transform.position = screenPosition;
        }
    }


    // �÷��̾� �ֺ� ������ �ݱ� �õ�
    void TryPickupItem()
    {
        if (inventory == null || player == null) return;

        // �ֺ� ��� ItemPickup Ž��
        ItemPickup[] pickups = FindObjectsOfType<ItemPickup>();
        foreach (var pickup in pickups)
        {
            if (pickup.TryPickup(player, inventory))
            {
                break; // �� �� �ݰ� ����
            }
        }
    }
}