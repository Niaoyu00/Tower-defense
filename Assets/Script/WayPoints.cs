using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] postions;
    // Start is called before the first frame update
    private void Awake()
    {
        postions = new Transform[transform.childCount];
        for (int i = 0; i < postions.Length; i++)
        {
            //ֱ��transform����ֱ���������λ������
            postions[i] = transform.GetChild(i);
        }
    }
}
