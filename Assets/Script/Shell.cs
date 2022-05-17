using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int damage = 50;//伤害值
    public float speed = 20;
    public GameObject hitEffect;//特效

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
    //子弹接触敌人
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //1.敌人掉血
            other.GetComponent<Enemy>().TakeDamage(damage);
            //当前位置实例化特效
            Destroyself();
            Destroy(this.gameObject);

        }
    }
}
