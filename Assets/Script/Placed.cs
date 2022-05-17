using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Placed : MonoBehaviour
{

    [HideInInspector]
    public GameObject turretGo;//保存当前放置点上的炮台
    public GameObject buileEffect;
    [HideInInspector]
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgrade = false;//默认塔未升级

    public void Buildturret(TurretData turretData)
    {
        this.turretData = turretData;
       // Debug.Log("建塔");

        isUpgrade = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject eff = GameObject.Instantiate(buileEffect, transform.position, Quaternion.identity);
        Destroy(eff, 1.5f);//延迟销毁
    }
    public void UpgradeTurret()
    {
        if (isUpgrade) return;
        Destroy(turretGo);//销毁旧炮台
        isUpgrade = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject eff = GameObject.Instantiate(buileEffect, transform.position, Quaternion.identity);
        Destroy(eff, 1.5f);//延迟销毁
    }
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgrade = false;
        turretGo = null;
        turretData = null;
        GameObject eff = GameObject.Instantiate(buileEffect, transform.position, Quaternion.identity);
        Destroy(eff, 1.5f);//延迟销毁
    }


}
