using UnityEngine;
using UnityEngine.UI;


public class TitlesOnPlayerRoom : MonoBehaviour
{
    public GameObject playerRoom;

    public IDMgr idmgr;
    // public GameObject Player2;

    void Awake()
    {
        idmgr.strPlayer_1 = "";
        idmgr.strPlayer_2 = "";
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
