
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player2 : MonoBehaviour
{

    public int bombPower;
    public int bombPowerMax = 10;
    public int bombRange;
    public int bombRangeMax = 7;
    public float playerSpeed;
    public float playerSpeedMax = 7f;
    public float playerHealth;
    public float playerMaxHealth = 2f;
    public string basicBubble;

    int playerBballonIndex = 10; // 물풍선 오브젝트 풀 사용할 때 필요한 playerAballonIndex 변수
    public int playerBcountIndex = 0; // 물풍선을 생성할 때, playerAmakeBalloon 을 false 값으로 바꿔 줄 때 필요한 조건문의 변수
    public bool playerBmakeBalloon = false; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수
    public ObjectManager objectManager;

    public Item2 item2;

    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한

    Rigidbody2D rigid;
    CircleCollider2D collider;


    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;
    }

    void Update() {

        Move();
        Skill();

    }

    void FixedUpdate() {

        // 대각선 이동 제한
        Vector2 moveVec = isHorizonMove ? new Vector2(hAxis, 0) : new Vector2(0, vAxis);
        rigid.velocity = moveVec * playerSpeed;
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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 물풍선 풀 가져오기
            switch (bombRange)
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

        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerHealth == 0f)//왼쪽컨트롤키를 누르고 플레이어의 피가 0인 경우에만 실행
            // 0번째 활성화된 아이템을 사용
            if (item2.Activeitem.Length > 0 && item2.Activeitem[0] != null)
            {
                string itemName = item2.Activeitem[0].name; // 현재 사용한 아이템의 이름 가져오기
                UnityEngine.Debug.Log("플레이어A가" + itemName + "아이템을 사용함");
                // 0번째 아이템을 사용하려면 아래와 같이 호출
                item2.ActiveUseItem(item2.Activeitem[0].name);

                // 1번째 아이템을 0번째로 끌어올림
                if (item2.Activeitem.Length > 1 && item2.Activeitem[1] != null)
                {
                    item2.Activeitem[0] = item2.Activeitem[1];
                    item2.Activeitem[1] = null;
                }
            }

            else
            {
                UnityEngine.Debug.Log("활성화된 아이템 없음");
            }

    }
    //플레이어가 먹은 아이템 저장배열

    //플레이어 상태 스크립트(행동가능, 물풍선 같힌상태, 죽음)

    void CountDown()
    {
        playerBcountIndex--;
    }
    void MakeBalloon(string Power)
    {
        // 포지션
        Vector3 MoveVec = transform.position;
        MoveVec = new Vector3((float)Math.Round(MoveVec.x), (float)Math.Round(MoveVec.y), MoveVec.z); //소수점 버림


        GameObject[] WaterBalloon;
        WaterBalloon = objectManager.GetPool(Power);

        if (!playerBmakeBalloon && playerBcountIndex < bombPower)
        {
            Debug.Log("LeftShift");
            if (!WaterBalloon[playerBballonIndex].activeInHierarchy)
            {
                WaterBalloon[playerBballonIndex].SetActive(true);
                WaterBalloon[playerBballonIndex].transform.position = MoveVec;

            }

            // playerAballonIndex 가 20 을 넘어가지 않게 10으로 초기화
            if (playerBballonIndex == 19)
            {
                playerBballonIndex = 10;
            }
            else
            {
                playerBballonIndex++;
            }

            playerBcountIndex++;
            Invoke("CountDown", 3f);
        }
    }

    //아이템 먹었을때 스탯 값 증감
    void OnTriggerEnter2D(Collider2D collision)
    {
        string iname = collision.gameObject.name;

        UnityEngine.Debug.Log("플레이어가 오브젝트에 닿음");

        // 플레이어가 물풍선 안에 있을 시, 물풍선 생성 불가능하게 변경
        if (collision.gameObject.tag == "Balloon")
        {
                playerBmakeBalloon = true;
            /*if () { niddle 사용하지 않았을때의 조건문
               Invoke("DeatTime", 5);
           }*/
            Invoke("DeatTime", 5);
        } else
        {
            playerBmakeBalloon = false;
        }

        if (collision.gameObject.CompareTag("powerItem"))
        {
            if (bombPower < bombPowerMax)
            {
                item2.PowerAdd(iname);
            }
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
           UnityEngine.Debug.Log("물풍선 아이템에 닿음");
        }

        else if (collision.gameObject.CompareTag("speedItem"))
        {

            if (playerSpeed < playerSpeedMax)
            {
                item2.SpeedAdd(iname);
            }
            UnityEngine.Debug.Log("스피드 아이템에 닿았음");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }


        else if (collision.gameObject.CompareTag("rangeItem"))
        {
            if (bombRange < bombRangeMax)
            {
                item2.RangeAdd(iname);
            }
            UnityEngine.Debug.Log("사거리 증가 아이템에 닿음");
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.CompareTag("superMan"))
        {
            item2.SuperMan(iname);
            UnityEngine.Debug.Log("슈퍼맨!!");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

      // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
        if (collision.gameObject.CompareTag("ActiveItem"))
        {
            UnityEngine.Debug.Log("ActiveItem ADD");
            item2.AddActiveItem(collision.gameObject, 0);
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

      
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 물풍선 밖으로 나갈 시, 물풍선 생성 가능하게 변경
        if (collision.gameObject.tag == "Balloon")
        {
            playerBmakeBalloon = false;
        }
    }
    void DeatTime()
    {
        string playername = "B";
        gameManager.Death2(playername);
    }

}
