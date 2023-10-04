using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//김인섭 왔다감2
public class Player : MonoBehaviour
{
    public GameObject WaterBalloon1; // 물풍선 1
    public GameObject WaterBalloon2; // 물풍선 2
    public GameObject WaterBalloon3; // 물풍선 3
    public GameObject WaterBalloon4; // 물풍선 4
    public GameObject WaterBalloon5; // 물풍선 5
    public GameObject WaterBalloon6; // 물풍선 6
    public GameObject WaterBalloon7; // 물풍선 7

    public int bombPower;
    public int bombPowerMax = 10;
    public int bombRange;
    public int bombRangeMax = 7;
    public float playerSpeed;
    public float playerSpeedMax = 7f;
    public float playerHealth;
    public float playerMaxHealth = 2f;
    public string basicBubble;

    public int playerHP = 2;

    public Item item;


    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한
    bool sDown; // 스페이스 바 다운

    Rigidbody2D rigid;
    CircleCollider2D circol;
    private PlayerStateManager stateManager;


    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        circol = GetComponent<CircleCollider2D>();
        stateManager = GetComponent<PlayerStateManager>();

        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;
        playerHP = 2;

    }

    void Update() {

        Move();
        Skill();
        GetPlayerState()
        

    }
    void FixedUpdate()
    {

        // 대각선 이동 제한
        Vector2 moveVec = isHorizonMove ? new Vector2(hAxis, 0) : new Vector2(0, vAxis);
        rigid.velocity = moveVec * playerSpeed;
    }
    void Move()
    {
        // 이동
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");


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
        Vector3 MoveVec = transform.position;
        MoveVec = new Vector3((float)Math.Round(MoveVec.x) , (float)Math.Round(MoveVec.y) , MoveVec.z); //소수점 버림
        // 물풍선
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            switch (bombPower)
            {
                case 1:
                    if (!WaterBalloon1.activeInHierarchy)
                    {
                        WaterBalloon1.SetActive(true);
                        WaterBalloon1.transform.position = MoveVec;
                    }
                    break;
                case 2:
                    if (!WaterBalloon2.activeInHierarchy)
                    {
                        WaterBalloon2.SetActive(true);
                        WaterBalloon2.transform.position = MoveVec;
                    }
                    break;
                case 3:
                    if (!WaterBalloon3.activeInHierarchy)
                    {
                        WaterBalloon3.SetActive(true);
                        WaterBalloon3.transform.position = MoveVec;
                    }
                    break;
                case 4:
                    if (!WaterBalloon4.activeInHierarchy)
                    {
                        WaterBalloon4.SetActive(true);
                        WaterBalloon4.transform.position = MoveVec;
                    }
                    break;
                case 5:
                    if (!WaterBalloon5.activeInHierarchy)
                    {
                        WaterBalloon5.SetActive(true);
                        WaterBalloon5.transform.position = MoveVec;
                    }
                    break;
                case 6:
                    if (!WaterBalloon6.activeInHierarchy)
                    {
                        WaterBalloon6.SetActive(true);
                        WaterBalloon6.transform.position = MoveVec;
                    }
                    break;
                case 7:
                    if (!WaterBalloon7.activeInHierarchy)
                    {
                        WaterBalloon7.SetActive(true);
                        WaterBalloon7.transform.position = MoveVec;
                    }
                    break;
            }
        }
        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.RightControl) && playerHealth == 0f)//왼쪽컨트롤키를 누르고 플레이어의 피가 0인 경우에만 실행
            // 0번째 활성화된 아이템을 사용
            if (item.Activeitem.Length > 0 && item.Activeitem[0] != null)
            {
                Debug.Log("플레이어A가 액티브 아이템을 사용함");
                // 0번째 아이템을 사용하려면 아래와 같이 호출
                item.ActiveUseItem(item.Activeitem[0].name);

                // 1번째 아이템을 0번째로 끌어올림
                if (item.Activeitem.Length > 1 && item.Activeitem[1] != null)
                {
                    item.Activeitem[0] = item.Activeitem[1];
                    item.Activeitem[1] = null;
                }
            }

            else
            {
                Debug.Log("활성화된 아이템 없음");
            }

    }
    //플레이어가 먹은 아이템 저장배열

    //플레이어 상태 스크립트(행동가능, 물풍선 같힌상태, 죽음)
    void GetPlayerState(){
    // 예: 플레이어 상태를 물풍선 같힌 상태로 설정
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        stateManager.SetPlayerState(1);
    }

    // 예: 플레이어 상태를 죽음 상태로 설정
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        stateManager.SetPlayerState(2);
    }

    // 예: 현재 플레이어 상태를 가져와서 확인
    int currentPlayerState = stateManager.GetPlayerState();
    Debug.Log("현재 플레이어 상태: " + currentPlayerState);
    }
    

    //아이템 먹었을때 스탯 값 증감
    void OnTriggerEnter2D(Collider2D collision)
    {
        string iname = collision.gameObject.name;

        Debug.Log("플레이어가 오브젝트에 닿음");

        if (collision.gameObject.CompareTag("powerItem"))
        {
            if (bombPower < bombPowerMax)
            {
                item.PowerAdd(iname);
            }
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
            Debug.Log("물풍선 아이템에 닿음");
        }

        else if (collision.gameObject.CompareTag("speedItem"))
        {

            if (playerSpeed < playerSpeedMax)
            {
                item.SpeedAdd(iname);
            }
            Debug.Log("스피드 아이템에 닿았음");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }


        else if (collision.gameObject.CompareTag("rangeItem"))
        {
            if (bombRange < bombRangeMax)
            {
                item.RangeAdd(iname);
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
                Debug.Log("사거리 증가 아이템에 닿음");
            }
        }

        else if (collision.gameObject.CompareTag("superMan"))
        {
            item.SuperMan(iname);
            Debug.Log("슈퍼맨!!");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

        // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
        if (collision.gameObject.CompareTag("ActiveItem"))
        {
            item.AddActiveItem(collision.gameObject, 0);
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);

        }
      
        
    }



}
