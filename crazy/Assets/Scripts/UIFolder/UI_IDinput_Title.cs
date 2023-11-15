using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IDinput_Title : IDMgr
{
    public InputField inputPlayer_1;
    public InputField inputPlayer_Expend_1;
    public InputField inputPlayer_Expend_2;

    public GameObject IdInputAlert;
    public Text IdInputAlertText;



    public GameObject StartGame;
    public GameObject P1UI;
    public GameObject P2UI;
    public GameObject P2UIBUtton;


    protected override void Awake()
    {
        base.Awake();

        // 텍스트 필드에 저장된 값을 표시
        inputPlayer_Expend_1.text = inputPlayer_1.text = strPlayer_1;
        inputPlayer_Expend_2.text = strPlayer_2;

    }

    public void Clicked_Btn1() //로그인 버튼 누를시 인풋 필트의 텍스트에 값 들어가기
    {
       if(P1UI.activeSelf)
        {
            if (!string.IsNullOrEmpty(inputPlayer_1.text))
            {
                if (inputPlayer_1.text.Length <= 6)
                {
                    // PlayerPrefs를 사용하여 입력 값을 저장
                    PlayerPrefs.SetString("PlayerName1", inputPlayer_1.text);
                    Debug.Log("로그인 성공!");
                    DeletePlayer2();
                    StartGame.SetActive(true);
                    P1UI.SetActive(false);
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
            if (!string.IsNullOrEmpty(inputPlayer_Expend_1.text) && !string.IsNullOrEmpty(inputPlayer_Expend_2.text))
            {
                if (inputPlayer_Expend_1.text.Length <= 6 && inputPlayer_Expend_2.text.Length <= 6)
                {
                    // PlayerPrefs를 사용하여 입력 값을 저장
                    PlayerPrefs.SetString("PlayerName1", inputPlayer_Expend_1.text);
                    PlayerPrefs.SetString("PlayerName2", inputPlayer_Expend_2.text);


                    Debug.Log("로그인 성공!");
                    StartGame.SetActive(true);
                    P2UI.SetActive(false);
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
        