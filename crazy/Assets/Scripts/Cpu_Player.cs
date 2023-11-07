using UnityEngine;

public class Cpu_Player : MonoBehaviour
{

    public GameObject upScanObject; // upRay 에 인식되는 오브젝트 변수
    public GameObject downScanObject; // downRay 에 인식되는 오브젝트 변수
    public GameObject leftScanObject; // leftRay 에 인식되는 오브젝트 변수
    public GameObject rightScanObject;
    void BoxRay()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.45f, 0), Vector3.up * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0, -0.45f, 0), Vector3.down * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left * 0.6f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + new Vector3(0.45f, 0, 0), Vector3.right * 0.6f, new Color(0, 1, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.45f, 0), Vector3.up, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B") | LayerMask.GetMask("Water"));
        RaycastHit2D downRayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.45f, 0), Vector3.down, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B") | LayerMask.GetMask("Water"));
        RaycastHit2D leftRayHit = Physics2D.Raycast(transform.position + new Vector3(-0.45f, 0, 0), Vector3.left, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B") | LayerMask.GetMask("Water"));
        RaycastHit2D rightRayHit = Physics2D.Raycast(transform.position + new Vector3(0.45f, 0, 0), Vector3.right, 0.6f, LayerMask.GetMask("Block") | LayerMask.GetMask("MoveBlock") | LayerMask.GetMask("Object") | LayerMask.GetMask("Player A") | LayerMask.GetMask("Player B") | LayerMask.GetMask("Water"));

        if (upRayHit.collider != null)
        {
            upScanObject = upRayHit.collider.gameObject;
        }
        else
        {
            upScanObject = null;
        }
        if (downRayHit.collider != null)
        {
            downScanObject = downRayHit.collider.gameObject;
        }
        else
        {
            downScanObject = null;
        }
        if (leftRayHit.collider != null)
        {
            leftScanObject = leftRayHit.collider.gameObject;
        }
        else
        {
            leftScanObject = null;
        }
        if (rightRayHit.collider != null)
        {
            rightScanObject = rightRayHit.collider.gameObject;
        }
        else
        {
            rightScanObject = null;
        }
    }

    void cpu_Move()
    {

    }

}
