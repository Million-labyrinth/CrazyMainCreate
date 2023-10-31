using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    Animator anim;
    BoxCollider2D collider;

    public GameObject upWater;
    public GameObject downWater;
    public GameObject leftWater;
    public GameObject rightWater;
    public GameObject balloonCollider;

    public GameObject mainBalloon; // 물풍선 본체
    public GameObject MainCollider; // 물풍선이 터지면 활성화 될, 물풍선 Collider
    BoxCollider2D mainCol;

    GameObject upScanObject; // upRay 에 인식되는 오브젝트 변수
    GameObject downScanObject; // downRay 에 인식되는 오브젝트 변수
    GameObject leftScanObject; // leftRay 에 인식되는 오브젝트 변수
    GameObject rightScanObject; // rightRay 에 인식되는 오브젝트 변수

    bool waterLineActive = true; // 물줄기 위에 물풍선 설치 시, 안 터지게 만들기 위한 변수

    // 아이템이 여러 개 나오는 오류 방지
    bool isHitUpBlock = false; // 물풍선 UpRay 가 Block 을 인식했을 때, Block 부수기
    bool isHitDownBlock = false; // 물풍선 DownRay 가 Block 을 인식했을 때, Block 부수기
    bool isHitLeftBlock = false; // 물풍선 LeftRay 가 Block 을 인식했을 때, Block 부수기
    bool isHitRightBlock = false; // 물풍선 RightRay 가 Block 을 인식했을 때, Block 부수기

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();

        mainCol = MainCollider.GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        Invoke("WaterLineActive", 0.1f);
        Invoke("Boom", 2.5f);
        Invoke("Finish", 3f);

        upWater.SetActive(false);
        downWater.SetActive(false);
        leftWater.SetActive(false);
        rightWater.SetActive(false);
        balloonCollider.SetActive(false);
    }

    void Update()
    {
        BalloonRay();
    }



    void BalloonRay()
    {
        // Ray
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.6f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));

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
                upBlock.Invoke("Hit", 0.5f);
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
                downBlock.Invoke("Hit", 0.2f);
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
                leftBlock.Invoke("Hit", 0.2f);

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
                rightBlock.Invoke("Hit", 0.2f);

                isHitRightBlock = true;
            }

        }

        balloonCollider.SetActive(true);
    }


    void Finish()
    {
        // 애니메이션
        anim.SetBool("Boom", false);

        mainBalloon.SetActive(false);

        // 물줄기, Main Collider 비활성화
        upWater.SetActive(false);
        downWater.SetActive(false);
        leftWater.SetActive(false);
        rightWater.SetActive(false);
        balloonCollider.SetActive(false);

        waterLineActive = true;

        // 트리거 활성화 (collider = Player A 물풍선, mainCol = Player B 물풍선)
        collider.isTrigger = true;
        mainCol.isTrigger = true;
    }
    void WaterLineActive()
    {
        waterLineActive = false;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        // 다른 물풍선의 물줄기에 맞으면 바로 터지게 만듦
        if (obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater")
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
