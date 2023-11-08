using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDinput : MonoBehaviour
{
    public InputField input_ID1; // ��ǲ�ʵ� ��
    public InputField input_ID2; // ��ǲ�ʵ� ��

    public Text user_Name1; //����� ��������
    public Text user_Name2; //����� ��������2

    public GameObject HowToGame;
    public GameObject StartGame;
    public GameObject InputUI;


    public void Clicked_Btn() //�α��� ��ư ������ ��ǲ ��Ʈ�� �ؽ�Ʈ�� �� ����
    {
        if (!string.IsNullOrEmpty(input_ID1.text) && !string.IsNullOrEmpty(input_ID2.text))
        {
            if (input_ID1.text.Length<=6 && input_ID2.text.Length<=6)
            {
                Debug.Log("�α��� ����!");
                HowToGame.SetActive(true);
                StartGame.SetActive(true);
                InputUI.SetActive(false);
            }
            else
            {
                Debug.Log("�Է¹��� �� �ʰ�");
            }
           
        }
        else
        {
            Debug.Log("ID���� Ȯ�� ���");
        }
    }



    public void SetUserName(string player1, string player2) 
    {
        user_Name1.text = player1;
        user_Name2.text = player2;
    }


}
