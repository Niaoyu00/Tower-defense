using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreate : MonoBehaviour
{
    public static int CountEnemyAlive = 0;//��������
    public Wave[] waves;
    public Transform Begin;
    public float waveRate = 3;


    void Start()
    {
        StartCoroutine("SpawnEnamy");
    }
    public void Stop()
    {
        //ֹͣ����
        StopCoroutine("SpawnEnamy");
    }
    IEnumerator SpawnEnamy()
    {
        foreach (Wave w in waves)
        {

            for (int i = 0; i < w.count; i++)
            {
                //ʵ��������
                GameObject.Instantiate(w.enemyprefab, Begin.position, Quaternion.identity);
                CountEnemyAlive++;
                //���һ���������ɲ���Ҫ�м��
                if (i != w.count - 1)
                {
                    yield return new WaitForSeconds(w.rate);
                }

            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;//��ͣ
            }
            //ÿһ��֮��ļ������λs
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0)
        {
            yield return 0;//��ͣ
        }
       // Debug.Log("��ʤ");
        GM.gm.Win();

    }
}
