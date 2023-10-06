using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float speed;
public int power;
public int count; // 물풍선 횟수


float hAxis;
float vAxis;
bool isHorizonMove; // 대각선 이동 제한

int playerBballonIndex = 10; // 물풍선 오브젝트 풀 사용할 때 필요한 playerBballonIndex 변수
public int playerBcountIndex = 0; // 물풍선을 생성할 때, playerBmakeBalloon 을 false 값으로 바꿔 줄 때 필요한 조건문의 변수
public bool playerBmakeBalloon = false; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수
public ObjectManager objectManager;

Rigidbody2D rigid;


void Awake() {
    rigid = GetComponent<Rigidbody2D>();
}

void Update() {

    Move();
    Skill();

}

void FixedUpdate() {

    // 대각선 이동 제한
    Vector2 moveVec = isHorizonMove ? new Vector2(hAxis, 0) : new Vector2(0, vAxis);
    rigid.velocity = moveVec * speed;
}

void Move()
{
    // 이동
    hAxis = Input.GetAxisRaw("2PH");
    vAxis = Input.GetAxisRaw("2PV");

    bool hDown = Input.GetButtonDown("2PH");
    bool vDown = Input.GetButtonDown("2PV");
    bool hUp = Input.GetButtonUp("2PH");
    bool vUp = Input.GetButtonUp("2PV");


    if (vDown)
    {
        isHorizonMove = false;
    }
    else if (hDown)
    {
        isHorizonMove = true;
    }
    else if (vUp || hUp)
    {
        isHorizonMove = hAxis != 0;
    }
}
void Skill()
{
    // 물풍선
    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
        switch (power)
        {
            case 1:
                MakeBalloon("WaterBalloon1");
                break;
            case 2:
                MakeBalloon("WaterBalloon2");
                break;
            case 3:
                MakeBalloon("WaterBalloon3");
                break;
            case 4:
                MakeBalloon("WaterBalloon4");
                break;
            case 5:
                MakeBalloon("WaterBalloon5");
                break;
            case 6:
                MakeBalloon("WaterBalloon6");
                break;
            case 7:
                MakeBalloon("WaterBalloon7");
                break;
        }
    }
}
void CountDown()
{
    playerBcountIndex--;
}
void MakeBalloon(string Power)
{
    Vector3 MoveVec = transform.position;
    MoveVec = new Vector3((float)Math.Round(MoveVec.x), (float)Math.Round(MoveVec.y), MoveVec.z); //소수점 버림

    GameObject[] WaterBalloon;
    WaterBalloon = objectManager.GetPool(Power);
    if (playerBcountIndex < count && !playerBmakeBalloon)
    {
        if (!WaterBalloon[playerBballonIndex].activeInHierarchy)
        {
            WaterBalloon[playerBballonIndex].SetActive(true);
            WaterBalloon[playerBballonIndex].transform.position = MoveVec;
            playerBballonIndex += 1;
            // playerBballonIndex 가 20 을 넘어가면 0으로 초기화
            if (WaterBalloon.Length == playerBballonIndex)
            {
                playerBballonIndex = 10;
            }
            playerBcountIndex++;
            Invoke("CountDown", 5f);
        }
        playerBmakeBalloon = true;
    }
}
void OnTriggerExit2D(Collider2D collision)
{
    // 플레이어가 물풍선 밖으로 나갈 시, 트리거 비활성화
    if (collision.gameObject.tag == "Balloon")
    {
        playerBmakeBalloon = false;
    }
}
}
