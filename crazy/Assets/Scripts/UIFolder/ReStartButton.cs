using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ReStartButton : MonoBehaviour
{
    public MapSelect mapSelect;
    public Button title_to_scene;
    public GameObject alert;
    public TitletoManual ttM;
    public GameRoomUIBUtton gameRoomUIBUtton;
    public Text playFieldName;
    int playerRoomState;
    int seenCtn; // 이전 값 읽을 변수

    
    public void GoPlayerRoom()
    {
        if (playFieldName.text.Contains("Forest") ){
            playerRoomState = 2; // 새로운 값을 설정


            // 값을 저장하고 현재 씬을 종료
            PlayerPrefs.SetInt("PlayerRoomState", playerRoomState);
            PlayerPrefs.SetString("PlayerRoomPvE", "PvERoom");
            PlayerPrefs.Save();

            // 현재 씬을 종료하면서 다음 씬으로 전환
            SceneManager.LoadScene("Titles"); 
        }
        else
        {
            playerRoomState = 3; // 새로운 값을 설정


            // 값을 저장하고 현재 씬을 종료
            PlayerPrefs.SetInt("PlayerRoomState", playerRoomState);
            PlayerPrefs.SetString("PlayerRoomPvE", "PvERoom");
            PlayerPrefs.Save();

            // 현재 씬을 종료하면서 다음 씬으로 전환
            SceneManager.LoadScene("Titles");
        }
    }



    public void GoTitle()
    {
        playerRoomState = 1; // 새로운 값을 설정


        // 값을 저장하고 현재 씬을 종료
        PlayerPrefs.SetInt("PlayerRoomState", playerRoomState);
        TitlesOnPlayerRoom.shouldInitialize = true;

        seenCtn = PlayerPrefs.GetInt("MyVariable", 0); //
        seenCtn = 1;//               1 = Ÿ  Ʋ/ 2=  ʼ   
        PlayerPrefs.SetInt("MyVariable", seenCtn); //          ߼ 

        PlayerPrefs.Save(); //          
        SceneManager.LoadScene("Titles"); //    ҷ      
    }

    public void openRestartAlert()
    {
        alert.SetActive(true);
    }

    public void closeReStartAlert() //
    {
        gameRoomUIBUtton.BackBoard.SetActive(false);
        alert.SetActive(false);
    }
    public void closeMapSelectAlert() // 
    {
        gameRoomUIBUtton.BackBoard.SetActive(false);
        mapSelect.MapSelectUI.SetActive(false); 
        alert.SetActive(false);
    }
}