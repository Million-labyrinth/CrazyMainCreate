using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{

    public GameObject map; //Ȱ��ȭ�� ������Ʈ
    public int receivedVariable = 0; // �޾Ƶ��̱� ���� ���� 

    void Awake()
    {
        receivedVariable = PlayerPrefs.GetInt("MyVariable", 0); //��ư ��ũ��Ʈ���� ������ ���� ����
        if (receivedVariable == 2)
        {
            map.SetActive(true); // �� Ȱ��ȭ
            receivedVariable = 0; // ���� �ʱ�ȭ
            PlayerPrefs.SetInt("MyVariable", receivedVariable);// �ʱ�ȭ�� �� �߼�
            PlayerPrefs.Save(); //�ٲ��� �ʰ� ����
        }
    }

}
