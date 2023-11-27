using UnityEngine;

public class TitlesOnPlayerRoom : MonoBehaviour
{
    public GameObject playerRoom;
    //public UI_IDinput_Title uI_IDinput_Title;
    // public GameObject Player2;

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
