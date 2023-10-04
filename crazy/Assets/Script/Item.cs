using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//김인섭 왔다감1
public class Item : MonoBehaviour
{
    public Player player;

    //���۸ǿ� ������ �ɷ�ġ
    private int previousBombPower;
    private int previousBombRange;
    private float previousPlayerSpeed;
    private bool isSupermanActive = false;

    public GameObject[] speedItem;
    public GameObject[] bombItem;
    public GameObject[] rangeItem;
    public GameObject[] Activeitem;

    CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    


    //�������� �����صа�
    //�Ŀ�
    public void PowerAdd(string name)
    {
        if(name.Contains("basicBubble"))
        {
            player.bombPower++;
            Debug.Log("�⺻ǳ�� : ���� ����");
        }
        
    }
    //���ǵ�
    public void SpeedAdd(string name)
    {
            if (name.Contains("roller"))
            {
                Debug.Log("�ѷ� : ���ǵ� ����");
                player.playerSpeed++ ;
               
            } 
            else if(name.Contains("redDevil"))
            {
                Debug.Log("�����Ǹ� : �̼� �ִ�ġ");
                player.playerSpeed = player.playerSpeedMax;
            }
        


    }
    //��Ÿ�
    public void RangeAdd(string name)
    {
        if(name.Contains("basicFluid"))
        {
            player.bombRange++;
            Debug.Log("�⺻ ���ٱ� : ��Ÿ� ����");
        }
        else if(name.Contains("ultraFluid"))
        {
            player.bombRange = player.bombRangeMax;
            Debug.Log("��Ʈ�� ���ٱ� : ��Ÿ� �ִ�ġ");
        }
    }

    //�ɷ�ġ �ҷ�����
    public void LoadState()
    {
        player.bombPower = previousBombPower;
        player.bombRange = previousBombRange;
        player.playerSpeed = previousPlayerSpeed; 
    }


    //���۸�
    public void SuperMan(string name)
    {
        if (name.Contains("superMan") && !isSupermanActive)
        {
            // ���۸� �������� �̹� Ȱ��ȭ ������ ǥ��
            isSupermanActive = true;

            // ���� �ɷ�ġ ����
            previousBombPower = player.bombPower;
            previousBombRange = player.bombRange;
            previousPlayerSpeed = player.playerSpeed;

            // ���ϴ� �ɷ�ġ�� ����
            player.bombPower = player.bombPowerMax;
            player.bombRange = player.bombRangeMax;
            player.playerSpeed = player.playerSpeedMax;

            // 5�� ���� ��� �� ����
            StartCoroutine(RestoreAbilitiesAfterDelay(5f));
        }
    }


    // �������� Activeitem �迭�� �߰��ϴ� �Լ� (������ ȹ�� �� ȣ��)
    public void AddActiveItem(GameObject item, int index)
    {
        if (index >= 0 && index < Activeitem.Length)
        {
            Activeitem[index] = item;
        }
    }

    //��Ƽ�� ������ ��� �ڵ�
    public void ActiveUseItem(string name)
    {
        if (Activeitem[0].name.Contains("niddle"))
        {
            Debug.Log("�÷��̾ �ٴ� �������� ����� ȸ��");
            player.playerHealth = 0f;
        }
        else if (Activeitem[0].name.Contains("shield"))
        {
            Debug.Log("�÷��̾ ���� �������� ���");
            
        }


        // ������ ��� ��, ����� �������� Activeitem �迭���� ����
        for (int i = 0; i < Activeitem.Length; i++)
        {
            if (Activeitem[i] != null && Activeitem[i].name == name)
            {
                Activeitem[i] = null;
                break;
            }
        }
    }


    private IEnumerator RestoreAbilitiesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 5�� �Ŀ� ����� �ɷ�ġ�� ����
        player.bombPower = previousBombPower;
        player.bombRange = previousBombRange;
        player.playerSpeed = previousPlayerSpeed;

        // ���۸� ������ ��Ȱ��ȭ
        isSupermanActive = false;
    }
}
