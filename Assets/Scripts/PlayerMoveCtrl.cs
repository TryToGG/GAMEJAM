using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl : MonoBehaviour
{
    //Basic moving data
    public Transform[] TargetPathList;
    public float BasicMoveSpeed = 1f;
    public float RandomHorizontalRange = 1f;
    public float HorizontalMoveSpeed = 1f;
    //Checkpoints
    public int TargetPointIndex = 0;
    private float HoriOffset = 0f;
    //Curve turn system
    private float estTime = 0;
    public float curveCtrlPointOffset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PathAlong();
        RandomHorizontal();
    }

    //Find the next checkpoint in the list and try to reach it (When having a curve, it will go round - WIP)
    public void PathAlong()
    {
        if (TargetPointIndex == -1 || TargetPathList.Length == 0)
            return;
        Transform targetPoint = TargetPathList[TargetPointIndex];
        Transform nxtTP = targetPoint;
        if (TargetPointIndex + 1 <= TargetPathList.Length)
        {
            nxtTP = TargetPathList[TargetPointIndex + 1];
        }
        //If there is a display error, check here first!!!              vvvvvvv
        Vector3 targetPosition = targetPoint.position + new Vector3(HoriOffset,0,0);
        //If it's a straight line
        if (targetPoint.transform.position.x == nxtTP.transform.position.x || targetPoint.transform.position.y == nxtTP.transform.position.y)
        {
            estTime = 0;
        }
        else if (estTime < 1)
        {
            Vector3 position;
            estTime += Time.deltaTime * BasicMoveSpeed;
            if (((nxtTP.position) - transform.position).x < 0)
            {
                position = BezierPoint(estTime, targetPoint.position, CtrlPointCalc(targetPoint.position, nxtTP.position, curveCtrlPointOffset,true), nxtTP.position);
            }
            else
            {
                position = BezierPoint(estTime, targetPoint.position, CtrlPointCalc(targetPoint.position, nxtTP.position, curveCtrlPointOffset,false), nxtTP.position);
            }
            transform.position = new Vector3(position.x, position.y, transform.position.z);

        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            TargetPointIndex++;
            if (TargetPointIndex >= TargetPathList.Length)
            {
                TargetPointIndex = -1; 
            }
        }
    }

    void RandomHorizontal()
    {
        if (Input.GetKey(KeyCode.A))
        {
            HoriOffset += HorizontalMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            HoriOffset -= HorizontalMoveSpeed * Time.deltaTime;
        }
    }

    Vector3 CtrlPointCalc(Vector3 start, Vector3 end, float hOffset, bool ifRight)
    {
        Vector3 midPoint = (start + end) / 2;
        if (ifRight)
        {
            return new Vector3(midPoint.x, midPoint.y + hOffset);
        }
        else
        {
            return new Vector3(midPoint.x - hOffset, midPoint.y);
        }
    }

    Vector2 BezierPoint(float EstT, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - EstT;
        float tt = EstT * EstT;
        float uu = u * u;
        Vector3 point = uu * p0;
        point += 2 * u * EstT * p1;
        point += tt * p2;
        return point;
    }
}
