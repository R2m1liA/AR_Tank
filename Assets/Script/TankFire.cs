using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankFire : MonoBehaviour
{
    public GameObject shell;
    public Transform fire;
    public Button firebutton;
    public float cooldownTime; // ������ȴʱ��
    private bool canFire = true; // �Ƿ���Թ���
    public AudioClip FireSound; // ��ը��Ч
    public CooldownBar cooldownBar;
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
            // �����ڵ�
            Instantiate(shell, fire.position, transform.rotation);
            AudioSource.PlayClipAtPoint(FireSound, transform.position);
            cooldownBar.StartCooldown();
            // ��ʼ��ȴ
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        canFire = false; // ��ʱ���ܹ���
        yield return new WaitForSeconds(cooldownTime); // �ȴ���ȴʱ��
        canFire = true; // ��ȴ��ɣ����Թ���
    }
}
