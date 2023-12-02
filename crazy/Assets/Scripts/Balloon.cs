using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Balloon : MonoBehaviour
{
    Animator anim;
    BoxCollider2D collider;
    public SpriteRenderer sprite;
    public Rigidbody2D rigid;

    public AudioClip boomSound; //물풍선 터질때
    AudioSource audioSource;
    bool isPlaying = false;


    public GameObject upWater;
    public GameObject downWater;
    public GameObject leftWater;
    public GameObject rightWater;

    public GameObject mainBalloon; // 물풍선 본체
    public BoxCollider2D colliderA; // 플레이어 A 전용 피격 판정 collider
    public BoxCollider2D colliderB; // 플레이어 B 전용 피격 판정 collider
    public GameObject hitCollider; // 물풍선 피격 판정 collider

    public GameObject upScanObject; // upRay 에 인식되는 오브젝트 변수
    public GameObject downScanObject; // downRay 에 인식되는 오브젝트 변수
    public GameObject leftScanObject; // leftRay 에 인식되는 오브젝트 변수
    public GameObject rightScanObject; // rightRay 에 인식되는 오브젝트 변수

    bool waterLineActive = true; // 물줄기 위에 물풍선 설치 시, 안 터지게 만들기 위한 변수

    // 아이템이 여러 개 나오는 오류 방지
    bool isHitUpBlock; // 물풍선 UpRay 가 Block 을 인식했을 때, Block 부수기
    bool isHitDownBlock; // 물풍선 DownRay 가 Block 을 인식했을 때, Block 부수기
    bool isHitLeftBlock; // 물풍선 LeftRay 가 Block 을 인식했을 때, Block 부수기
    bool isHitRightBlock; // 물풍선 RightRay 가 Block 을 인식했을 때, Block 부수기

    // 물풍선 활성화 시간
    public float enableTime;
    bool isEnable;
    public bool isBoom;

    void Awake()
    {
        anim = GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        isEnable = true;
        isBoom = false;

        upWater.SetActive(false);
        downWater.SetActive(false);
        leftWater.SetActive(false);
        rightWater.SetActive(false);
        hitCollider.SetActive(false);

        // 블럭 부수기 조건 초기화
        isHitUpBlock = false;
        isHitDownBlock = false;
        isHitLeftBlock = false;
        isHitRightBlock = false;

    }

    void Update()
    {
        BalloonRay();

        if (isEnable)
        {
            enableTime += Time.deltaTime;

            if (enableTime > 0.2f)
            {
                WaterLineActive();
            }
            if (enableTime > 2.5f && !isBoom)
            {
                Boom();
                isBoom = true;
                Invoke("Finish", 0.5f);
                isEnable = false;
            }
        }
    }



    void BalloonRay()
    {
        // Ray
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.7f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));

        if (upRayHit.collider != null)
        {
            upScanObject = upRayHit.collider.gameObject;
        }
        else
        {
            upScanObject = null;
        }
        if (downRayHit.collider != null)
        {
            downScanObject = downRayHit.collider.gameObject;
        }
        else
        {
            downScanObject = null;
        }
        if (leftRayHit.collider != null)
        {
            leftScanObject = leftRayHit.collider.gameObject;
        }
        else
        {
            leftScanObject = null;
        }
        if (rightRayHit.collider != null)
        {
            rightScanObject = rightRayHit.collider.gameObject;
        }
        else
        {
            rightScanObject = null;
        }

        Collider2D mainARay = Physics2D.OverlapCircle(rigid.position, 0.45f, LayerMask.GetMask("Player A"));

        if (mainARay == null)
        {
            StartCoroutine("ChangeATrigger");
        }

        Collider2D mainBRay = Physics2D.OverlapCircle(rigid.position, 0.45f, LayerMask.GetMask("Player B"));

        if (mainBRay == null)
        {
            StartCoroutine("ChangeBTrigger");
        }

        Collider2D grassRay = Physics2D.OverlapCircle(rigid.position, 0.45f, LayerMask.GetMask("Grass"));

        if(grassRay != null)
        {
            sprite.enabled = false;
        } else
        {
            sprite.enabled = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 1f, 0f);
        // mainRay 모습
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }

    IEnumerator ChangeATrigger()
    {
        yield return new WaitForSeconds(0.1f);
        colliderA.isTrigger = false;

        StopCoroutine("ChangeATrigger");
    }

    IEnumerator ChangeBTrigger()
    {
        yield return new WaitForSeconds(0.1f);
        colliderB.isTrigger = false;

        StopCoroutine("ChangeBTrigger");
    }


    void Boom()
    {

        // 애니메이션
        anim.SetBool("Boom", true);

        // 물줄기, Main Collider 활성화
        // Ray 에 블럭이 인식이 안될 경우에만 활성화
        if (upScanObject == null || upScanObject.tag == "grass")
        {
            upWater.SetActive(true);
            isHitUpBlock = false;

        }
        else if (upScanObject != null)
        {
            if (!isHitUpBlock && upScanObject.tag == "Block")
            {
                // Ray 에 블럭이 인식될 경우 블럭 Hit 작동
                Block upBlock = upScanObject.GetComponent<Block>();
                upBlock.anim.SetBool("Hit", true);
                upBlock.Invoke("Hit", 0.55f);
                isHitUpBlock = true;
            }
        }

        if (downScanObject == null || downScanObject.tag == "grass")
        {
            downWater.SetActive(true);
            isHitDownBlock = false;

        }
        else if (downScanObject != null)
        {
            if (!isHitDownBlock && downScanObject.tag == "Block")
            {
                Block downBlock = downScanObject.GetComponent<Block>();
                downBlock.anim.SetBool("Hit", true);
                downBlock.Invoke("Hit", 0.55f);
                isHitDownBlock = true;

            }

        }

        if (leftScanObject == null || leftScanObject.tag == "grass")
        {
            leftWater.SetActive(true);
            isHitLeftBlock = false;

        }
        else if (leftScanObject != null)
        {
            if (!isHitLeftBlock && leftScanObject.tag == "Block")
            {
                Block leftBlock = leftScanObject.GetComponent<Block>();
                leftBlock.anim.SetBool("Hit", true);
                leftBlock.Invoke("Hit", 0.55f);

                isHitLeftBlock = true;
            }

        }

        if (rightScanObject == null || rightScanObject.tag == "grass")
        {
            rightWater.SetActive(true);
            isHitRightBlock = false;

        }
        else if (rightScanObject != null)
        {
            if (!isHitRightBlock && rightScanObject.tag == "Block")
            {
                Block rightBlock = rightScanObject.GetComponent<Block>();
                rightBlock.anim.SetBool("Hit", true);
                rightBlock.Invoke("Hit", 0.55f);

                isHitRightBlock = true;
            }

        }

        hitCollider.SetActive(true);


        // 사운드
        if (!isPlaying)
        {
            audioSource.clip = boomSound;
            audioSource.Play();
            isPlaying = true;
            Invoke("SoundReset", audioSource.clip.length);

        }

    }
    void SoundReset()
    {
        isPlaying = false;
    }


    void Finish()
    {
        // 애니메이션
        anim.SetBool("Boom", false);

        mainBalloon.SetActive(false);

        // 물줄기, hit Collider 비활성화
        upWater.SetActive(false);
        downWater.SetActive(false);
        leftWater.SetActive(false);
        rightWater.SetActive(false);
        hitCollider.SetActive(false);

        waterLineActive = true;
        isEnable = false;
        sprite.enabled = false;

        // 트리거 활성화 (collider = Player A 물풍선, mainCol = Player B 물풍선)
        colliderA.isTrigger = true;
        colliderB.isTrigger = true;

        // 사운드 초기화
        audioSource.clip = null;

        // 블럭 부수기 조건 초기화
        isHitUpBlock = false;
        isHitDownBlock = false;
        isHitLeftBlock = false;
        isHitRightBlock = false;

        // 시간 초기화
        enableTime = 0;
    }
    void WaterLineActive()
    {
        waterLineActive = false;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        // 다른 물풍선의 물줄기에 맞으면 바로 터지게 만듦
        if (obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater" || obj.gameObject.tag == "bossWater")
        {
            // 물줄기 위에서는 작동을 안하게 만듦
            if (!waterLineActive)
            {
                Boom();
                Invoke("Finish", 0.5f);
            }
        }
    }

}
