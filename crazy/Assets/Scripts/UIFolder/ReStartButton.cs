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

    int seenCtn; // ���� ����

    public void ttv()
    {

        seenCtn = PlayerPrefs.GetInt("MyVariable", 0); //
        seenCtn = 2;//���� ���� ���� 1 = Ÿ��Ʋ/ 2= �ʼ���
        PlayerPrefs.SetInt("MyVariable", seenCtn); // �� ���� �߼�

        PlayerPrefs.Save(); // ���� ����
        SceneManager.LoadScene("Titles"); //�� �ҷ����� 
    }

    public void GoTitle()
    {
        seenCtn = PlayerPrefs.GetInt("MyVariable", 0); //
        seenCtn = 1;//���� ���� ���� 1 = Ÿ��Ʋ/ 2= �ʼ���
        PlayerPrefs.SetInt("MyVariable", seenCtn); // �� ���� �߼�


        PlayerPrefs.Save(); // ���� ����
        SceneManager.LoadScene("Titles"); //�� �ҷ����� 

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