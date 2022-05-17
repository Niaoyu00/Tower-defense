using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    //���봥����Χ
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //list����ӵ���
            enemys.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemys.Remove(other.gameObject);
        }

    }
    public float attackRateTime = 1; //����cd ��λ ��
    private float timer = 0;

    public GameObject shellPrefab;//�ڵ�
    public Transform firePosition;
    public Transform Turrethead;//��Ͳ

    public bool useLaser = false;
    public float damageRate = 60;//�����ٶ�
    public LineRenderer laserR;
    public GameObject laserEffect;

    private void Start()
    {
        //�����������е��ˣ�ֱ�ӹ���
        timer = attackRateTime;
    }

    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            //Debug.Log("��������");
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = Turrethead.position.y;
            Turrethead.LookAt(targetPosition);
        }
        if (!useLaser)//��ʹ�ü��⹥��
        {
            //ʹ���ӵ�����
            timer += Time.deltaTime;
            //���˴���0����cdʱ���㹻
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if (enemys.Count > 0)
        {
            if (!laserR.enabled)
            {
                laserR.enabled = true;
                laserEffect.SetActive(true);
            }
            if (enemys[0] == null)
            {
                //������nullֵʱ���������
                UpdateEnemys();
            }
            //ʹ�ü���
            if (enemys.Count > 0)
            {
                //���ü���λ�ã��������顣�������з�
                laserR.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;//��ȡ���������y��
                pos.y = enemys[0].transform.position.y;//����������y�ᱣ��һ��
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserR.enabled = false;
        }


    }

    private void Attack()
    {
        if (enemys[0] == null)
        {
            //������nullֵʱ���������
            UpdateEnemys();
        }
        else if (enemys.Count > 0)
        {
            //�����г��˵�һ����е��ˣ����������
            GameObject shell = GameObject.Instantiate(shellPrefab, firePosition.position, firePosition.rotation);
            shell.GetComponent<Shell>().SetTarget(enemys[0].transform);

        }
        else
        {
            //��ȫΪ�գ�����ʱ������Ϊ����״̬
            timer = attackRateTime;
        }
    }
    void UpdateEnemys()
    {
        //��ȡֵΪnull�ļ��ϵ����� ��ɾ��ֵ
        enemys.RemoveAll(item => item == null);
    }
}

