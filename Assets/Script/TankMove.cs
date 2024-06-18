using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed;
    public FixedJoystick fixedJoystick;
    public Camera mainCamera;
    public AudioClip idleEngineClip;  // ����״̬��������Ƶ����
    public AudioClip movingEngineClip;  // �ƶ�״̬��������Ƶ����

    private AudioSource idleEngineSource;  // ����״̬����ƵԴ
    private AudioSource movingEngineSource;  // �ƶ�״̬����ƵԴ

    public float idleVolume = 0.5f;  // ����״̬������
    public float movingVolume = 1.0f;  // �ƶ�״̬������

    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = GameObject.FindObjectOfType<FixedJoystick>();

        // ������ƵԴ
        idleEngineSource = gameObject.AddComponent<AudioSource>();
        movingEngineSource = gameObject.AddComponent<AudioSource>();

        // ������ƵԴ
        idleEngineSource.clip = idleEngineClip;
        idleEngineSource.loop = true;
        idleEngineSource.volume = idleVolume;

        movingEngineSource.clip = movingEngineClip;
        movingEngineSource.loop = true;
        movingEngineSource.volume = movingVolume;

        // ���Ŵ���״̬����������
        idleEngineSource.Play();
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

        // ���̹���Ƿ����ƶ�
        isMoving = movement != Vector3.zero;

        // �����ƶ�״̬���Ų�ͬ����������
        if (isMoving && !movingEngineSource.isPlaying)
        {
            idleEngineSource.Stop();
            movingEngineSource.Play();
        }
        else if (!isMoving && !idleEngineSource.isPlaying)
        {
            movingEngineSource.Stop();
            idleEngineSource.Play();
        }


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
