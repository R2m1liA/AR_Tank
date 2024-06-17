using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed;
    public FixedJoystick fixedJoystick;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = GameObject.FindObjectOfType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        //float verticalMove = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // 确保前方向的y值为0，以避免垂直方向的影响
        cameraForward.y = 0;
        cameraForward.Normalize();

        // 确保右方向的y值为0
        cameraRight.y = 0;
        cameraRight.Normalize();

        // 根据摄像机的方向计算移动方向
        Vector3 movement = cameraForward * fixedJoystick.Vertical + cameraRight * fixedJoystick.Horizontal;

        // 移动物体
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // 仅当有移动输入时才改变朝向
        if (movement != Vector3.zero)
        {
            // 计算朝向
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            // 插值旋转到目标朝向
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, moveSpeed * Time.deltaTime * 100f);
        }
    }
}
