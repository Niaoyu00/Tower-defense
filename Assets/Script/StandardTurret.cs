using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    //进入触发范围
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //list中添加敌人
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
    public float attackRateTime = 1; //攻击cd 单位 秒
    private float timer = 0;

    public GameObject shellPrefab;//炮弹
    public Transform firePosition;
    public Transform Turrethead;//炮筒

    public bool useLaser = false;
    public float damageRate = 60;//攻击速度
    public LineRenderer laserR;
    public GameObject laserEffect;

    private void Start()
    {
        //放置炮塔若有敌人，直接攻击
        timer = attackRateTime;
    }

    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            //Debug.Log("炮塔索敌");
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = Turrethead.position.y;
            Turrethead.LookAt(targetPosition);
        }
        if (!useLaser)//不使用激光攻击
        {
            //使用子弹攻击
            timer += Time.deltaTime;
            //敌人大于0并且cd时间足够
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
                //集合有null值时，清除空项
                UpdateEnemys();
            }
            //使用激光
            if (enemys.Count > 0)
            {
                //设置激光位置，定义数组。己方，敌方
                laserR.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;//获取炮塔自身的y轴
                pos.y = enemys[0].transform.position.y;//是他跟敌人y轴保持一致
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
            //集合有null值时，清除空项
            UpdateEnemys();
        }
        else if (enemys.Count > 0)
        {
            //集合中除了第一项，还有敌人，则继续攻击
            GameObject shell = GameObject.Instantiate(shellPrefab, firePosition.position, firePosition.rotation);
            shell.GetComponent<Shell>().SetTarget(enemys[0].transform);

        }
        else
        {
            //完全为空，将定时器重置为攻击状态
            timer = attackRateTime;
        }
    }
    void UpdateEnemys()
    {
        //获取值为null的集合的索引 并删除值
        enemys.RemoveAll(item => item == null);
    }
}

