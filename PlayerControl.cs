using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D HeroBody;
    public float MaxSpeed = 5;
    public float moveForce = 100;
    public float fInput = 0.1f;//方向量，左负右正
    private bool bFaceRight = true;
    Transform mGroundCheck;

    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();//获取刚体组件
        mGroundCheck = transform.Find("GroundCheck");
    }
    // Update is called once per frame
    void Update()//每帧运行
    {
        fInput = Input.GetAxisRaw("Horizontal");//刚体的移动，Horizontal为X轴的名称
        if (fInput < 0 && bFaceRight)
        {
            flip();
        }
        else if (fInput > 0 && !bFaceRight)
        {
            flip();
        }

        mGroundCheck = Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()//以固定帧率运行
    {
         if (fInput * HeroBody.velocity.x < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * moveForce * fInput);//水平方向施加力，非匀速
        }
        if (Mathf.Abs(HeroBody.velocity.x) > MaxSpeed)//限制最大速度，把超过的速度变回最大速度
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);
        }   
        float y = Input.GetAxis("Jump");
        if (Input.GetButtonDown("Jump"))
        {
            HeroBody.AddForce(Vector3.up * 150 * y);
        }       
    }

    public void flip()//转身
    {
        Vector3 theScale = transform.localScale;//获取父物体的方向
        theScale.x *= -1;//不能直接用transfor.localScale.x *= -1，因为它是一个属性,不提供transfor.localScale.x这样的访问
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }

}
