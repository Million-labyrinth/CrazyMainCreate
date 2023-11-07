using UnityEngine;

public class Cpu_Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;//���� �ൿ��ǥ�� ������ ����
    Vector3 endPosition;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = endPosition; //nextMove �� 0:���� -1:���� 1:������ ���� �̵� 
    }


    void Think()
    {//���Ͱ� ������ �����ؼ� �Ǵ� (-1:�����̵� ,1:������ �̵� ,0:����  ���� 3���� �ൿ�� �Ǵ�)

        //Random.Range : �ּ�<= ���� <�ִ� /������ ���� ���� ����(�ִ�� �����̹Ƿ� �����ؾ���)
        nextMove = Random.Range(-1, 2);

        //Think(); : ����Լ� : �����̸� ���� ������ CPU����ȭ �ǹǷ� ����Լ��� ���� �׻� ���� ->Think()�� ���� ȣ���ϴ� ��� Invoke()���
        Invoke("Think", 5); //�Ű������� ���� �Լ��� 5���� �����̸� �ο��Ͽ� ����� 
    }
}
