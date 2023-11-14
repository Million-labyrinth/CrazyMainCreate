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

    public Text user_Name1; //����� ��������
    public Text user_Name2; //����� ��������2

    public GameObject StartGame;
    public GameObject InputUI;
    public GameObject P1UI;
    public GameObject P2UI;
    public GameObject P2UIBUtton;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // ScenesManager�� ã�Ƽ� scenesManager�� �Ҵ�
        scenesManager = FindObjectOfType<ScenesManager>();

        // ���� ���۵� �� ����� ���� �ҷ���
        string playerName1 = PlayerPrefs.GetString("PlayerName1", "");
        string playerName2 = PlayerPrefs.GetString("PlayerName2", "");

        // �ؽ�Ʈ �ʵ忡 ����� ���� ǥ��
        user_Name1.text = playerName1;
        user_Name2.text = playerName2;
    }

    public void Clicked_Btn1() //�α��� ��ư ������ ��ǲ ��Ʈ�� �ؽ�Ʈ�� �� ����
    {
       if(P1UI.activeSelf)
        {
            if (!string.IsNullOrEmpty(scenesManager.inputID1.text))
            {
                if (scenesManager.inputID1.text.Length <= 6)
                {
                    // PlayerPrefs�� ����Ͽ� �Է� ���� ����
                    PlayerPrefs.SetString("PlayerName1", scenesManager.inputID1.text);

                    Debug.Log("�α��� ����!");
                    StartGame.SetActive(true);
                    InputUI.SetActive(false);
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
            if (scenesManager.inputID1.text !=null && scenesManager.inputID2.text != null)
            {
                if (scenesManager.inputID1.text.Length <= 6 && scenesManager.inputID2.text.Length <= 6)
                {
                    // PlayerPrefs�� ����Ͽ� �Է� ���� ����
                    PlayerPrefs.SetString("PlayerName1", scenesManager.inputID1.text);
                    PlayerPrefs.SetString("PlayerName2", scenesManager.inputID2.text);


                    Debug.Log("�α��� ����!");
                    StartGame.SetActive(true);
                    InputUI.SetActive(false);
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
        