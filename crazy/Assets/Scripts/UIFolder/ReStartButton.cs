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

    int seenCtn; // 이전 값 읽을 변수

    public void GoPlayerRoom()
    {
        int playerRoomState = 2; // 새로운 값을 설정

        // 값을 저장하고 현재 씬을 종료
        PlayerPrefs.SetInt("PlayerRoomState", playerRoomState);
        PlayerPrefs.Save();

        // 현재 씬을 종료하면서 다음 씬으로 전환
        SceneManager.LoadScene("Titles");
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