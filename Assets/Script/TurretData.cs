using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData 
{
    public GameObject turretPrefab;
    public float cost;//���
    public GameObject turretUpgradedPrefab;
    public float costUpgraded;//���
    //public TurretType type;
}
public enum TurretType
{
    LaserBeamer,
    MissileLauncher,
    StandardTurret
}
