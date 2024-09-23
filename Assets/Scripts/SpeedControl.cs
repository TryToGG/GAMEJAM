using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedControl : MonoBehaviour
{
    public PlayerMoveCtrl playerMoveCtrl;
    public GameObject snail;
    public void SpeedDown()
    {
        playerMoveCtrl.BasicMoveSpeed = playerMoveCtrl.BasicMoveSpeed - 0.025f;
    }


    public bool touchingStone = false;
    public bool touchingLeaf = false;
    public float playerSpeedMultip = 1.5f;
    public float MoveSpeed = 1f; 
    public float origBasicSpeed = 1f;

    public float playerSpeedReducer = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        playerMoveCtrl = snail.GetComponent <PlayerMoveCtrl>();
        origBasicSpeed = playerMoveCtrl.BasicMoveSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpeed = playerMoveCtrl.MoveSpeed;

        if (MoveSpeed > origBasicSpeed)
            {
                MoveSpeed *= playerSpeedReducer;
            }
            else if(MoveSpeed < origBasicSpeed)
            {
            MoveSpeed = origBasicSpeed;
            }

    }
    // 当发生物理碰撞时调用（针对2D碰撞）
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 获取碰撞的对象
        GameObject otherObject = collision.gameObject;

        // 打印碰撞对象的名字
        Debug.Log("Collided with: " + otherObject.name);
        
        // 例如，检测与特定物体的碰撞
        if (otherObject.CompareTag("Stone"))
        {
            if (playerMoveCtrl.BasicMoveSpeed >= 0)
            {
                SpeedDown();
            }

            touchingStone = true;

        }
      
    
        if (otherObject.CompareTag("Leaf"))
        {   
        
            playerMoveCtrl.MoveSpeed *= playerSpeedMultip;

            touchingLeaf = true;
        }   
    }

    
    // 当触发器发生接触时调用（针对2D触发器）
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);
    }
   
}
    
   

   

