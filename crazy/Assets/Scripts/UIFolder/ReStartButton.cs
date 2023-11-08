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

    public void Awake()
    {
       ttM = GetComponent<TitletoManual>();
      
        
    }

    public void ttv() // 버튼 누를 때 다음 씬 연결
    {

        SceneManager.LoadScene("Titles");
        GameObject objToActivate = GameObject.Find("MapSelect");
        objToActivate.SetActive(true);
        Debug.Log("버튼눌림");
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