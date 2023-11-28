using System.Data;
using UnityEngine;
using UnityEngine.UI;


public class TitlesOnPlayerRoom : MonoBehaviour
{
    public GameObject playerRoom;
    public IDMgr idmgr;

    // public GameObject Player2;
    private void Start()
    {
       idmgr.DeletePlayer1();
        idmgr.DeletePlayer2();
    }
    void Awake()
    {
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
