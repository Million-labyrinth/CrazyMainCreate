
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
//김인섭 왔다감2
public class Player : MonoBehaviour
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

    int playerAballonIndex = 0; // 물풍선 오브젝트 풀 사용할 때 필요한 playerAballonIndex 변수
    public int playerAcountIndex = 0; // 물풍선을 생성할 때, playerAmakeBalloon 을 false 값으로 바꿔 줄 때 필요한 조건문의 변수
    public bool playerAmakeBalloon = false; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수
    public ObjectManager objectManager;
    GameObject[] WaterBalloon; // 오브젝트 풀을 가져오기 위한 변수

    CircleCollider2D collider;

    public Item item;
    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한

    Rigidbody2D rigid;

    Vector2 moveVec; // 플레이어가 움직이는 방향
    Vector2 dirVec; // Ray 방향
    Vector2 PlusVec; // Ray 시작지점

    GameObject scanObject;

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
    void LateUpdate()
    {
        // 대각선 이동 제한
        moveVec = isHorizonMove ? new Vector2(hAxis, 0) : new Vector2(0, vAxis);
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

        // Ray 방향
        if (vDown && vAxis == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && vAxis == -1)
        {
            dirVec = Vector3.down;
        }
        else if (hDown && hAxis == -1)
        {
            dirVec = Vector3.left;
        }
        else if (hDown && hAxis == 1)
        {
            dirVec = Vector3.right;
        }

        // Ray
        Ray();

    }

    void Ray()
    {
        // 시작점 및 방향 설정
        if(moveVec.x == 0 && moveVec.y == 1)
        {
            PlusVec = new Vector2(0, 0.9f);
            dirVec = Vector2.up;
        } else if(moveVec.x == 0 && moveVec.y == -1)
        {
            PlusVec = new Vector2(0, -0.9f);
            dirVec = Vector2.down;
        } else if(moveVec.x == -1 && moveVec.y == 0)
        {
            PlusVec = new Vector2(-0.9f, 0);
            dirVec = Vector2.left;
        } else if (moveVec.x == 1 && moveVec.y == 0)
        {
            PlusVec = new Vector2(0.9f, 0);
            dirVec = Vector2.right;
        }

        // Ray
        Debug.DrawRay(rigid.position + PlusVec, dirVec * 0.3f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position + PlusVec, dirVec, 0.3f, LayerMask.GetMask("Balloon"));

            if (rayHit.collider != null)
            {
                scanObject = rayHit.collider.gameObject;
                moveVec = Vector2.zero;
                Debug.Log(scanObject.name);
            }
            else
            {
                scanObject = null;

            }
    }


    void Skill()
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
        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerHealth == 0f)//왼쪽컨트롤키를 누르고 플레이어의 피가 0인 경우에만 실행
            // 0번째 활성화된 아이템을 사용
            if (item.Activeitem.Length > 0 && item.Activeitem[0] != null)
            {
                string itemName = item.Activeitem[0].name; // 현재 사용한 아이템의 이름 가져오기
                UnityEngine.Debug.Log("플레이어A가" + itemName + "아이템을 사용함");
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
                UnityEngine.Debug.Log("활성화된 아이템 없음");
            }

    }
    //플레이어가 먹은 아이템 저장배열

    //플레이어 상태 스크립트(행동가능, 물풍선 같힌상태, 죽음)
   
   void CountDown()
{
    playerAcountIndex--;
}
void MakeBalloon(string Power)
{
    // 포지션
    Vector3 MoveVec = transform.position;
    MoveVec = new Vector3((float)Math.Round(MoveVec.x) , (float)Math.Round(MoveVec.y) , MoveVec.z); //소수점 버림

    WaterBalloon = objectManager.GetPool(Power);

    if (Input.GetKeyDown(KeyCode.RightShift) && !playerAmakeBalloon && playerAcountIndex < bombPower)
    {
            Debug.Log("RightShift");
        if (!WaterBalloon[playerAballonIndex].activeInHierarchy)
        {
            WaterBalloon[playerAballonIndex].SetActive(true);
            WaterBalloon[playerAballonIndex].transform.position = MoveVec;

        }

            // playerAballonIndex 가 10 을 넘어가지 않게 0으로 초기화
            if (playerAballonIndex == 9)
        {
            playerAballonIndex = 0;
        }
        else
        {
            playerAballonIndex++;
        }

        playerAcountIndex++;
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
            playerAmakeBalloon = true;
            /*if () { niddle 사용하지 않았을때의 조건문
                Invoke("DeatTime", 5);
            }*/
            Invoke("DeatTime", 5);
        } else { 
            playerAmakeBalloon = false; 
        }

        if (collision.gameObject.CompareTag("powerItem"))
        {
            if (bombPower < bombPowerMax)
            {
                item.PowerAdd(iname);
            }
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
           UnityEngine.Debug.Log("물풍선 아이템에 닿음");
        }

        else if (collision.gameObject.CompareTag("speedItem"))
        {

            if (playerSpeed < playerSpeedMax)
            {
                item.SpeedAdd(iname);
            }
            UnityEngine.Debug.Log("스피드 아이템에 닿았음");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }


        else if (collision.gameObject.CompareTag("rangeItem"))
        {
            if (bombRange < bombRangeMax)
            {
                item.RangeAdd(iname);
            }
            UnityEngine.Debug.Log("사거리 증가 아이템에 닿음");
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.CompareTag("superMan"))
        {
            item.SuperMan(iname);
            UnityEngine.Debug.Log("슈퍼맨!!");
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }

      // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
        if (collision.gameObject.CompareTag("ActiveItem"))
        {
            UnityEngine.Debug.Log("ActiveItem ADD");
            item.AddActiveItem(collision.gameObject, 0);
            // 먹은 아이템 비활성화
            collision.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 물풍선 밖으로 나갈 시, 물풍선 생성 가능하게 변경
        if (collision.gameObject.tag == "Balloon")
        {
            playerAmakeBalloon = false;
        }
    }

    void DeatTime()
    {
        string playername = "A";
        gameManager.Death(playername);
    }

}
