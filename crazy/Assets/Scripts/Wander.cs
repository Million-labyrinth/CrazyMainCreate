using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    public float currentSpeed;

    public float directionChangeInterval;
    public bool followPlayer;

    Coroutine moveCoroutine;
    CircleCollider2D CircleCollider2D;
    Rigidbody2D rb2d;
    Animator anim;

    Transform targetTransform = null;
    Vector3 endPosition;
    float currenAngle = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        CircleCollider2D = GetComponent<CircleCollider2D>();
        currentSpeed = wanderSpeed;
        StartCoroutine(WanderRoutine());
    }
    void Update()
    {
        Debug.DrawLine(rb2d.position, endPosition, Color.red);
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    
    void ChooseNewEndPoint()
    {
        currenAngle += Random.Range(0, 360);
        currenAngle = Mathf.Repeat(currenAngle, 360);
        endPosition += Vector3FromAngle(currenAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }
    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidBodyToMove != null)
            {
                Vector2 currentPosition = rigidBodyToMove.position;
                Vector2 newPosition;

                if (Mathf.Abs(endPosition.x - currentPosition.x) > Mathf.Abs(endPosition.y - currentPosition.y))
                {
                    // 더 가까운 축을 기준으로 수평 이동
                    newPosition = new Vector2(endPosition.x, currentPosition.y);
                }
                else
                {
                    // 더 가까운 축을 기준으로 수직 이동
                    newPosition = new Vector2(currentPosition.x, endPosition.y);
                }

                newPosition = Vector2.MoveTowards(currentPosition, newPosition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerA") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerA"))
        {
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }
    void OnDrawGizmos()
    {
        if (CircleCollider2D != null)
        {
            Gizmos.DrawWireSphere(transform.position, CircleCollider2D.radius);
        }
    }



}
