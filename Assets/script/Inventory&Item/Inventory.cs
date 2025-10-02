// Inventory.cs - ������ ����
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

    [Header("ȸ�� �ִϸ��̼� ����")]
    [SerializeField] private float rotationDuration = 0.3f; // ȸ�� �ִϸ��̼� �ð�
    [SerializeField] private AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // �ִϸ��̼� Ŀ��

    private int selectedSlotIndex = 0; // ���� ���õ� ���� �ε��� (�׻� �߾� ������ ���õ�)
    private bool isRotating = false; // ȸ�� ������ Ȯ��

    void Awake()
    {
        FreshSlot();
        UpdateSelection(); // �ʱ� ���� ���� ����
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
            print("������ ���� �� �ֽ��ϴ�.");
        }
    }

    public void RotateSlots()
    {
        Debug.Log($"RotateSlots ȣ���! ���� ������ ����: {items.Count}");

        // ȸ�� ���̸� ����
        if (isRotating) return;

        // �������� 2�� �̻��� ���� ȸ��
        if (items.Count > 1)
        {
            Debug.Log("������ ȸ�� ����!");
            StartCoroutine(AnimateItemRotation());
        }
        else
        {
            Debug.Log("�������� 1�� ���Ͽ��� ȸ������ �ʽ��ϴ�.");
        }
    }

    private IEnumerator AnimateItemRotation()
    {
        isRotating = true;

        // ������ ����Ʈ�� ȸ�� (�������� �� ĭ)
        Item firstItem = items[0];
        items.RemoveAt(0);
        items.Add(firstItem);

        // �ִϸ��̼� ȿ���� ���� �ð��� ��ȯ
        float elapsed = 0f;

        // ���̵� �ƿ�
        while (elapsed < rotationDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / (rotationDuration / 2));

            // ��� ������ ���İ� ����
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

        // ���� ����
        FreshSlot();

        // ���̵� ��
        elapsed = 0f;
        while (elapsed < rotationDuration / 2)
        {
            elapsed += Time.deltaTime;
            float alpha = elapsed / (rotationDuration / 2);

            // ��� ������ ���İ� ����
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

        // ���İ� ���� ����
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

        Debug.Log($"������ ȸ�� �Ϸ�!");
    }

    private void UpdateSelection()
    {
        // ��� ������ ���� ���� �ʱ�ȭ
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].isSelected = false;
        }

        // ù ��° ����(�Ǵ� �߾� ����)�� �׻� ���õ� ���·�
        if (slots.Length > 0)
        {
            // ù ��° ���� ����:
            slots[0].isSelected = true;
            selectedSlotIndex = 0;
            Debug.Log($"���� {selectedSlotIndex}��(��) ���õǾ����ϴ�.");
        }
    }

    // ���� ���õ� ������ ��ȯ
    public Item GetSelectedItem()
    {
        if (selectedSlotIndex < items.Count)
        {
            return items[selectedSlotIndex];
        }
        return null;
    }

    // ���õ� �������� �ε��� ��ȯ
    public int GetSelectedSlotIndex()
    {
        return selectedSlotIndex;
    }

    // ���õ� ���Կ� �������� �ִ��� Ȯ��
    public bool HasSelectedItem()
    {
        return selectedSlotIndex < items.Count && items[selectedSlotIndex] != null;
    }

    // ȸ�� ������ Ȯ��
    public bool IsRotating()
    {
        return isRotating;
    }
}