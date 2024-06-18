using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed;
    public FixedJoystick fixedJoystick;
    public Camera mainCamera;
    public AudioClip idleEngineClip;  // 待机状态的引擎音频剪辑
    public AudioClip movingEngineClip;  // 移动状态的引擎音频剪辑

    private AudioSource idleEngineSource;  // 待机状态的音频源
    private AudioSource movingEngineSource;  // 移动状态的音频源

    public float idleVolume = 0.5f;  // 待机状态的音量
    public float movingVolume = 1.0f;  // 移动状态的音量

    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = GameObject.FindObjectOfType<FixedJoystick>();

        // 创建音频源
        idleEngineSource = gameObject.AddComponent<AudioSource>();
        movingEngineSource = gameObject.AddComponent<AudioSource>();

        // 设置音频源
        idleEngineSource.clip = idleEngineClip;
        idleEngineSource.loop = true;
        idleEngineSource.volume = idleVolume;

        movingEngineSource.clip = movingEngineClip;
        movingEngineSource.loop = true;
        movingEngineSource.volume = movingVolume;

        // 播放待机状态的引擎声音
        idleEngineSource.Play();
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

        // 检查坦克是否在移动
        isMoving = movement != Vector3.zero;

        // 根据移动状态播放不同的引擎声音
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
