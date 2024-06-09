using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed;
    public FixedJoystick fixedJoystick;

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

        Vector3 movement = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;

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
