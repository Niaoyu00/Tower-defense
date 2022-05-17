using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Placed : MonoBehaviour
{

    [HideInInspector]
    public GameObject turretGo;//���浱ǰ���õ��ϵ���̨
    public GameObject buileEffect;
    [HideInInspector]
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgrade = false;//Ĭ����δ����

    public void Buildturret(TurretData turretData)
    {
        this.turretData = turretData;
       // Debug.Log("����");

        isUpgrade = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject eff = GameObject.Instantiate(buileEffect, transform.position, Quaternion.identity);
        Destroy(eff, 1.5f);//�ӳ�����
    }
    public void UpgradeTurret()
    {
        if (isUpgrade) return;
        Destroy(turretGo);//���پ���̨
        isUpgrade = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject eff = GameObject.Instantiate(buileEffect, transform.position, Quaternion.identity);
        Destroy(eff, 1.5f);//�ӳ�����
    }
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgrade = false;
        turretGo = null;
        turretData = null;
        GameObject eff = GameObject.Instantiate(buileEffect, transform.position, Quaternion.identity);
        Destroy(eff, 1.5f);//�ӳ�����
    }


}
