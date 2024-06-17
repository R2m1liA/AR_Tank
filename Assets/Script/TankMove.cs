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
        mainCamera = Camera.main;
        fixedJoystick = GameObject.FindObjectOfType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        //float verticalMove = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // ȷ��ǰ�����yֵΪ0���Ա��ⴹֱ�����Ӱ��
        cameraForward.y = 0;
        cameraForward.Normalize();

        // ȷ���ҷ����yֵΪ0
        cameraRight.y = 0;
        cameraRight.Normalize();

        // ����������ķ�������ƶ�����
        Vector3 movement = cameraForward * fixedJoystick.Vertical + cameraRight * fixedJoystick.Horizontal;

        // �ƶ�����
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // �������ƶ�����ʱ�Ÿı䳯��
        if (movement != Vector3.zero)
        {
            // ���㳯��
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            // ��ֵ��ת��Ŀ�곯��
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, moveSpeed * Time.deltaTime * 100f);
        }
    }
}
