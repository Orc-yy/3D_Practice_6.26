using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // ���� ������ ��
    [SerializeField]
    private Gun currentGun;

    // �ѱ��� ���� �ӵ�
    private float currentFireRate;

    // ���� ����
    private bool isReloading = false;
    [HideInInspector]
    public bool isFineSightMode = false; // ������ ��� ����

    // ���� ������ ��
    private Vector3 originPos;

    // ȿ���� ���
    private AudioSource audioSource;


    // ������ �浹 ���� �޾ƿ�
    private RaycastHit hitInfo;


    // �ʿ��� ������Ʈ
    [SerializeField]
    private Camera theCam;
    private CrossHair crosshair;


    [SerializeField]
    private GameObject hit_effect_Prefab;

    void Start()
    {
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        crosshair = FindObjectOfType<CrossHair>();
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();
    }

    // ����ӵ� ���
    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime; // 60���� 1 = 1
        }
    }

    // �߻� �õ�
    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0 && !isReloading)
        {
            Fire();
        }
    }

    // �߻� �� ���
    private void Fire()
    {
        if (!isReloading)
        {
            if (currentGun.currentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
        
    }

    // �߻� �� ���
    private void Shoot()
    {
        crosshair.FireAnimation(); // ũ�ν� ��� �ִϸ��̼� ����
        currentGun.currentBulletCount--; // �Ѿ� ����
        currentFireRate = currentGun.fireRate; // ���� �ӵ� ����
        PlaySE(currentGun.fireSound); // �Ѿ� �߻� ���� ���
        currentGun.muzzleFlash.Play(); // �ѱ� ȭ�� ȿ�� ���
        Hit();

        // �ѱ� �ݵ� �ڷ�ƾ ����
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());

    }

    private void Hit()
    {
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward + 
            new Vector3(Random.Range(-crosshair.GetAccuracy() - currentGun.accuracy, crosshair.GetAccuracy() + currentGun.accuracy),
                        Random.Range(-crosshair.GetAccuracy() - currentGun.accuracy, crosshair.GetAccuracy() + currentGun.accuracy), 0)
                        , out hitInfo, currentGun.range))
        {
            GameObject clone = Instantiate(hit_effect_Prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2f); // 2�� �� �ı�
        }
    }

    // ������ �õ�
    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentGun.carryBulletCount > currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    // ������
    IEnumerator ReloadCoroutine()
    { 
        if(currentGun.carryBulletCount > 0)
        {
            isReloading = true; 
            currentGun.anim.SetTrigger("Reload");

            // ���� �Ѿ��� źâ�� �߰�
            currentGun.carryBulletCount += currentGun.currentBulletCount; 
            // ���� �Ѿ� ���� 0���� �ʱ�ȭ
            currentGun.currentBulletCount = 0; 

            yield return new WaitForSeconds(currentGun.reloadTime); // ������ �ð� ���

            if (currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;    
            }
            else
            {
                currentGun.currentBulletCount += currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            isReloading = false; 
        }
        else
        {
            Debug.Log("������ �Ѿ��� �����ϴ�.");
        }
    }

    // ������ �õ�
    private void TryFineSight()
    {
        if(Input.GetButtonDown("Fire2") && !isReloading)
        {
            FineSight();
        }
    }

    // ������ ���
    public void CancelFineSight()
    {
        if (isFineSightMode)
        {
            FineSight();
        }
    }

    // ������ ���� ����
    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);
        crosshair.FineSightAnimation(isFineSightMode);

        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightCoroutine());
        }
        else
        {
            StartCoroutine(FineSightDeactivateCoroutine());
        }

    }

    // ������ Ȱ��ȭ
    IEnumerator FineSightCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }

    // ������ ��Ȱ��ȭ
    IEnumerator FineSightDeactivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    // �ݵ� �ڷ�ƾ
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3 (currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);

        if (!isFineSightMode)
        {
            currentGun.transform.localPosition = originPos;
            // �ݵ� ����
            while(currentGun.transform.localPosition.x <= currentGun.retroActionForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            // �ݵ� ��
            while (currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }

        }
        else
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;
            // �ݵ� ����
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            // �ݵ� ��
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }



    // ȿ���� ��� �Լ�
    private void PlaySE(AudioClip _clip)    
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun()
    {
        return currentGun;
    }

    public bool GetFineSightMode()
    {
        return isFineSightMode;
    }
}
