using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IDinput : MonoBehaviour
{
    public ScenesManager scenesManager;

    public static IDinput IDinputTest;
    public GameObject IdInputAlert;
    public Text IdInputAlertText;

    public Text user_Name1; //출력할 유저네임
    public Text user_Name2; //출력할 유저네임2

    public GameObject StartGame;
    public GameObject InputUI;
    public GameObject P1UI;
    public GameObject P2UI;
    public GameObject P2UIBUtton;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // ScenesManager를 찾아서 scenesManager에 할당
        scenesManager = FindObjectOfType<ScenesManager>();

        // 씬이 시작될 때 저장된 값을 불러옴
        string playerName1 = PlayerPrefs.GetString("PlayerName1", "");
        string playerName2 = PlayerPrefs.GetString("PlayerName2", "");

        // 텍스트 필드에 저장된 값을 표시
        user_Name1.text = playerName1;
        user_Name2.text = playerName2;
    }

    public void Clicked_Btn1() //로그인 버튼 누를시 인풋 필트의 텍스트에 값 들어가기
    {
       if(P1UI.activeSelf)
        {
            if (!string.IsNullOrEmpty(scenesManager.inputID1.text))
            {
                if (scenesManager.inputID1.text.Length <= 6)
                {
                    // PlayerPrefs를 사용하여 입력 값을 저장
                    PlayerPrefs.SetString("PlayerName1", scenesManager.inputID1.text);

                    Debug.Log("로그인 성공!");
                    StartGame.SetActive(true);
                    InputUI.SetActive(false);
                }
                else
                {
                    IdInputAlert.SetActive(true);
                    IdInputAlertText.text = "입력받은 값 초과";
                    Debug.Log("입력받은 값 초과");
                }
            }
            else
            {
                IdInputAlert.SetActive(true);
                IdInputAlertText.text = "ID공백 확인 요망";

                Debug.Log("ID공백 확인 요망");
            }

        }


    }

    public void Clicked_Btn2() //로그인 버튼 누를시 인풋 필트의 텍스트에 값 들어가기
    {
        if (P2UI.activeSelf)
        {
            if (scenesManager.inputID1.text !=null && scenesManager.inputID2.text != null)
            {
                if (scenesManager.inputID1.text.Length <= 6 && scenesManager.inputID2.text.Length <= 6)
                {
                    // PlayerPrefs를 사용하여 입력 값을 저장
                    PlayerPrefs.SetString("PlayerName1", scenesManager.inputID1.text);
                    PlayerPrefs.SetString("PlayerName2", scenesManager.inputID2.text);


                    Debug.Log("로그인 성공!");
                    StartGame.SetActive(true);
                    InputUI.SetActive(false);
                }
                else
                {
                    IdInputAlert.SetActive(true);
                    IdInputAlertText.text = "입력받은 값 초과";
                    Debug.Log("입력받은 값 초과");
                }
            }
            else
            {
                IdInputAlert.SetActive(true);
                IdInputAlertText.text = "ID공백 확인 요망";

                Debug.Log("2P : ID공백 확인 요망");
            }

        }
    }
   

    public void P2UIActiveButton()
        {
            P1UI.SetActive(false);
            P2UI.SetActive(true);
            Debug.Log("p2 ui 작동");
        }
    public void P1UIActiveButton()
    {
        P1UI.SetActive(true);
        P2UI.SetActive(false);
    }

    public void IdInputAlertOffButton()
        {
            IdInputAlert.SetActive(false);
        }
    }
        