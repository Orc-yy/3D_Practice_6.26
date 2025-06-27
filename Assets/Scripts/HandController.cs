using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // ���� ������ Hand�� Ÿ�� ����
    [SerializeField]
    private Hand currentHand;

    // ������ ?
    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hitInfo; // ���� ������ ������ �����


    

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                // �ڷ�ƾ ����
                StartCoroutine(AttackCoroutine());
            }
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true; // �ߺ� ���� ����
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDelayA);
        isSwing = true;

        // ���� Ȱ��ȭ ����
        StartCoroutine(HitCorutine());

        yield return new WaitForSeconds(currentHand.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentHand.attackDelayA - currentHand.attackDelayB);
        isAttack = false;

    }

    IEnumerator HitCorutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false; // �ѹ��� �浹�ϰ� ����

                // �浹 ����
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range))
        {
            return true;
        }
        return false;
    }
}
