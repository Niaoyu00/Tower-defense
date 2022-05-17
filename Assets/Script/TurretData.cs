using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData 
{
    public GameObject turretPrefab;
    public float cost;//½ð¶î
    public GameObject turretUpgradedPrefab;
    public float costUpgraded;//½ð¶î
    //public TurretType type;
}
public enum TurretType
{
    LaserBeamer,
    MissileLauncher,
    StandardTurret
}
