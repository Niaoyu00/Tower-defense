using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreate : MonoBehaviour
{
    public static int CountEnemyAlive = 0;//敌人数量
    public Wave[] waves;
    public Transform Begin;
    public float waveRate = 3;


    void Start()
    {
        StartCoroutine("SpawnEnamy");
    }
    public void Stop()
    {
        //停止运行
        StopCoroutine("SpawnEnamy");
    }
    IEnumerator SpawnEnamy()
    {
        foreach (Wave w in waves)
        {

            for (int i = 0; i < w.count; i++)
            {
                //实例化对象
                GameObject.Instantiate(w.enemyprefab, Begin.position, Quaternion.identity);
                CountEnemyAlive++;
                //最后一个敌人生成不需要有间隔
                if (i != w.count - 1)
                {
                    yield return new WaitForSeconds(w.rate);
                }

            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;//暂停
            }
            //每一波之间的间隔，单位s
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0)
        {
            yield return 0;//暂停
        }
       // Debug.Log("获胜");
        GM.gm.Win();

    }
}
