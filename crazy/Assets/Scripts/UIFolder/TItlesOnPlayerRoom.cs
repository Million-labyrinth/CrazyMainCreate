using System.Data;
using UnityEngine;
using UnityEngine.UI;


public class TitlesOnPlayerRoom : MonoBehaviour
{
    public GameObject playerRoom;
    public IDMgr idmgr;
    public static bool shouldInitialize = true; // 초기화를 진행할지 여부를 결정하는 변수

    // public GameObject Player2;

    void Awake()
    {

        if (shouldInitialize == true)
        {
            // 초기화 코드 실행
            idmgr.DeletePlayer1();
            idmgr.DeletePlayer2();


            // 초기화가 끝났으므로 false로 변경
            shouldInitialize = false;
        }

        if (playerRoom != null)
        {
            int activatePlayerRoom = PlayerPrefs.GetInt("PlayerRoomState", 0);

            if (activatePlayerRoom == 2)
            {
                playerRoom.SetActive(true);

                // 값을 초기화
                PlayerPrefs.SetInt("PlayerRoomState", 0);
                PlayerPrefs.Save();


            }
        }
    }
}
