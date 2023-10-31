using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Animator anim;
    public ObjectManager objectManager;
    public bool blockBreak = false;
   

    void Awake() {
        
        anim = GetComponent<Animator>();
        // 현재 활성화된 Scene에서 ObjectManager 클래스의 인스턴스를 찾아서 가져와서 초기화
        objectManager = FindObjectOfType<ObjectManager>();
    }

    void Hit() {
        gameObject.SetActive(false);
        anim.SetBool("Hit", false);

        Invoke("SpawnItem", 0.3f);
    }

    void SpawnItem()
    {
        // Item 드랍률
        int ran = Random.Range(0, 100);
        if (ran < 50)
        { // Not Item 40%
            Debug.Log("Not Item");
        }
        else if (ran < 65)
        {   // bubbleItem 15%
            GameObject bubbleItem = objectManager.MakeItem("BubbleItem");
            bubbleItem.transform.position = transform.position;
        }
        else if (ran < 75)
        {   // flulidItem 10%
            GameObject flulidItem = objectManager.MakeItem("FluidItem");
            flulidItem.transform.position = transform.position;
        }
        else if (ran < 85)
        {   // rollerItem 5%
            GameObject rollerItem = objectManager.MakeItem("RollerItem");
            rollerItem.transform.position = transform.position;
        }
        else if (ran < 90)
        {   // shieldItem 5%
            GameObject shieldItem = objectManager.MakeItem("ShieldItem");
            shieldItem.transform.position = transform.position;
        }
        else if (ran < 95)
        {   // niddleItem 5%
            GameObject niddleItem = objectManager.MakeItem("NiddleItem");
            niddleItem.transform.position = transform.position;
        }
        else if (ran < 100)
        {   // ultraFluidItem 5%
            GameObject ultraFluidItem = objectManager.MakeItem("UltraFluidItem");
            ultraFluidItem.transform.position = transform.position;
        }
    }

    // 이제 필요 없음
    void OnTriggerEnter2D(Collider2D  obj) {
        if(obj.gameObject.tag == "upWater" || obj.gameObject.tag == "downWater" || obj.gameObject.tag == "leftWater" || obj.gameObject.tag == "rightWater") {
            anim.SetBool("Hit", true);
            Invoke("Hit", 0.5f);
        }
    }


}
