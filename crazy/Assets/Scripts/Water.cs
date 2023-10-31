using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Balloon ballon;

    public GameObject[] upWater;
    public GameObject[] downWater;
    public GameObject[] leftWater;
    public GameObject[] rightWater;

    GameObject scanObject; // upRay 에 인식되는 오브젝트 변수
    SpriteRenderer sprite;

    bool isHitBlock = false; // 물줄기 Ray 가 Block 을 인식했을 때, Block 부수기

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Active();
        sprite.enabled = true; // Sprite Renderer 가 꺼지는 오류 발생해서 추가한 코드
    }

    void Update()
    {
        // 마지막 물줄기를 제외하고 Ray 적용
        switch (gameObject.tag)
        {
            case "upWater":
                if (Array.IndexOf(upWater, gameObject) != upWater.Length - 1)
                {
                    Ray(Vector3.up);
                }
                break;
            case "downWater":
                if (Array.IndexOf(downWater, gameObject) != downWater.Length - 1)
                {
                    Ray(Vector3.down);
                }
                break;
            case "leftWater":
                if (Array.IndexOf(leftWater, gameObject) != leftWater.Length - 1)
                {
                    Ray(Vector3.left);
                }
                break;
            case "rightWater":
                if (Array.IndexOf(rightWater, gameObject) != rightWater.Length - 1)
                {
                    Ray(Vector3.right);
                }
                break;
        }
    }

    void Ray(Vector3 waterVec)
    {
        // Ray
        Debug.DrawRay(transform.position, waterVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, waterVec, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
            FindObject();

            if (!isHitBlock && scanObject.tag == "Block")
            {
                Block Block = scanObject.GetComponent<Block>();
                if (Block != null)
                {
                    Block.anim.SetBool("Hit", true);
                    Block.Invoke("Hit", 0.5f);
                    isHitBlock = true;
                }
            }
            else if (scanObject.tag == "grass")
            {
                grass grassLogic = scanObject.GetComponent<grass>();
                Debug.Log(scanObject.name);
                if (grassLogic != null && !grassLogic.haveObj)
                {
                    scanObject.SetActive(false);
                }
            }
        }
        else
        {
            scanObject = null;
            isHitBlock = false;
        }
    }

    // 물줄기 순서대로 활성화
    void Active()
    {
        switch (gameObject.tag)
        {
            case "upWater":
                for (int i = 0; i < upWater.Length; i++)
                {
                    upWater[i].SetActive(true);
                }

                Invoke("BackCondition", 0.5f);
                break;
            case "downWater":
                for (int i = 0; i < downWater.Length; i++)
                {
                    downWater[i].SetActive(true);
                }
                Invoke("BackCondition", 0.5f);
                break;
            case "leftWater":
                for (int i = 0; i < leftWater.Length; i++)
                {
                    leftWater[i].SetActive(true);
                }
                Invoke("BackCondition", 0.5f);
                break;
            case "rightWater":
                for (int i = 0; i < rightWater.Length; i++)
                {
                    rightWater[i].SetActive(true);
                }
                Invoke("BackCondition", 0.5f);
                break;
        }

    }

    void FindObject()
    {
        switch (gameObject.tag)
        {
            case "upWater":
                for (int i = Array.IndexOf(upWater, gameObject) + 1; i < upWater.Length; i++)
                {
                    upWater[i].SetActive(false);
                }
                break;
            case "downWater":
                for (int i = Array.IndexOf(downWater, gameObject) + 1; i < downWater.Length; i++)
                {
                    downWater[i].SetActive(false);
                }
                break;
            case "leftWater":
                for (int i = Array.IndexOf(leftWater, gameObject) + 1; i < leftWater.Length; i++)
                {
                    leftWater[i].SetActive(false);
                }
                break;
            case "rightWater":
                for (int i = Array.IndexOf(rightWater, gameObject) + 1; i < rightWater.Length; i++)
                {
                    rightWater[i].SetActive(false);
                }
                break;
        }
    }

    // 0.5초가 지나면 물줄기 전부 다 끄기 (오류 방지)
    void BackCondition()
    {
        switch (gameObject.tag)
        {
            case "upWater":
                for (int i = Array.IndexOf(upWater, gameObject); i < upWater.Length; i++)
                {
                    upWater[i].SetActive(false);
                }
                break;
            case "downWater":
                for (int i = Array.IndexOf(downWater, gameObject); i < downWater.Length; i++)
                {
                    downWater[i].SetActive(false);
                }
                break;
            case "leftWater":
                for (int i = Array.IndexOf(leftWater, gameObject); i < leftWater.Length; i++)
                {
                    leftWater[i].SetActive(false);
                }
                break;
            case "rightWater":
                for (int i = Array.IndexOf(rightWater, gameObject); i < rightWater.Length; i++)
                {
                    rightWater[i].SetActive(false);
                }
                break;
        }
    }
}
