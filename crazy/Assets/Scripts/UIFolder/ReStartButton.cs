using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ReStartButton : MonoBehaviour
{
    public Button title_to_scene;
    public GameObject alert;
    public TitletoManual ttM;

    int seenCtn; // 보낼 변수
    string id1;
    string id2;

    public void ttv()
    {
        id1 = PlayerPrefs.GetString("MyId1");
        id1 = PlayerPrefs.GetString("MyId2");

        seenCtn = PlayerPrefs.GetInt("MyVariable", 0); //
        seenCtn += 2;//보낼 변수 저장 1 = 타이틀/ 2= 맵선택
        PlayerPrefs.SetInt("MyVariable", seenCtn); // 씬 변수 발송
        PlayerPrefs.SetString("MyId1", id1); // 씬 변수 발송
        PlayerPrefs.SetString("MyId1", id2); // 씬 변수 발송

        PlayerPrefs.Save(); // 변수 저장
        SceneManager.LoadScene("Titles"); //씬 불러오기 
    }
 
    public void GoTitle()
    {
        SceneManager.LoadScene("Titles"); //씬 불러오기 

    }

    public void openRestartAlert()
    {
        alert.SetActive(true);
    }

    public void closeReStartAlert() //해당 창 닫힘 버튼
    {
        alert.SetActive(false);
    }
}