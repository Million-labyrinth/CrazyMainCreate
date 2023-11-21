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

    GameObject scanObject; // upRay �� �νĵǴ� ������Ʈ ����
    SpriteRenderer sprite;

    bool isHitBlock; // ���ٱ� Ray �� Block �� �ν����� ��, Block �μ���
    bool isActivation; // Ray �� �ƹ��͵� �ν� �ȵǸ� ���ٱ� Ȱ��ȭ


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        isHitBlock = false;
        isActivation = false;
    }

    void OnEnable()
    {
        sprite.enabled = true; // Sprite Renderer �� ������ ���� �߻��ؼ� �߰��� �ڵ�

        isHitBlock = false;
    }

    void Update()
    {

        // ������ ���ٱ⸦ �����ϰ� Ray ����
        switch (gameObject.tag)
        {
            case "upWater":
                if (Array.IndexOf(upWater, gameObject) != upWater.Length - 1)
                {
                    Vector3 plus = new Vector3(0, 0.3f, 0);
                    Ray(plus, Vector3.up);

                    if (scanObject == null && isActivation)
                    {
                        isActivation = false;
                        upWater[Array.IndexOf(upWater, gameObject) + 1].SetActive(true);
                        Invoke("BackCondition", 0.55f);
                    }
                }
                break;
            case "downWater":
                if (Array.IndexOf(downWater, gameObject) != downWater.Length - 1)
                {
                    Vector3 plus = new Vector3(0, -0.3f, 0);
                    Ray(plus, Vector3.down);

                    if (scanObject == null)
                    {
                        downWater[Array.IndexOf(downWater, gameObject) + 1].SetActive(true);
                        Invoke("BackCondition", 0.55f);
                    }
                }
                break;
            case "leftWater":
                if (Array.IndexOf(leftWater, gameObject) != leftWater.Length - 1)
                {
                    Vector3 plus = new Vector3(-0.3f, 0, 0);
                    Ray(plus, Vector3.left);

                    if (scanObject == null)
                    {
                        leftWater[Array.IndexOf(leftWater, gameObject) + 1].SetActive(true);
                        Invoke("BackCondition", 0.55f);
                    }
                }
                break;
            case "rightWater":
                if (Array.IndexOf(rightWater, gameObject) != rightWater.Length - 1)
                {
                    Vector3 plus = new Vector3(0.3f,0 , 0);
                    Ray(plus, Vector3.right);

                    if(scanObject == null)
                    {
                        rightWater[Array.IndexOf(rightWater, gameObject) + 1].SetActive(true);
                        Invoke("BackCondition", 0.55f);
                    }
                }
                break;
        }
    }

    void Ray(Vector3 plusVec, Vector3 waterVec)
    {
        // Ray
        Debug.DrawRay(transform.position + plusVec, waterVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position + plusVec, waterVec, 0.7f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
            isActivation = false;

            if (!isHitBlock && scanObject.tag == "Block")
            {
                isHitBlock = true;

                Block Block = scanObject.GetComponent<Block>();
                    
                Block.anim.SetBool("Hit", true);
                Block.Invoke("Hit", 0.55f);

                StartCoroutine("hitBlock");
            }
            else if (scanObject.tag == "grass")
            {
                grass grassLogic = scanObject.GetComponent<grass>();

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
            isActivation = true;
        }
    }

    // 0.5�ʰ� ������ ���ٱ� ���� �� ���� (���� ����)
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

    IEnumerator hitBlock()
    {
        yield return new WaitForSeconds(0.5f);
        isHitBlock = false;

        StopCoroutine("hitBlock");
    }
}
