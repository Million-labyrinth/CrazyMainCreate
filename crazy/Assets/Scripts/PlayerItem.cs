using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItem : MonoBehaviour
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


    public GameObject WaterBalloon1; // 물풍선 1
    public GameObject WaterBalloon2; // 물풍선 2
    public GameObject WaterBalloon3; // 물풍선 3
    public GameObject WaterBalloon4; // 물풍선 4
    public GameObject WaterBalloon5; // 물풍선 5

    public Item item;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한
    bool sDown; // 스페이스 바 다운

    Rigidbody2D rigid;
    CapsuleCollider2D capcol;


    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        capcol = GetComponent <CapsuleCollider2D>();

    }


    void Start()
    {
        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 1.0f; 
        playerHealth = 0f;


    }

    void Update() {
        // 이동
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if(vDown) {
            isHorizonMove = false;
        } else if(hDown) {
            isHorizonMove = true;
        } else if(vUp || hUp) {
            isHorizonMove = hAxis != 0;
        }


        // 물풍선
        if (Input.GetButtonDown("Jump")) {
                switch(bombPower) {
                case 1:
                        if(!WaterBalloon1.activeInHierarchy) {
                            WaterBalloon1.SetActive(true);

                            Invoke("FinishBoom", 5f);
                        }
                    break;
                case 2:
                    if(!WaterBalloon2.activeInHierarchy) {
                        WaterBalloon2.SetActive(true);

                        Invoke("FinishBoom", 5f);
                    }
                    break;
                case 3:
                    if(!WaterBalloon3.activeInHierarchy) {
                        WaterBalloon3.SetActive(true);

                        Invoke("FinishBoom", 5f);
                    }
                    break;
                case 4:
                    if(!WaterBalloon4.activeInHierarchy) {
                        WaterBalloon4.SetActive(true);

                        Invoke("FinishBoom", 5f);
                    }
                    break;
                case 5: 
                    if(!WaterBalloon5.activeInHierarchy) {
                        WaterBalloon5.SetActive(true);

                        Invoke("FinishBoom", 5f);
                    }
                    break;
            }
        }

        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerHealth == 0f)//왼쪽컨트롤키를 누르고 플레이어의 피가 0인 경우에만 실행
            // 0번째 활성화된 아이템을 사용
            if (item.Activeitem.Length > 0 && item.Activeitem[0] != null)
            {
                Debug.Log("플레이어가 액티브 아이템을 사용함");
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

    void FixedUpdate() {

        // 대각선 이동 제한
        UnityEngine.Vector2 moveVec = isHorizonMove ? new UnityEngine.Vector2(hAxis, 0) : new UnityEngine.Vector2(0, vAxis);
        rigid.velocity = moveVec.normalized * playerSpeed;
    }

    void FinishBoom() {
        WaterBalloon1.SetActive(false);
        WaterBalloon2.SetActive(false);
        WaterBalloon3.SetActive(false);
        WaterBalloon4.SetActive(false);
        WaterBalloon5.SetActive(false);
    }

    //플레이어가 먹은 아이템 저장배열


    //아이템 먹었을때 스탯 값 증감
    void OnTriggerEnter2D(Collider2D collision)
    {
        string iname = collision.gameObject.name;

        Debug.Log("플레이어가 오브젝트에 닿음");

        if (collision.gameObject.CompareTag("powerItem") || collision.gameObject.CompareTag("powerItem2"))
        {
            if(bombPower < bombPowerMax)
            {
                item.PowerAdd();
            }
            Debug.Log("물풍선 아이템에 닿음");
        }

        else if (collision.gameObject.CompareTag("speedItem"))
        {
            
            if (playerSpeed < playerSpeedMax)
            {
                item.SpeedAdd();
            }
            Debug.Log("스피드 아이템에 닿았음");
        }
            
      
        else if (collision.gameObject.CompareTag("rangeItem"))
        {
            if(bombRange < bombRangeMax)
            {
                item.RangeAdd(iname);
                Debug.Log("사거리 증가 아이템에 닿음");
            }
        }

        else if (collision.gameObject.CompareTag("superMan"))
        {
            item.SuperMan(iname);
            Debug.Log("슈퍼맨!!");
        }

        // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
        if (collision.gameObject.CompareTag("ActiveItem"))
        {
            item.AddActiveItem(collision.gameObject, 0);
        }

        // 먹은 아이템 비활성화
        collision.gameObject.SetActive(false);
    }


}



