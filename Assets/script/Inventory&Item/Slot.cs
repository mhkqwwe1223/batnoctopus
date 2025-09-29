// Slot.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] private GameObject selectionIndicator; // ���� ǥ�� UI (������)

    private Item _item;
    private bool _isSelected = false;

    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public bool isSelected
    {
        get { return _isSelected; }
        set
        {
            _isSelected = value;
            // ���� ���� �ð��� ǥ�� (���� ǥ�� UI�� �ִٸ�)
            if (selectionIndicator != null)
            {
                selectionIndicator.SetActive(_isSelected);
            }
        }
    }
}