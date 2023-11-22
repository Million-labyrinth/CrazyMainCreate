using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForestDao : MonoBehaviour
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

    //ȿ����
    public AudioClip itemAddSound;//������ ȹ�� �Ҹ�
    public AudioClip balloonSetSound;//��ǳ�� ���� �Ҹ�
    public AudioClip balloonEscapeSound;//��ǳ�� Ż�� �Ҹ�
    public AudioClip balloonLockSound;//��ǳ�� ������ �Ҹ�
    public AudioClip deathSound; //ĳ���� ���� ��ǳ�� ������
    AudioSource audioSource;

    int playerAballonIndex = 0; // ��ǳ�� ������Ʈ Ǯ ����� �� �ʿ��� playerAballonIndex ����
    public int playerAcountIndex = 0; // ��ǳ���� ������ ��, �÷��̾ ������ ��ǳ���� ������ üũ�� �� �ʿ��� ����
    public bool playerAmakeBalloon; // count �� 2 �̻��� ��, �ٷ� ��ǳ���� ���� �����ϰ� ����� ���� ����

    public ObjectManager objectManager;
    GameObject[] WaterBalloon; // ������Ʈ Ǯ�� �������� ���� ����
    public GameObject p2shield;
    public GameObject p2niddle;

    CircleCollider2D collider;

    public Item item;
    public GameManager gameManager;

    float hAxis;
    float vAxis;
    bool isHorizonMove; // �밢�� �̵� ����


    bool hStay; // Horizontal Ű �ٿ�
    bool vStay; // Vertical Ű �ٿ�
    public GameObject pushBlock; // �νĵ� �� �� �ִ� �� ����� ����
    public GameObject pushBalloon; // �νĵ� �� �� �ִ� ��ǳ�� ����� ����
    public float nextPushTime; // �� �б� �ִ�(����) ��Ÿ��
    public float curPushTime; // �� �б� ����(����) ��Ÿ��

    Rigidbody2D rigid;
    public Animator anim;


    public GameObject Shieldeffect;
    public bool isDying = false; // ��ǳ���� ���� �ִ� �� ���θ� �Ǵ��ϴ� bool ��
    public float dyingTime; // ��ǳ���� ���� �ִ� �ð�

    Vector2 moveVec; // �÷��̾ �����̴� ����
    Vector2 rayDir; // Ray ����
    Vector2 rayStartPos; // Ray ������
    public SpriteRenderer playerRenderer; //��������Ʈ Ȱ��ȭ ��Ȱ��ȭ

    float playerSpeedRemeber; // ��ǳ���� ���� �� ���� �ӵ� �����
    bool getShoesItem; // �Ź� ������ ȹ�� ����
    bool canKickBalloon; // ��ǳ�� ���� ���� �� ��ǳ���� ���� ���� ����
    bool getPurpleDevil; // ����Ǹ� ȹ�� ����
    float purpleDevilTime; // ����Ǹ� ���� �ð�
    int changeColorCount = 0; // ����Ǹ� ȹ�� �� �� ��ȯ Ƚ�� üũ
    string purpleDevilMode; // ��ǳ�� ��ġ or ����Ű �ݴ�

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        //�÷��̾� ���۽� �⺻ ����
        bombPower = 1;
        bombRange = 1;
        playerSpeed = 4.0f;
        playerHealth = 0f;

        playerAmakeBalloon = true;
        getShoesItem = false;
        canKickBalloon = true;
        getPurpleDevil = false;
        playerSpeedRemeber = playerSpeed;

    }

    void Update()
    {
        Move();
        Ray();
        UseItem();

        if (Input.GetKeyDown(KeyCode.RightShift) && !isDying)
        {
            Skill();
        }
        /*colliderRay();*/

        if (isDying)
        {
            dyingTime += Time.deltaTime;

            if (dyingTime > 4)
            {
                DeadTime();
            }
        }

        if (purpleDevilMode == "Balloon" && getPurpleDevil)
        {
            Skill();
            changeColorCount = 0;
            purpleDevilTime += Time.deltaTime;

            if (purpleDevilTime >= 10)
            {
                getPurpleDevil = false;
                purpleDevilTime = 0;
                StopCoroutine("AfterGetPurpleDevil");
                changeColorCount = 0;
                playerRenderer.color = new Color(1, 1, 1);
            }
        }
    }
    void LateUpdate()
    {
        // �밢�� �̵� ����
        moveVec = isHorizonMove ? new Vector2(hAxis, 0) : new Vector2(0, vAxis);
        rigid.velocity = moveVec * playerSpeed;
    }

    void Move()
    {
        // �̵�
        if (purpleDevilMode == "Move" && getPurpleDevil)
        {
            hAxis = Input.GetAxisRaw("Horizontal") * -1;
            vAxis = Input.GetAxisRaw("Vertical") * -1;
        }
        else
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
        }
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        hStay = Input.GetButton("Horizontal");
        vStay = Input.GetButton("Vertical");

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
            }
            else if (hUp)
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

        // ������ �� �ִϸ��̼� Ʈ���� �ʱ�ȭ
        if (hAxis == 0 && vAxis == 0)
        {
            anim.ResetTrigger("vDown");
            anim.ResetTrigger("hDown");
        }

        // ����/��ǳ�� �б�� Ray ���� ����
        if (moveVec.y > 0)
        {
            rayDir = Vector2.up;
            rayStartPos = rigid.position + new Vector2(0, 0.25f);
        }
        else if (moveVec.y < 0)
        {
            rayDir = Vector2.down;
            rayStartPos = rigid.position - new Vector2(0, 0.95f);
        }
        else if (moveVec.x > 0)
        {
            rayDir = Vector2.right;
            rayStartPos = rigid.position + new Vector2(0.6f, -0.35f);
        }
        else if (moveVec.x < 0)
        {
            rayDir = Vector2.left;
            rayStartPos = rigid.position - new Vector2(0.6f, 0.35f);
        }

    }

    void Ray()
    {

        // ��ǳ���� ��ġ�� ���� ���ϰ� ���� �� �ʿ��� Ray + ��� �÷��̾� �ǰ� Ray
        Collider2D playerARay = Physics2D.OverlapCircle(rigid.position - new Vector2(0, 0.35f), 0.45f, LayerMask.GetMask("Balloon") | LayerMask.GetMask("Player B"));
        GameObject scanObject;

        if (playerARay != null)
        {
            scanObject = playerARay.gameObject;

            // ��ǳ�� ���� ���� ����
            if (scanObject.layer == 3)
            {
                playerAmakeBalloon = false;
            }

            // ��� �÷��̾ ��ǳ���� ���� ���� �� �ǰ� �����ϰ� ������ִ� �ڵ�
            if (scanObject.tag == "PlayerB")
            {
                Player2 playerBLogic = scanObject.GetComponent<Player2>();

                if (playerBLogic.isDying == true && isDying == false)
                {
                    playerBLogic.DeadTime();
                    //gameManager.touchDeath();
                    playerBLogic.dyingTime = 0;
                }
            }
        }
        else
        {
            scanObject = null;
            playerAmakeBalloon = true;
        }

        // �� �� �ִ� ���� / ��ǳ�� Ray
        if (hStay || vStay)
        {
            Debug.DrawRay(rayStartPos, rayDir * 0.5f, new Color(1, 0, 0));
            RaycastHit2D pushRay = Physics2D.Raycast(rayStartPos, rayDir, 0.5f, LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("BalloonGroup"));

            if (pushRay.collider != null)
            {
                curPushTime += Time.deltaTime; // ��ü �ν� �� �ð� ����

                // �� �� �ִ� ����
                if (pushRay.collider.gameObject.layer == 13)
                {
                    nextPushTime = 0.5f;
                    pushBlock = pushRay.collider.gameObject;
                    Box pushBlockLogic = pushBlock.GetComponent<Box>();

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

                        // �ð� �ʱ�ȭ
                        curPushTime = 0;
                    }
                }
                // ��ǳ��
                else if (pushRay.collider.gameObject.layer == 11)
                {
                    nextPushTime = 0.2f;
                    pushBalloon = pushRay.collider.gameObject;
                    PushBalloon pushBalloonLogic = pushBalloon.GetComponent<PushBalloon>();

                    if (curPushTime > nextPushTime && getShoesItem && canKickBalloon)
                    {
                        if (rayDir == Vector2.up && pushBalloonLogic.balloonLogic.upScanObject == null && pushBalloonLogic.balloonLogic.isBoom == false)
                        {
                            pushBalloonLogic.MoveBalloon("Up");
                        }
                        else if (rayDir == Vector2.down && pushBalloonLogic.balloonLogic.downScanObject == null && pushBalloonLogic.balloonLogic.isBoom == false)
                        {
                            pushBalloonLogic.MoveBalloon("Down");
                        }
                        else if (rayDir == Vector2.left && pushBalloonLogic.balloonLogic.leftScanObject == null && pushBalloonLogic.balloonLogic.isBoom == false)
                        {
                            pushBalloonLogic.MoveBalloon("Left");
                        }
                        else if (rayDir == Vector2.right && pushBalloonLogic.balloonLogic.rightScanObject == null && pushBalloonLogic.balloonLogic.isBoom == false)
                        {
                            pushBalloonLogic.MoveBalloon("Right");
                        }

                        // �ð� �ʱ�ȭ
                        curPushTime = 0;
                    }
                }
            }
            else
            {
                pushBlock = null;
                pushBalloon = null;
            }
        }
        else
        {
            // Ű �ٿ� ���� �� �ð� �ʱ�ȭ
            curPushTime = 0;
            nextPushTime = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f);
        // playerARay ���
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 0.35f), 0.45f);
    }


    void Skill()
    {
        // ��ǳ�� Ǯ ��������
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
        //�ٴ� ������ ���
        if (Input.GetKeyDown(KeyCode.RightControl))
        { //������Ʈ��Ű�� ������ �÷��̾��� ���°� true�� ��쿡�� ����
          // 0��° Ȱ��ȭ�� �������� ���
            if (item.Activeitem[0].name.Contains("shield") && !isDying)
            {
                Debug.Log("���� ���");
                Shieldeffect.SetActive(true);
                useShield = true;
                p2shield.SetActive(false);
                //�κ�ũ�� �̿��Ͽ� ���带 3�ʵڿ� ������ ��
                Invoke("stopShield", 2f);
            }
            else if (item.Activeitem[0].name.Contains("niddle") && !useniddle)
            {
                Debug.Log("�ٴ� ���");
                useniddle = true;

                p2niddle.SetActive(false);

                if (isDying)
                {

                    audioSource.clip = balloonEscapeSound;
                    audioSource.Play();

                    dyingTime = 0; // �״� �ð� �ʱ�ȭ
                    isDying = false; // ��ǳ�� Ż��

                    playerSpeed = 0;
                    anim.SetBool("isDamaged", false);
                    anim.SetBool("isDying", false);
                    anim.SetTrigger("useNiddle");
                    StartCoroutine(BackSpeed());
                }

            }
            string itemName = item.Activeitem[0].name; // ���� ����� �������� �̸� ��������
            UnityEngine.Debug.Log("�÷��̾�A��" + itemName + "�������� �����");
            // 0��° �������� ����Ϸ��� �Ʒ��� ���� ȣ��
            item.ActiveUseItem(item.Activeitem[0].name);
        }
    }
    //���� ���ߴ� �ڵ�
    private void stopShield()
    {
        useShield = false;
        Shieldeffect.SetActive(false);
    }
    //�÷��̾ ���� ������ ����迭

    IEnumerator BackSpeed()
    {
        yield return new WaitForSeconds(0.4f);
        playerSpeed = playerSpeedRemeber;
        StopCoroutine(BackSpeed());
        useniddle = false;
    }

    //�÷��̾� ���� ��ũ��Ʈ(�ൿ����, ��ǳ�� ��������, ����)

    void CountDown()
    {
        playerAcountIndex--;
    }
    void MakeBalloon(string Power)
    {
        // ������
        Vector3 MoveVec = transform.position - new Vector3(0f, 0.35f, 0f);
        MoveVec = new Vector3((float)Math.Round(MoveVec.x), (float)Math.Round(MoveVec.y), MoveVec.z); //�Ҽ��� ����

        WaterBalloon = objectManager.GetPool(Power);

        if (playerAmakeBalloon && playerAcountIndex < bombPower)
        {
            audioSource.clip = balloonSetSound;
            audioSource.Play();

            if (!WaterBalloon[playerAballonIndex].activeInHierarchy)
            {
                WaterBalloon[playerAballonIndex].SetActive(true);
                WaterBalloon[playerAballonIndex].transform.position = MoveVec;

            }

            // playerAballonIndex �� 10 �� �Ѿ�� �ʰ� 0���� �ʱ�ȭ
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

    IEnumerator AfterGetPurpleDevil()
    {
        while (changeColorCount <= 20)
        {
            playerRenderer.color = Color.magenta;
            yield return new WaitForSeconds(0.25f);
            playerRenderer.color = new Color(1, 1, 1);
            yield return new WaitForSeconds(0.25f);

            changeColorCount++;
        }
    }

    //������ �Ծ����� ���� �� ����
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "upWater" || collision.gameObject.tag == "downWater" || collision.gameObject.tag == "leftWater" || collision.gameObject.tag == "rightWater" || collision.gameObject.tag == "hitCollider")
        {
            Debug.Log(collision.gameObject.name);
            if (useShield == true)
            {
                //DeathTime(); ����ȵ�
            }
            else
            {
                DeathTime();
                getPurpleDevil = false;
                StopCoroutine("AfterGetPurpleDevil");
                playerRenderer.color = new Color(1, 1, 1);

            }
        }

        // ������
        // ��ǳ���� �������� �� ������ �� �԰� �ϴ� ���� "isDying == false"
        string itemName = collision.gameObject.name;

        if (isDying == false && collision.gameObject.layer == 17)
        {
            switch (collision.gameObject.tag)
            {
                case "powerItem":
                    item.PowerAdd();
                    break;
                case "powerItem2":
                    item.PowerAdd();
                    break;
                case "speedItem":
                    item.SpeedAdd();
                    playerSpeedRemeber = playerSpeed;
                    break;
                case "rangeItem":
                    item.RangeAdd(itemName);
                    break;
                case "shoesItem":
                    getShoesItem = true;
                    break;
                case "redDevil":
                    item.RedDeVil();
                    playerSpeedRemeber = playerSpeed;
                    break;
                case "ActiveItem":
                    // ���� �������� Activeitem �迭�� �߰� (ActiveItem �±׸� ���� �����۸� �߰�)
                    Debug.Log(itemName);
                    if (itemName.Contains("shield"))
                    {
                        if (p2niddle == true)
                        {
                            p2niddle.SetActive(false);
                        }
                        p2shield.SetActive(true);
                    }
                    else if (itemName.Contains("niddle"))
                    {
                        if (p2shield == true)
                        {
                            p2shield.SetActive(false);
                        }
                        p2niddle.SetActive(true);

                        useniddle = false; // �ٽ� �ٴ� ��밡���ϰ� �ʱ�ȭ

                    }
                    item.AddActiveItem(collision.gameObject, 0);
                    break;
                case "purpleDevil":
                    getPurpleDevil = true;
                    purpleDevilTime = 0;
                    StartCoroutine("AfterGetPurpleDevil");

                    int ran = UnityEngine.Random.Range(0, 2);
                    if (ran == 0)
                    {
                        purpleDevilMode = "Balloon";
                    }
                    else if (ran == 1)
                    {
                        purpleDevilMode = "Move";
                        Invoke("OffpurpleDevilmode", 10f);
                    }
                    break;
            }

            // ���� ������ ��Ȱ��ȭ
            collision.gameObject.SetActive(false);

            // ����
            audioSource.clip = itemAddSound;
            audioSource.Play();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "balloon")
        {
            canKickBalloon = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        playerRenderer.enabled = true;

        // ��ǳ�� ������ ������ Ʈ���� ��Ȱ��ȭ
        if (other.gameObject.layer == 8)
        {
            Collider2D col = other.gameObject.GetComponent<Collider2D>();
            col.isTrigger = false;
            canKickBalloon = true;
        }

    }

    void DeathTime()
    {
        Debug.Log("�÷��̾ �������� ����");
        anim.SetBool("isDamaged", true);
        anim.SetBool("isDying", true);


        playerSpeed = 0.8f;
        audioSource.clip = balloonLockSound;
        audioSource.Play();

        isDying = true;

        Debug.Log("player A Hit");
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
    private void OffpurpleDevilmode()
    {
        purpleDevilMode = "Nomal";
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }
}
