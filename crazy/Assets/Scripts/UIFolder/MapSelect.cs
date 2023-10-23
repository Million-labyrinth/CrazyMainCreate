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

    public void OnPointerEnter(PointerEventData eventData)
    {
        largeImage.SetActive(true);
        border.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        largeImage.SetActive(false);
        border.SetActive(false);
    }

    public void tts()
    {
        SceneManager.LoadScene("BattelFieldVillage");
    }
}

