using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // ��ǳ�� ������
    public GameObject waterBalloon1Prefab;
    public GameObject waterBalloon2Prefab;
    public GameObject waterBalloon3Prefab;
    public GameObject waterBalloon4Prefab;
    public GameObject waterBalloon5Prefab;
    public GameObject waterBalloon6Prefab;
    public GameObject waterBalloon7Prefab;

    // ������ ������
    public GameObject bubbleItemPrefab;
    public GameObject fluidItemPrefab;
    public GameObject rollerItemPrefab;
    public GameObject niddleItemPrefab;
    public GameObject shieldItemPrefab;
    public GameObject ultraFluidItemPrefab;
    public GameObject shoesItemPrefab;
    public GameObject redDevilPrefab;
    public GameObject purpleDevilPrefab;

    // ��ǳ�� �迭
    GameObject[] waterBalloon1;
    GameObject[] waterBalloon2;
    GameObject[] waterBalloon3;
    GameObject[] waterBalloon4;
    GameObject[] waterBalloon5;
    GameObject[] waterBalloon6;
    GameObject[] waterBalloon7;


    // ������ �迭
    GameObject[] bubbleItem;
    GameObject[] fluidItem;
    GameObject[] rollerItem;
    GameObject[] niddleItem;
    GameObject[] shieldItem;
    GameObject[] ultraFluidItem;
    GameObject[] shoesItem;
    GameObject[] redDevil;
    GameObject[] purpleDevil;

    GameObject[] targetPool; // switch ���� ���� ������ ������Ʈ �迭�� �뵵

    void Awake()
    {

        // ù �ε� �ð� = ��� ��ġ + ������Ʈ Ǯ ����

        // �ѹ��� ������ ������ ����Ͽ� �迭 ���� �Ҵ�
        waterBalloon1 = new GameObject[20];
        waterBalloon2 = new GameObject[20];
        waterBalloon3 = new GameObject[20];
        waterBalloon4 = new GameObject[20];
        waterBalloon5 = new GameObject[20];
        waterBalloon6 = new GameObject[20];
        waterBalloon7 = new GameObject[20];

        bubbleItem = new GameObject[15];
        fluidItem = new GameObject[10];
        rollerItem = new GameObject[10];
        niddleItem = new GameObject[5];
        shieldItem = new GameObject[5];
        ultraFluidItem = new GameObject[5];
        shoesItem = new GameObject[5];
        redDevil = new GameObject[5];
        purpleDevil = new GameObject[5];

        Generate();

    }

    // Instantiate() �� ������ �������� �ν��Ͻ��� �迭�� ����
    void Generate()
    {
        // WaterBalloon
        for (int index = 0; index < waterBalloon1.Length; index++)
        {
            waterBalloon1[index] = Instantiate(waterBalloon1Prefab);
            waterBalloon1[index].SetActive(false);
        
        }
        for (int index = 0; index < waterBalloon2.Length; index++)
        {
            waterBalloon2[index] = Instantiate(waterBalloon2Prefab);
            waterBalloon2[index].SetActive(false);
        }
        for (int index = 0; index < waterBalloon3.Length; index++)
        {
            waterBalloon3[index] = Instantiate(waterBalloon3Prefab);
            waterBalloon3[index].SetActive(false);
        }
        for (int index = 0; index < waterBalloon4.Length; index++)
        {
            waterBalloon4[index] = Instantiate(waterBalloon4Prefab);
            waterBalloon4[index].SetActive(false);
        }
        for (int index = 0; index < waterBalloon5.Length; index++)
        {
            waterBalloon5[index] = Instantiate(waterBalloon5Prefab);
            waterBalloon5[index].SetActive(false);
        }
        for (int index = 0; index < waterBalloon6.Length; index++)
        {
            waterBalloon6[index] = Instantiate(waterBalloon6Prefab);
            waterBalloon6[index].SetActive(false);
        }
        for (int index = 0; index < waterBalloon7.Length; index++)
        {
            waterBalloon7[index] = Instantiate(waterBalloon7Prefab);
            waterBalloon7[index].SetActive(false);
        }

        // Item
        for (int index = 0; index < bubbleItem.Length; index++)
        {
            bubbleItem[index] = Instantiate(bubbleItemPrefab);
            bubbleItem[index].SetActive(false);
        }
        for (int index = 0; index < fluidItem.Length; index++)
        {
            fluidItem[index] = Instantiate(fluidItemPrefab);
            fluidItem[index].SetActive(false);
        }
        for (int index = 0; index < rollerItem.Length; index++)
        {
            rollerItem[index] = Instantiate(rollerItemPrefab);
            rollerItem[index].SetActive(false);
        }
        for (int index = 0; index < shieldItem.Length; index++)
        {
            shieldItem[index] = Instantiate(shieldItemPrefab);
            shieldItem[index].SetActive(false);
        }
        for (int index = 0; index < niddleItem.Length; index++)
        {
            niddleItem[index] = Instantiate(niddleItemPrefab);
            niddleItem[index].SetActive(false);
        }
        for (int index = 0; index < ultraFluidItem.Length; index++)
        {
            ultraFluidItem[index] = Instantiate(ultraFluidItemPrefab);
            ultraFluidItem[index].SetActive(false);
        }
        for (int index = 0; index < shoesItem.Length; index++)
        {
            shoesItem[index] = Instantiate(shoesItemPrefab);
            shoesItem[index].SetActive(false);
        }
        for (int index = 0; index < redDevil.Length; index++)
        {
            redDevil[index] = Instantiate(redDevilPrefab);
            redDevil[index].SetActive(false);
        }
        for (int index = 0; index < purpleDevil.Length; index++)
        {
            purpleDevil[index] = Instantiate(purpleDevilPrefab);
            purpleDevil[index].SetActive(false);
        }
    }

    public GameObject MakeItem(string type)
    {
        switch (type)
        {
            case "BubbleItem":
                targetPool = bubbleItem;
                break;
            case "FluidItem":
                targetPool = fluidItem;
                break;
            case "RollerItem":
                targetPool = rollerItem;
                break;
            case "ShieldItem":
                targetPool = shieldItem;
                break;
            case "NiddleItem":
                targetPool = niddleItem;
                break;
            case "UltraFluidItem":
                targetPool = ultraFluidItem;
                break;
            case "ShoesItem":
                targetPool = shoesItem;
                break;
            case "RedDevil":
                targetPool = redDevil;
                break;
            case "PurpleDevil":
                targetPool = purpleDevil;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            // ��Ȱ��ȭ �� ������Ʈ�� �����Ͽ� Ȱ��ȭ ��, ��ȯ
            if (!targetPool[index].activeSelf)
            { // activeSelf : ������Ʈ Ȱ��ȭ ����
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        // Ǯ(Pool)�� �� �̻� Ȱ��ȭ���� ���� ������Ʈ�� �������� ���� �� null ���� return ����
        // ������Ʈ�� ��� ��� ���� ��쿡�� targetPool[index].SetActive(true);�� �������� �ʰ� ��
        return null;
    }

    // ������ ������Ʈ Ǯ�� �������� �Լ� �߰�
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "WaterBalloon1":
                targetPool = waterBalloon1;
                break;
            case "WaterBalloon2":
                targetPool = waterBalloon2;
                break;
            case "WaterBalloon3":
                targetPool = waterBalloon3;
                break;
            case "WaterBalloon4":
                targetPool = waterBalloon4;
                break;
            case "WaterBalloon5":
                targetPool = waterBalloon5;
                break;
            case "WaterBalloon6":
                targetPool = waterBalloon6;
                break;
            case "WaterBalloon7":
                targetPool = waterBalloon7;
                break;
            case "BubbleItem":
                targetPool = bubbleItem;
                break;
            case "FluidItem":
                targetPool = fluidItem;
                break;
            case "RollerItem":
                targetPool = rollerItem;
                break;
            case "ShieldItem":
                targetPool = shieldItem;
                break;
            case "NiddleItem":
                targetPool = niddleItem;
                break;
            case "UltraFluid":
                targetPool = ultraFluidItem;
                break;
            case "ShoesItem":
                targetPool = ultraFluidItem;
                break;
            case "RedDevil":
                targetPool = ultraFluidItem;
                break;
            case "PurpleDevil":
                targetPool = ultraFluidItem;
                break;
        }

        return targetPool;
    }
}
