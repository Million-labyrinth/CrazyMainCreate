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

    public string[] RandomSceneNames;




    public void OnPointerEnter(PointerEventData eventData) // ū �̹��� ���콺 �÷��� �� ���� �ڵ�
    {
        largeImage.SetActive(true);
        border.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) // ���콺�� �������� ���� �ڵ�
    {
        largeImage.SetActive(false);
        border.SetActive(false);
    }

    public void ttv() // ��ư ���� �� ���� �� ����
    {

        SceneManager.LoadScene("BattelFieldVillage");
    }
    public void ttf()
    {
        SceneManager.LoadScene("BattelFieldFactory 1");
    }

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

