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
    public GameObject PlayerRoomImg;
    public GameObject MapSelectUI;
    public GameObject MapIcon;
    public GameObject MapNameText;


    public GameObject[] OtherIcons; // 다른 아이콘들을 배열로 선언
    public GameObject[] OtherMapName;
    public GameObject[] OtherMapBorder;


    public string[] RandomSceneNames;




    public void OnPointerEnter(PointerEventData eventData) // ū �̹��� ���콺 �÷��� �� ���� �ڵ�
    {
        largeImage.SetActive(true);
        MapIcon.SetActive(true);
        PlayerRoomImg.SetActive(true);
        border.SetActive(true);

        MapNameText.SetActive(true);
        foreach (GameObject icon in OtherIcons)
        {
            icon.SetActive(false); // 다른 아이콘들 비활성화
        }
        foreach(GameObject mapname in OtherMapName) 
        { 
            mapname.SetActive(false);
        }
        foreach(GameObject mapborder in OtherMapBorder)
        {
            mapborder.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData) // ���콺�� �������� ���� �ڵ�
    {
        largeImage.SetActive(false);
        MapIcon.SetActive(false);
        PlayerRoomImg.SetActive(false);
        border.SetActive(false);


        MapNameText.SetActive(false);
        foreach (GameObject icon in OtherIcons)
        {
            icon.SetActive(false); // 다른 아이콘들 비활성화
        }
    }   

    public void Click_MapSelect_UI()
    {
        // 맵 선택 UI를 닫을 때 모든 요소를 비활성화
        MapSelectUI.SetActive(false);

        foreach (GameObject icon in OtherIcons)
        {
            icon.SetActive(false); // 다른 아이콘들 비활성화
        }
    }

 

    //public void ttv() // ��ư ���� �� ���� �� ����
    //{

    //    SceneManager.LoadScene("BattelFieldVillage");
    //}
    //public void ttf()
    //{
    //    SceneManager.LoadScene("BattelFieldFactory 1");
    //}

    public void LoadRandomScene()
    {
        if (RandomSceneNames.Length > 0)
        {
            int randomIndex = Random.Range(0, RandomSceneNames.Length);
            string randomSceneName = RandomSceneNames[randomIndex];
            SceneManager.LoadScene(randomSceneName);
        }
        else
        {
            Debug.LogError("씬 이름이 지정되지 않았습니다.");
        }
    }
}

