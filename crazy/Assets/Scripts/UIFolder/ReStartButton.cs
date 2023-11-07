using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ReStartButton : MonoBehaviour
{
    public Button title_to_scene;
    public GameObject alert;



    public void ttv() // ��ư ���� �� ���� �� ����
    {
        SceneManager.LoadScene("Titles");
        Debug.Log("��ư����");
    }

    public void openRestartAlert()
    {
        alert.SetActive(true);
    }

    public void closeReStartAlert() //�ش� â ���� ��ư
    {
        alert.SetActive(false);
    }
}