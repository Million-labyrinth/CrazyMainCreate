using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    public Animator anim;
    public ObjectManager objectManager;
    bool blockBreak = false;
    int testNum; // 중복 확인용 랜덤 숫자
    int ran; // 진짜 아이템 확률 숫자
    bool getRanNum;


    void Awake()
    {

        anim = GetComponent<Animator>();
        // 현재 활성화된 Scene에서 ObjectManager 클래스의 인스턴스를 찾아서 가져와서 초기화
        objectManager = FindObjectOfType<ObjectManager>();
    }

    void OnEnable()
    {
        blockBreak = false;
        getRanNum = false;
    }

    void Hit()
    {
        gameObject.SetActive(false);
        anim.SetBool("Hit", false); // 애니메이션 Idle 로 변경

        if (!blockBreak)
        {
            Invoke("SpawnItem", 0.2f);
            blockBreak = true;
        }
    }

    void SpawnItem()
    {
        // Item 드랍률 (중복제거)
        for (int i = 0; i < 100; i++)
        {
            testNum = UnityEngine.Random.Range(0, 100);

            if (!objectManager.rememberNumbers.Contains(testNum))
            {
                objectManager.rememberNumbers.Add(testNum);
                ran = testNum;
                break;
            }
        }

        if (ran < 12)
        {   // bubbleItem 12%
            GameObject bubbleItem = objectManager.MakeItem("BubbleItem");
            bubbleItem.transform.position = transform.position;

            MapItemMove ItemPos = bubbleItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 24)
        {   // flulidItem 12%
            GameObject flulidItem = objectManager.MakeItem("FluidItem");
            flulidItem.transform.position = transform.position;

            MapItemMove ItemPos = flulidItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 29)
        {   // rollerItem 5%
            GameObject rollerItem = objectManager.MakeItem("RollerItem");
            rollerItem.transform.position = transform.position;

            MapItemMove ItemPos = rollerItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 31)
        {   // shieldItem 2%
            GameObject shieldItem = objectManager.MakeItem("ShieldItem");
            shieldItem.transform.position = transform.position;

            MapItemMove ItemPos = shieldItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 33)
        {   // ultraFluidItem 2%
            GameObject ultraFluidItem = objectManager.MakeItem("UltraFluidItem");
            ultraFluidItem.transform.position = transform.position;

            MapItemMove ItemPos = ultraFluidItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 35)
        {   // niddleItem 2%
            GameObject niddleItem = objectManager.MakeItem("NiddleItem");
            niddleItem.transform.position = transform.position;

            MapItemMove ItemPos = niddleItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 38)
        {   // shoesItem 3%
            GameObject shoesItem = objectManager.MakeItem("ShoesItem");
            shoesItem.transform.position = transform.position;

            MapItemMove ItemPos = shoesItem.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 40)
        {   // redDevil 2%
            GameObject redDevil = objectManager.MakeItem("RedDevil");
            redDevil.transform.position = transform.position;

            MapItemMove ItemPos = redDevil.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;
        }
        else if (ran < 42)
        {   // purpleDevil 2%
            GameObject purpleDevil = objectManager.MakeItem("PurpleDevil");
            purpleDevil.transform.position = transform.position;

            MapItemMove ItemPos = purpleDevil.GetComponent<MapItemMove>();
            ItemPos.ObjPos = transform.position; ;

        }
        else if (ran < 100)
        { // Not Item 58%
            Debug.Log("Not Item");
        }
    }

    // 이제 필요 없음
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater")
        {
            anim.SetBool("Hit", true);
            Invoke("Hit", 0.55f);
        }
    }


}
