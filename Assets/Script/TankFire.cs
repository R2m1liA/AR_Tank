using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankFire : MonoBehaviour
{
    public GameObject shell;
    public Transform fire;
    public Button firebutton;
    public float cooldownTime; // 攻击冷却时间
    private bool canFire = true; // 是否可以攻击
    // Start is called before the first frame update
    void Start()
    {
        firebutton.onClick.AddListener(Fire);
    }

    // Update is called once per frame
    void Fire()
    {
        if (canFire)
        {
            // 发射炮弹
            Instantiate(shell, fire.position, transform.rotation);
            // 开始冷却
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        canFire = false; // 暂时不能攻击
        yield return new WaitForSeconds(cooldownTime); // 等待冷却时间
        canFire = true; // 冷却完成，可以攻击
    }
}
