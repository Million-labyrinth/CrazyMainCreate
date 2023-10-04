using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private int playerState = 0;

    // 플레이어 상태를 설정하는 함수
    public void SetPlayerState(int newState)
    {
        playerState = newState;

        // 플레이어 상태가 변경될 때 필요한 동작을 수행합니다.
        switch (playerState)
        {
            case 0:
                // 행동 가능한 상태
                // 여기서 필요한 행동 가능한 상태에서의 동작을 수행합니다.
                break;
            case 1:
                // 물풍선 같힌 상태
                // 여기서 필요한 물풍선 같힌 상태에서의 동작을 수행합니다.
                break;
            case 2:
                // 죽음 상태
                // 여기서 필요한 죽음 상태에서의 동작을 수행합니다.
                break;
            default:
                break;
        }
    }

    // 플레이어 상태를 가져오는 함수
    public int GetPlayerState()
    {
        return playerState;
    }
}
