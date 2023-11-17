using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button title_to_scene;
    public GameObject largeImage;
    public GameObject border;




    public void OnPointerEnter(PointerEventData eventData) // 큰 이미지 마우스 올렸을 때 띄우는 코드
    {
        largeImage.SetActive(true);
        border.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스가 나갔을때 끄는 코드
    {
        largeImage.SetActive(false);
        border.SetActive(false);
    }

    public void ttv() // 버튼 누를 때 다음 씬 연결
    {

        SceneManager.LoadScene("BattelFieldVillage");
    }
    public void ttf()
    {
        SceneManager.LoadScene("BattelFieldFactory");
    }

}

