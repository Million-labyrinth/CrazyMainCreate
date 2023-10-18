
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
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
    public int playerAcountIndex = 0; // 물풍선을 생성할 때, 플레이어가 생성한 물풍선의 개수를 체크할 때 필요한 변수
    public bool playerAmakeBalloon; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수
    public ObjectManager objectManager;
    GameObject[] WaterBalloon; // 오브젝트 풀을 가져오기 위한 변수

    CircleCollider2D collider;

    public Item item;
    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한

    Rigidbody2D rigid;
    Animator anim;

    RaycastHit2D rayHit; // 물풍선 충돌 판정에 필요한 Ray
    Vector2 moveVec; // 플레이어가 움직이는 방향
    Vector2 dirVec; // Ray 방향
    Vector2 PlusVec; // 물풍선 충돌 판정Ray 시작지점
    Vector2 MakeVec; // 물풍선 생성 판정 Ray 시작지점
    GameObject scanObject; // Ray 로 스캔한 오브젝트

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;

        playerAmakeBalloon = true;

        PlusVec = new Vector2(0, 1.3f);
        dirVec = Vector2.up;
        rayHit = Physics2D.Raycast(rigid.position + PlusVec, dirVec, 0.4f, LayerMask.GetMask("Balloon A"));
    }

    void Update() {
        Move();
        Skill();
        Ray();
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
            anim.SetFloat("vertical", vAxis);
        }
        else if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vUp || hUp)
        {
            isHorizonMove = hAxis != 0;

            if(vUp)
            {
                anim.SetFloat("vertical", vAxis);
            }
        }

    }

    void Ray()
    {
        // Ray (물풍선 충돌 판정)  
        for (float angle = 0.0f; angle < 360.0f; angle += 5.0f)
        {
            float x = rigid.position.x + 1.6f * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = rigid.position.y + 1.6f * Mathf.Sin(Mathf.Deg2Rad * angle);
            
            Collider2D obj = Physics2D.OverlapPoint(new Vector2(x, y), LayerMask.GetMask("Balloon A"));

            if (obj != null)
            {
                obj.gameObject.layer = 10;
                Debug.Log(obj.name);
            }
        }

        // 물풍선을 겹치게 생성 못하게 만들 때 필요한 Ray
        Collider2D forMake = Physics2D.OverlapCircle(rigid.position - new Vector2(0, 0.1f), 0.5f, LayerMask.GetMask("Balloon A") | LayerMask.GetMask("Balloon B") | LayerMask.GetMask("Balloon Hard A") | LayerMask.GetMask("Balloon Hard B"));

        if (forMake != null)
        {
            playerAmakeBalloon = false;
        } else
        {
            playerAmakeBalloon = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f);
        // 물풍선 충돌 판정
        Gizmos.DrawWireSphere(transform.position, 1.6f);
        // 물풍선 생성 판정
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 0.1f), 0.5f);
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

    if (Input.GetKeyDown(KeyCode.RightShift) && playerAmakeBalloon && playerAcountIndex < bombPower)
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

        // 플레이어가 물풍선 위에 있을 시
        if (collision.gameObject.tag == "Balloon")
        {

            /*if () { niddle 사용하지 않았을때의 조건문
                Invoke("DeatTime", 5);
            }*/
            Invoke("DeatTime", 5);
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

    void DeatTime()
    {
        string playername = "A";
        gameManager.Death(playername);
    }

}
