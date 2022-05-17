using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int damage = 50;//�˺�ֵ
    public float speed = 20;
    public GameObject hitEffect;//��Ч

    private Transform target;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    private void Update()
    {
        if (target==null)
        {
            Destroyself();
            Destroy(this.gameObject);
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
    void Destroyself() {
        GameObject hiteff = GameObject.Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(hiteff,1.5f);
    }
    //�ӵ��Ӵ�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //1.���˵�Ѫ
            other.GetComponent<Enemy>().TakeDamage(damage);
            //��ǰλ��ʵ������Ч
            Destroyself();
            Destroy(this.gameObject);

        }
    }
}
