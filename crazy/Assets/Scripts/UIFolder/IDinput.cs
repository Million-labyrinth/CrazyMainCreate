using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDinput : MonoBehaviour
{
    public InputField input_ID1; // 인풋필드 값
    public InputField input_ID2; // 인풋필드 값

    public Text user_Name1; //출력할 유저네임
    public Text user_Name2; //출력할 유저네임2

    public GameObject HowToGame;
    public GameObject StartGame;
    public GameObject InputUI;


    public void Clicked_Btn() //로그인 버튼 누를시 인풋 필트의 텍스트에 값 들어가기
    {
        if (!string.IsNullOrEmpty(input_ID1.text) && !string.IsNullOrEmpty(input_ID2.text))
        {
            if (input_ID1.text.Length<=6 && input_ID2.text.Length<=6)
            {
                Debug.Log("로그인 성공!");
                HowToGame.SetActive(true);
                StartGame.SetActive(true);
                InputUI.SetActive(false);
            }
            else
            {
                Debug.Log("입력받은 값 초과");
            }
           
        }
        else
        {
            Debug.Log("ID공백 확인 요망");
        }
    }



    public void SetUserName(string player1, string player2) 
    {
        user_Name1.text = player1;
        user_Name2.text = player2;
    }


}
