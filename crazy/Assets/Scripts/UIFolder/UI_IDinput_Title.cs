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

        // �ؽ�Ʈ �ʵ忡 ����� ���� ǥ��
        inputPlayer_Expend_1.text = inputPlayer_1.text = strPlayer_1;
        inputPlayer_Expend_2.text = strPlayer_2;

    }

    public void Clicked_Btn1() //�α��� ��ư ������ ��ǲ ��Ʈ�� �ؽ�Ʈ�� �� ����
    {
       if(P1UI.activeSelf)
        {
            if (!string.IsNullOrEmpty(inputPlayer_1.text))
            {
                if (inputPlayer_1.text.Length <= 6)
                {
                    // PlayerPrefs�� ����Ͽ� �Է� ���� ����
                    PlayerPrefs.SetString("PlayerName1", inputPlayer_1.text);
                    Debug.Log("�α��� ����!");
                    DeletePlayer2();
                    StartGame.SetActive(true);
                    P1UI.SetActive(false);
                }
                else
                {
                    IdInputAlert.SetActive(true);
                    IdInputAlertText.text = "�Է¹��� �� �ʰ�";
                    Debug.Log("�Է¹��� �� �ʰ�");
                }
            }
            else
            {
                IdInputAlert.SetActive(true);
                IdInputAlertText.text = "ID���� Ȯ�� ���";

                Debug.Log("ID���� Ȯ�� ���");
            }

        }


    }

    public void Clicked_Btn2() //�α��� ��ư ������ ��ǲ ��Ʈ�� �ؽ�Ʈ�� �� ����
    {
        if (P2UI.activeSelf)
        {
            if (!string.IsNullOrEmpty(inputPlayer_Expend_1.text) && !string.IsNullOrEmpty(inputPlayer_Expend_2.text))
            {
                if (inputPlayer_Expend_1.text.Length <= 6 && inputPlayer_Expend_2.text.Length <= 6)
                {
                    // PlayerPrefs�� ����Ͽ� �Է� ���� ����
                    PlayerPrefs.SetString("PlayerName1", inputPlayer_Expend_1.text);
                    PlayerPrefs.SetString("PlayerName2", inputPlayer_Expend_2.text);


                    Debug.Log("�α��� ����!");
                    StartGame.SetActive(true);
                    P2UI.SetActive(false);
                }
                else
                {
                    IdInputAlert.SetActive(true);
                    IdInputAlertText.text = "�Է¹��� �� �ʰ�";
                    Debug.Log("�Է¹��� �� �ʰ�");
                }
            }
            else
            {
                IdInputAlert.SetActive(true);
                IdInputAlertText.text = "ID���� Ȯ�� ���";

                Debug.Log("2P : ID���� Ȯ�� ���");
            }

        }
    }
   


    public void P2UIActiveButton()
        {
            P1UI.SetActive(false);
            P2UI.SetActive(true);
            Debug.Log("p2 ui �۵�");
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
        