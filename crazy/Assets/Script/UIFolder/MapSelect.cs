using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject largeImage; // �ش� ���� �̹����� �����ϴ� ū �̹���

    public void OnPointerEnter(PointerEventData eventData)
    {
        largeImage.SetActive(true); // ū �̹��� Ȱ��ȭ
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        largeImage.SetActive(false); // ū �̹��� ��Ȱ��ȭ
    }
}

