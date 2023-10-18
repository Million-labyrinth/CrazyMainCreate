using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject largeImage; // 해당 작은 이미지에 대응하는 큰 이미지

    public void OnPointerEnter(PointerEventData eventData)
    {
        largeImage.SetActive(true); // 큰 이미지 활성화
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        largeImage.SetActive(false); // 큰 이미지 비활성화
    }
}

