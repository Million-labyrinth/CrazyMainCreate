using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button title_to_scene;
    public GameObject largeImage; // �ش� ���� �̹����� �����ϴ� ū �̹���

    public void OnPointerEnter(PointerEventData eventData)
    {
        largeImage.SetActive(true); // ū �̹��� Ȱ��ȭ
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        largeImage.SetActive(false); // ū �̹��� ��Ȱ��ȭ
    }

    public void tts()
    {
        SceneManager.LoadScene("BattelFieldVillage");
    }
}

