using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
using System.Linq.Expressions;

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
    public bool useShield = false;
    public bool useniddle = false;
    public bool playerDead = false;

    int playerBballonIndex = 10; // 물풍선 오브젝트 풀 사용할 때 필요한 playerBballonIndex 변수
    public int playerBcountIndex = 0; // 물풍선을 생성할 때, 플레이어가 생성한 물풍선의 개수를 체크할 때 필요한 변수
    public bool playerBmakeBalloon; // count 가 2 이상일 시, 바로 물풍선을 생성 가능하게 만들기 위한 변수

    public ObjectManager objectManager;
    GameObject[] WaterBalloon; // 오브젝트 풀을 가져오기 위한 변수
    public GameObject p1shield;
    public GameObject p1niddle;

    //효과음
    public AudioClip itemAddSound;//아이템 획득 소리
    public AudioClip balloonSetSound;//물풍선 놓는 소리
    public AudioClip balloonEscapeSound;//물풍선 탈출 소리
    public AudioClip balloonLockSound;//물풍선 갇힐때 소리
    public AudioClip deathSound; //캐릭터 갇힌 물풍선 터질때
    AudioSource audioSource;

    CircleCollider2D collider;

    public Item2 item2;
    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // 대각선 이동 제한


    bool hStay; // Horizontal 키 다운
    bool vStay; // Vertical 키 다운
    public GameObject pushBlock; // 인식된 밀 수 있는 블럭 저장용 변수
    public float nextPushTime; // 블럭 밀기 최대(설정) 쿨타임
    public float curPushTime; // 블럭 밀기 현재(충전) 쿨타임

    Rigidbody2D rigid;
    public Animator anim;


    public GameObject Shieldeffect;
    public bool isDying = false; // 물풍선에 갇혀 있는 지 여부를 판단하는 bool 값
    public float dyingTime; // 물풍선에 갇혀 있는 시간

    Vector2 moveVec; // 플레이어가 움직이는 방향
    Vector2 rayDir; // Ray 방향
    public SpriteRenderer playerRenderer; //스프라이트 활성화 비활성화

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        //플레이어 시작시 기본 스탯
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;

        nextPushTime = 0.5f;

        playerBmakeBalloon = true;
    }

    void Update()
    {
        Move();
        Skill();
        Ray();
        UseItem();
        /*colliderRay();*/

        if (isDying)
        {
            dyingTime += Time.deltaTime;

            if (dyingTime > 4)
            {
                DeadTime();
            }
        }
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
        hAxis = Input.GetAxisRaw("2PH");
        vAxis = Input.GetAxisRaw("2PV");

        bool hDown = Input.GetButtonDown("2PH");
        bool vDown = Input.GetButtonDown("2PV");
        bool hUp = Input.GetButtonUp("2PH");
        bool vUp = Input.GetButtonUp("2PV");

        hStay = Input.GetButton("2PH");
        vStay = Input.GetButton("2PV");

        if (vDown)
        {
            isHorizonMove = false;
            anim.SetFloat("vAxisRaw", vAxis);

            anim.SetTrigger("vDown");
            anim.ResetTrigger("hDown");
        }
        else if (hDown)
        {
            isHorizonMove = true;

            anim.SetTrigger("hDown");
            anim.ResetTrigger("vDown");
        }
        else if (vUp || hUp)
        {
            isHorizonMove = hAxis != 0;

            if (vUp)
            {
                anim.SetFloat("vAxisRaw", vAxis);

                anim.SetTrigger("vDown");
                anim.ResetTrigger("hDown");
            } else if(hUp)
            {
                anim.SetTrigger("hDown");
                anim.ResetTrigger("vDown");
            }
        }
        if (anim.GetInteger("hAxisRaw") != hAxis)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)hAxis);
        }
        else if (anim.GetInteger("vAxisRaw") != vAxis)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)vAxis);
        }
        else
            anim.SetBool("isChange", false);


        // 멈췄을 때 애니메이션 트리거 초기화
        if (hAxis == 0 && vAxis == 0)
        {
            anim.ResetTrigger("vDown");
            anim.ResetTrigger("hDown");
        }

        // 상자 밀기용 Ray 방향 설정
        if (moveVec.y > 0)
        {
            rayDir = Vector2.up;
        }
        else if (moveVec.y < 0)
        {
            rayDir = Vector2.down;
        }
        else if (moveVec.x > 0)
        {
            rayDir = Vector2.right;
        }
        else if (moveVec.x < 0)
        {
            rayDir = Vector2.left;
        }

    }

    void Ray()
    {
        // 물풍선을 겹치게 생성 못하게 만들 때 필요한 Ray + 상대 플레이어 피격 Ray
        Collider2D playerBRay = Physics2D.OverlapCircle(rigid.position - new Vector2(0, 0.1f), 0.45f, LayerMask.GetMask("Balloon A") | LayerMask.GetMask("Balloon B") | LayerMask.GetMask("Player A"));
        GameObject scanObject;

        if (playerBRay != null)
        {
            scanObject = playerBRay.gameObject;

            // 물풍선 생성 가능 여부
            if (scanObject.layer == 8 || scanObject.layer == 9)
            {
                playerBmakeBalloon = false;
            }
            

            // 상대 플레이어가 물풍선에 갇혀 있을 때 피격 가능하게 만들어주는 코드
            if (scanObject.tag == "PlayerA")
            {
                Player playerALogic = scanObject.GetComponent<Player>();

                if (playerALogic.isDying == true && isDying == false)
                {
                    playerALogic.DeadTime();
                    //gameManager.touchDeath();
                    playerALogic.dyingTime = 0;
                }
            }
        }
        else
        {
            scanObject = null;
            playerBmakeBalloon = true;
        }

        // 밀 수 있는 상자 Ray
        if (hStay || vStay)
        {
            Debug.DrawRay(rigid.position - new Vector2(0, 0.1f), rayDir * 0.7f, new Color(1, 0, 0));
            RaycastHit2D pushRay = Physics2D.Raycast(rigid.position - new Vector2(0, 0.1f), rayDir, 0.7f, LayerMask.GetMask("MoveBlock"));

            if (pushRay.collider != null)
            {
                pushBlock = pushRay.collider.gameObject;
                Box pushBlockLogic = pushBlock.GetComponent<Box>();



                curPushTime += Time.deltaTime;

                if (curPushTime > nextPushTime)
                {
                    if (rayDir == Vector2.up && pushBlockLogic.upScanObject == null)
                    {
                        pushBlockLogic.MoveBox("Up");

                    }
                    else if (rayDir == Vector2.down && pushBlockLogic.downScanObject == null)
                    {
                        pushBlockLogic.MoveBox("Down");

                    }
                    else if (rayDir == Vector2.left && pushBlockLogic.leftScanObject == null)
                    {
                        pushBlockLogic.MoveBox("Left");

                    }
                    else if (rayDir == Vector2.right && pushBlockLogic.rightScanObject == null)
                    {
                        pushBlockLogic.MoveBox("Right");
                    }

                    // 시간 초기화
                    curPushTime = 0;
                }
            }
            else
            {
                pushBlock = null;
            }
        }
        else
        {
            // 키 다운 해제 시 시간 초기화
            curPushTime = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f);
        // 물풍선 생성 판정
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 0.1f), 0.45f);
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
    }

    void UseItem()
    {
        //바늘 아이템 사용
        if (Input.GetKeyDown(KeyCode.LeftControl) && gameManager.plB == true)
        { //왼쪽컨트롤키를 누르고 플레이어의 상태가 true인 경우에만 실행
          // 0번째 활성화된 아이템을 사용
            if (item2.Activeitem[0].name.Contains("shield"))
            {
                Debug.Log("쉴드 사용");
                Shieldeffect.SetActive(true);
                useShield = true;
                p1shield.SetActive(false);
                //인보크를 이용하여 쉴드를 3초뒤에 꺼지게 함
                Invoke("stopShield", 2f);
            }
            else if (item2.Activeitem[0].name.Contains("niddle"))
            {
                Debug.Log("바늘 사용");
                useniddle = true;
                p1niddle.SetActive(false);
                Debug.Log(useniddle);
                audioSource.clip = balloonEscapeSound;
                audioSource.Play();

                dyingTime = 0; // 죽는 시간 초기화
                isDying = true; // 물풍선 탈출

            }
            string itemName = item2.Activeitem[0].name; // 현재 사용한 아이템의 이름 가져오기
            UnityEngine.Debug.Log("플레이어B가" + itemName + "아이템을 사용함");
            // 0번째 아이템을 사용하려면 아래와 같이 호출
            item2.ActiveUseItem(item2.Activeitem[0].name);
        }
    }
    //쉴드 멈추는 코드
    private void stopShield()
    {
        useShield = false;
        Shieldeffect.SetActive(false);
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

        WaterBalloon = objectManager.GetPool(Power);

        if (Input.GetKeyDown(KeyCode.LeftShift) && playerBmakeBalloon && playerBcountIndex < bombPower)
        {
            Debug.Log("LeftShift");
            audioSource.clip = balloonSetSound;
            audioSource.Play();
            if (!WaterBalloon[playerBballonIndex].activeInHierarchy)
            {
                WaterBalloon[playerBballonIndex].SetActive(true);
                WaterBalloon[playerBballonIndex].transform.position = MoveVec;

            }

            // playerBballonIndex 가 10 을 넘어가지 않게 0으로 초기화
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


        if (collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater" || collision.gameObject.tag == "BalloonCollider")
        {
            if (useShield == true)
            {
                //DeathTime(); 실행안됨
            }
            else
            {
                    DeathTime();  
            }

            Debug.Log(collision.name);

        }


        string iname = collision.gameObject.name;

        // 물풍선에 갇혀있을 땐 아이템 못 먹게 하는 조건 "isDying == false"
        if (isDying == false)
        {
            // 플레이어가 물풍선 위에 있을 시
            if (collision.gameObject.tag == "Balloon")
            {



            }

            if (collision.gameObject.CompareTag("powerItem"))
            {
                if (bombPower < bombPowerMax)
                {
                    item2.PowerAdd(iname);
                    audioSource.clip = itemAddSound;
                    audioSource.Play();
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
                    audioSource.clip = itemAddSound;
                    audioSource.Play();
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
                    audioSource.clip = itemAddSound;
                    audioSource.Play();
                }
                UnityEngine.Debug.Log("사거리 증가 아이템에 닿음");
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
            }

            else if (collision.gameObject.CompareTag("superMan"))
            {
                item2.SuperMan(iname);
                UnityEngine.Debug.Log("슈퍼맨!!");
                audioSource.clip = itemAddSound;
                audioSource.Play();
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
            }

            // 먹은 아이템을 Activeitem 배열에 추가 (ActiveItem 태그를 가진 아이템만 추가)
            if (collision.gameObject.CompareTag("ActiveItem"))
            {
                if (collision.gameObject.name.Contains("shield"))
                {
                    audioSource.clip = itemAddSound;
                    audioSource.Play();
                    if (p1niddle == true)
                    {
                        p1niddle.SetActive(false);
                    }
                    p1shield.SetActive(true);
                }
                else if (collision.gameObject.name.Contains("niddle"))
                {
                    audioSource.clip = itemAddSound;
                    audioSource.Play();
                    if (p1shield == true)
                    {
                        p1shield.SetActive(false);
                    }
                    p1niddle.SetActive(true);

                    useniddle = false; // 다시 바늘 사용가능하게 초기화
                }
                UnityEngine.Debug.Log("ActiveItem ADD");
                item2.AddActiveItem(collision.gameObject, 0);
                // 먹은 아이템 비활성화
                collision.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        playerRenderer.enabled = true;

        // 물풍선 밖으로 나가면 트리거 비활성화
        if (other.gameObject.layer == 9)
        {
            Collider2D col = other.gameObject.GetComponent<Collider2D>();
            col.isTrigger = false;
        }

    }

    void DeathTime()
    {
        Debug.Log("플레이어가 데미지를 입음");
        anim.SetBool("isDamaged", true);
        playerSpeed = 0.8f;
        audioSource.clip = balloonLockSound;
        audioSource.Play();

        isDying = true;

        Debug.Log("player B Hit");
    }

    public void DeadTime()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
        anim.SetTrigger("isDead");
        playerSpeed = 0f;
        playerDead = true;
        isDying = false;

        gameManager.Death();
    }


/*  이것은 구판 오더레이어
    void colliderRay()
    {
        // Ray
        Debug.DrawRay(transform.position - new Vector3(0.35f, 0.65f, 0), Vector3.right * 0.7f, new Color(1, 1, 1));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position - new Vector3(0.35f, 0.65f, 0), Vector3.right, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));

        if (downRayHit.collider != null)
        {
            GameObject downObj = downRayHit.collider.gameObject;
            SpriteRenderer orderInLayer = downObj.GetComponent<SpriteRenderer>();
            int objOrder = orderInLayer.sortingOrder;
            playerRenderer.sortingOrder = orderInLayer.sortingOrder - 1;
            // orderInLayer.sortingOrder += playerRenderer.sortingOrder;
            objOrder = orderInLayer.sortingOrder;
            Debug.Log(downObj.name);
        }
        else if (downRayHit.collider == null)
        {
            playerRenderer.sortingOrder = 13;
        }
    }*/
}
