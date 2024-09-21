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
    public float RotationSpeed = 200f;
    public int RoadType = 0;
    //Checkpoints
    public int TargetPointIndex = 0;
    private float HoriOffset = 0f;
    //Curve turn system
    private float estTime = 0;
    public float curveCtrlPointOffset = 1f;
    public float CurveDuration = 3f;
    public bool DirectionFlag = false;
    public float DefaultCurvature = 0.5f;
    public GameObject DebugCc;

    public int debugI;
    

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
        Transform lastTP = TargetPathList[TargetPointIndex - 1];
        float crsPro = 0;

        if (DirectionFlag == false)
        {
            Vector2 fw = transform.right;
            Vector2 tTg = targetPoint.position - lastTP.position;
            crsPro = fw.x * tTg.y - fw.y * tTg.x;
            DirectionFlag = true;
        }

        Debug.Log(crsPro);

        //Rotate Angle
        Vector2 direction = (targetPoint.position - transform.position).normalized;

        //If it's a straight line
        if (-0.1f <= crsPro && crsPro <= 0.1f && RoadType != 2)
        {
            //Debug.Log("Straight" + (TargetPointIndex - 1) + TargetPointIndex);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, BasicMoveSpeed * Time.deltaTime);
        }
        
        else if (estTime <= 1 && RoadType != 1)
        {

            estTime += Time.deltaTime * DefaultCurvature;

            Vector3 position;

            //Debug.Log("TimeDebug"+estTime);
            

            if (crsPro > 0.1f)
            {
                position = BezierPoint(estTime, targetPoint.position, CtrlPointCalc(lastTP.position, targetPoint.position, curveCtrlPointOffset, false), targetPoint.position);
                //DebugCc.transform.position = CtrlPointCalc(lastTP.position, targetPoint.position, curveCtrlPointOffset, false);
            }

            else
            {
                position = BezierPoint(estTime, targetPoint.position, CtrlPointCalc(lastTP.position, targetPoint.position, curveCtrlPointOffset, true), targetPoint.position);
                //DebugCc.transform.position = CtrlPointCalc(lastTP.position, targetPoint.position, curveCtrlPointOffset, true);
            }
            DebugCc.transform.position = position;
            transform.position = new Vector3(position.x, position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position,new Vector3(position.x, position.y, transform.position.z),BasicMoveSpeed * Time.deltaTime);
            RoadType = 2;
        }

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            TargetPointIndex++;
            estTime = 0;
            if (TargetPointIndex >= TargetPathList.Length)
            {
                TargetPointIndex = -1; 
            }
            DirectionFlag = false;
            RoadType = 0;
        }

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
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
        Vector2 direction = (end - start).normalized;
        Vector2 normal = new Vector2(-direction.y, direction.x);
        Vector2 midPoint = (start + end) / 2;
        if (ifRight)
        {
            return midPoint + normal * + DefaultCurvature;
        }
        else
        {
            return midPoint + normal * - DefaultCurvature;
        }
        /*
        Vector3 midPoint = (start + end) / 2;
        if (ifRight)
        {
            return new Vector3(midPoint.x, midPoint.y + hOffset);
        }
        else
        {
            return new Vector3(midPoint.x - hOffset, midPoint.y);
        }*/
    }

    Vector2 BezierPoint(float EstT, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - EstT;
        return u * u * p0 + 2 * u * EstT * p1 + EstT * EstT * p2;
    }
}
