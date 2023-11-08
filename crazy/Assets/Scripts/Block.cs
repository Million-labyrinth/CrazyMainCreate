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
   
    void Awake() {
        
        anim = GetComponent<Animator>();
        // 현재 활성화된 Scene에서 ObjectManager 클래스의 인스턴스를 찾아서 가져와서 초기화
        objectManager = FindObjectOfType<ObjectManager>();
    }

    void OnEnable()
    {
        blockBreak = false;
    }

    void Hit() {
        gameObject.SetActive(false);
        anim.SetBool("Hit", false);

        if(!blockBreak)
        {
            Invoke("SpawnItem", 0.3f);
            blockBreak = true;
        }
    }

    void SpawnItem()
    {
        // Item 드랍률
        int ran = UnityEngine.Random.Range(0, 100);
        if (ran < 50)
        { // Not Item 60%
            Debug.Log("Not Item");
        }
        else if (ran < 70)
        {   // bubbleItem 20%
            GameObject bubbleItem = objectManager.MakeItem("BubbleItem");
            bubbleItem.transform.position = transform.position;

            VerticalMovement movement = bubbleItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 85)
        {   // flulidItem 15%
            GameObject flulidItem = objectManager.MakeItem("FluidItem");
            flulidItem.transform.position = transform.position;

            VerticalMovement movement = flulidItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 90)
        {   // rollerItem 5%
            GameObject rollerItem = objectManager.MakeItem("RollerItem");
            rollerItem.transform.position = transform.position;

            VerticalMovement movement = rollerItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 91)
        {   // shieldItem 1%
            GameObject shieldItem = objectManager.MakeItem("ShieldItem");
            shieldItem.transform.position = transform.position;

            VerticalMovement movement = shieldItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 94)
        {   // ultraFluidItem 3%
            GameObject ultraFluidItem = objectManager.MakeItem("UltraFluidItem");
            ultraFluidItem.transform.position = transform.position;

            VerticalMovement movement = ultraFluidItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 95)
        {   // niddleItem 1%
            GameObject niddleItem = objectManager.MakeItem("NiddleItem");
            niddleItem.transform.position = transform.position;

            VerticalMovement movement = niddleItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 98)
        {   // shoesItem 3%
            GameObject shoesItem = objectManager.MakeItem("ShoesItem");
            shoesItem.transform.position = transform.position;

            VerticalMovement movement = shoesItem.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        else if (ran < 100)
        {   // redDevil 2%
            GameObject redDevil = objectManager.MakeItem("RedDevil");
            redDevil.transform.position = transform.position;

            VerticalMovement movement = redDevil.GetComponent<VerticalMovement>();
            movement.minY = transform.position.y;
            movement.maxY = transform.position.y + 0.1f;
        }
        //else if (ran < 100)
        //{   
        //    GameObject purpleDevil = objectManager.MakeItem("PurpleDevil");
        //    purpleDevil.transform.position = transform.position;

        //    VerticalMovement movement = purpleDevil.GetComponent<VerticalMovement>();
        //    movement.minY = transform.position.y;
        //    movement.maxY = transform.position.y + 0.1f;
        //}


    }

    // 이제 필요 없음
    void OnTriggerEnter2D(Collider2D  obj) {
        if(obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater") {
            anim.SetBool("Hit", true);
            Invoke("Hit", 0.5f);
        }
    }


}
