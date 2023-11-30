using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Water : MonoBehaviour
{

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
        Ray();
        if (scanObject == null && isActivation)
        {
            isActivation = false;
        }
    }

    void Ray()
    {
        // Ray
        Collider2D rayHit = Physics2D.OverlapCircle(transform.position, 0.45f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Grass"));


        if (rayHit != null)
        {
            scanObject = rayHit.gameObject;
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f);
        // playerARay ���
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }


    IEnumerator hitBlock()
    {
        yield return new WaitForSeconds(0.5f);
        isHitBlock = false;

        StopCoroutine("hitBlock");
    }
}