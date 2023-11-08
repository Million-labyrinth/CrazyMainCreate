using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    public GameObject map; //Ȱ��ȭ�� ������Ʈ
    public int receivedVariable = 0; // �޾Ƶ��̱� ���� ���� 
    public InputField inputID1;
    public InputField inputID2;

    void Awake()
    {
        receivedVariable = PlayerPrefs.GetInt("MyVariable", 0); //��ư ��ũ��Ʈ���� ������ ���� ����
        inputID1.text = PlayerPrefs.GetString("");
        inputID2.text = PlayerPrefs.GetString("");

        if (receivedVariable == 2)
        {
            map.SetActive(true); // �� Ȱ��ȭ
            receivedVariable = 0; // ���� �ʱ�ȭ
            PlayerPrefs.SetInt("MyVariable", receivedVariable);// �ʱ�ȭ�� �� �߼�
            PlayerPrefs.SetString("MyId1", inputID1.text);// �ʱ�ȭ�� �� ��
            PlayerPrefs.SetString("MyId2", inputID2.text);// �ʱ�ȭ�� �� ��

            PlayerPrefs.Save(); //�ٲ��� �ʰ� ����
        }
    }

}
