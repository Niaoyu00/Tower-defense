using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform[] Enpositions;
    private int index = 0;//����·���յ��±�
    public float speed;//�����н��ٶ�
    public float Enemyspeed = 100;//������ת�ٶ�
    public float HP = 100;//��Ѫ��Ѫ��
    private float TotalHP;
    public GameObject EnemyhitEffect;//�����ܻ���Ч
    public Transform Enemyobj;//��ȡ��������(������ת)
    private Slider hpSlider;//Ѫ����
    public Text hpTxt;
    void Start()
    {
        Enpositions = WayPoints.postions;
        TotalHP = HP;
        hpSlider = GetComponentInChildren<Slider>();
        hpTxt.text = HP.ToString("#0.0");//����С�����һλ

    }
    private void Move()
    {
        //if (index > Enpositions.Length - 1) return;

        if (index > Enpositions.Length - 1)
        {
            GM.gm.Failed();
            //Debug.Log("��Ϸʧ��");
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            transform.Translate((Enpositions[index].position - transform.position).normalized * Time.deltaTime * speed);
            //ʹ�Ӷ�����ת
            Enemyobj.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * Enemyspeed);
            //���Ǿ���С��0.2
            if (Vector3.Distance(Enpositions[index].position, transform.position) < 0.2f)
            {
                index++;
            }
        }

    }

    private void OnDestroy()
    {
        EnemyCreate.CountEnemyAlive--;//��������
    }

    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;
        HP -= damage;
        hpSlider.value = HP / TotalHP;

        hpTxt.text = HP.ToString("#0.0");//����С�����һλ
        if (HP <= 0)
        {
            EnemyDie();
        }
        //Debug.Log($"���˿�Ѫ:-{hpSlider.value}");

    }
    void EnemyDie()
    {
        GameObject eff = GameObject.Instantiate(EnemyhitEffect, transform.position, transform.rotation);
        Destroy(eff, 1.5f);
        Destroy(this.gameObject);
      
    }
    void Update()
    {
        Move();
    }

}
