using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    public GameObject map; //활성화할 오브젝트
    public int receivedVariable = 0; // 받아들이기 위한 변수 
    public InputField inputID1;
    public InputField inputID2;

    void Awake()
    {
        receivedVariable = PlayerPrefs.GetInt("MyVariable", 0); //버튼 스크립트에서 세팅한 변수 저장
        inputID1.text = PlayerPrefs.GetString("");
        inputID2.text = PlayerPrefs.GetString("");

        if (receivedVariable == 2)
        {
            map.SetActive(true); // 맵 활성화
            receivedVariable = 0; // 변수 초기화
            PlayerPrefs.SetInt("MyVariable", receivedVariable);// 초기화된 값 발송
            PlayerPrefs.SetString("MyId1", inputID1.text);// 초기화된 값 발
            PlayerPrefs.SetString("MyId2", inputID2.text);// 초기화된 값 발

            PlayerPrefs.Save(); //바뀌지 않게 저장
        }
    }

}
