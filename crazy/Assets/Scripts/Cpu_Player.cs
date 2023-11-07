using UnityEngine;

public class Cpu_Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;//다음 행동지표를 결정할 변수
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
        rigid.velocity = endPosition; //nextMove 에 0:멈춤 -1:왼쪽 1:오른쪽 으로 이동 
    }


    void Think()
    {//몬스터가 스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤  으로 3가지 행동을 판단)

        //Random.Range : 최소<= 난수 <최대 /범위의 랜덤 수를 생성(최대는 제외이므로 주의해야함)
        nextMove = Random.Range(-1, 2);

        //Think(); : 재귀함수 : 딜레이를 쓰지 않으면 CPU과부화 되므로 재귀함수쓸 때는 항상 주의 ->Think()를 직접 호출하는 대신 Invoke()사용
        Invoke("Think", 5); //매개변수로 받은 함수를 5초의 딜레이를 부여하여 재실행 
    }
}
