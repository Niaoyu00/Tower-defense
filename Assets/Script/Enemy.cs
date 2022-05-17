using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform[] Enpositions;
    private int index = 0;//怪物路径拐点下标
    public float speed;//怪物行进速度
    public float Enemyspeed = 100;//怪物旋转速度
    public float HP = 100;//总血量血量
    private float TotalHP;
    public GameObject EnemyhitEffect;//怪物受击特效
    public Transform Enemyobj;//获取怪物子类(用于旋转)
    private Slider hpSlider;//血量条
    public Text hpTxt;
    void Start()
    {
        Enpositions = WayPoints.postions;
        TotalHP = HP;
        hpSlider = GetComponentInChildren<Slider>();
        hpTxt.text = HP.ToString("#0.0");//保留小数点后一位

    }
    private void Move()
    {
        //if (index > Enpositions.Length - 1) return;

        if (index > Enpositions.Length - 1)
        {
            GM.gm.Failed();
            //Debug.Log("游戏失败");
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            transform.Translate((Enpositions[index].position - transform.position).normalized * Time.deltaTime * speed);
            //使子对象旋转
            Enemyobj.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * Enemyspeed);
            //与标记距离小于0.2
            if (Vector3.Distance(Enpositions[index].position, transform.position) < 0.2f)
            {
                index++;
            }
        }

    }

    private void OnDestroy()
    {
        EnemyCreate.CountEnemyAlive--;//敌人数量
    }

    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;
        HP -= damage;
        hpSlider.value = HP / TotalHP;

        hpTxt.text = HP.ToString("#0.0");//保留小数点后一位
        if (HP <= 0)
        {
            EnemyDie();
        }
        //Debug.Log($"敌人扣血:-{hpSlider.value}");

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
