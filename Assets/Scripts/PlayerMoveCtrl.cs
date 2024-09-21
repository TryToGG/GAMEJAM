using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl : MonoBehaviour
{
    public Transform[] TargetPathList;
    public float BasicMoveSpeed = 1f;
    public float MoveSpeedMultiplier = 1f;
    public float RandomHorizontalRange = 1f;
    public float HorizontalMoveSpeed = 1f;

    public int TargetPointIndex = 0;
    private float HoriOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PathAlong()
    {
        if (TargetPointIndex == -1 || TargetPathList.Length == 0)
            return;
        //如果图层出错记得找这里!!!!!!!!!!!!!!
        Transform targetPoint = TargetPathList[TargetPointIndex];
        Vector3 targetPosition = targetPoint.position + new Vector3(HoriOffset,0,0);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeedMultiplier * Time.deltaTime);

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
    
    }
}
