using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BuildManager : MonoBehaviour
{
    public Toggle laerToggle;
    public Toggle MissileToggle;
    public Toggle standardToggle;


    public TurretData laerData;
    public TurretData missileData;
    public TurretData standardData;

    //被选中的炮台,要建造的炮台
    public TurretData beselectTurretData;//ui中选择的炮台
    //当前选中的炮台，场景中的游戏物体
    public Placed selectedPlaced;

    public Text moneyText;
    public Animator moneyAnimator;
    public float money = 500;
    public GameObject upgradeCanvas;
    private Animator upgradeCanvasAnimator;
    public Button btnUpgrade;

    public Material mPlaced;//获取材质球
    public static bool AllowtoBuile = true;//控制是否可建造
    RaycastHit hit;

    private void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }


    //金钱变动
    public void ChangeMoney(float change = 0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }
    private void Awake()
    {
        moneyText.text = "￥" + money;
        mPlaced.color = Color.white;
        AllowtoBuile = false;
    }

    private void Update()
    {
        //GetMouseButtonDown(0) 0代表鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            //EventSystem.current.IsPointerOverGameObject()，表示点击到了ui上。
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {   //没点到ui则建造炮台
                //鼠标的位置转化为射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //射线检测 范围1000 返回至hit
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("placed"));
                if (isCollider)
                {
                    Placed placed = hit.collider.GetComponent<Placed>();//获得放置点上的炮台
                    if (placed.turretGo == null && AllowtoBuile == true)
                    {   //可以创建
                        if (money >= beselectTurretData.cost)
                        {
                            ChangeMoney(-beselectTurretData.cost);//改变金额显示
                            placed.Buildturret(beselectTurretData);//创建炮塔
                            AllowtoBuile = false;
                            mPlaced.color = Color.white;
                            laerToggle.SetIsOnWithoutNotify(false);
                            MissileToggle.SetIsOnWithoutNotify(false);
                            standardToggle.SetIsOnWithoutNotify(false);

                        }
                        else
                        {
                         //   Debug.Log("钱不够");
                            moneyAnimator.SetTrigger("Flick");
                        }
                    }
                    else if (placed.turretGo != null)
                    {
                        //弹出升级面板
                        //print("弹出升级面板");

                        //判断ui炮台和物体炮台是否是同一个，同样的话就隐藏ui。
                        if (placed == selectedPlaced && upgradeCanvas.activeInHierarchy)
                        {   //隐藏ui,协程
                            StartCoroutine(HideUpgradeUI());

                        }
                        else
                        {
                            //placed.isUpgrade获取isUpgrade中判断炮塔是否可升级。
                            ShowUpgradeUI(placed.transform.position, placed.isUpgrade);
                        }
                        selectedPlaced = placed;
                    }
                }
            }
        }
    }



    public void OnLaerSelected(bool isOn)
    {

        if (isOn)
        {
            AllowtoBuile = true;
            beselectTurretData = laerData;
            //可放置位变色
            mPlaced.color = Color.yellow;
        }
        else
        {
            AllowtoBuile = false;
            mPlaced.color = Color.white;
        }

    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            AllowtoBuile = true;
            beselectTurretData = missileData;
            //可放置位变色
            mPlaced.color = Color.green;
        }
        else
        {
            AllowtoBuile = false;
            mPlaced.color = Color.white;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            AllowtoBuile = true;
            beselectTurretData = standardData;
            //可放置位变色
            mPlaced.color = Color.gray;
        }
        else
        {
            AllowtoBuile = false;
            mPlaced.color = Color.white;
        }
    }
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpdgrade = false)
    {
        //先暂停隐藏ui的动画，再重启状态机，使切换点击炮台时 展示动画
        StopCoroutine(HideUpgradeUI());
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);

        upgradeCanvas.transform.position = pos;
        btnUpgrade.interactable = !isDisableUpdgrade;
    }
    IEnumerator HideUpgradeUI()
    {
        //播放动画 
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {
        //升级按钮
        //判断升级所需钱是否够
        if (money >= selectedPlaced.turretData.costUpgraded)
        {
            ChangeMoney(-selectedPlaced.turretData.costUpgraded);//改钱
            selectedPlaced.UpgradeTurret();
            StartCoroutine(HideUpgradeUI());
           // Debug.Log("升级");
        }
        else
        {
           // Debug.Log("钱不够");
            moneyAnimator.SetTrigger("Flick");
        }


    }
    public void OnDestroyButtonDown()
    {
        //拆除按钮
        ChangeMoney((float)(+selectedPlaced.turretData.cost*0.5));
        selectedPlaced.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
        //Debug.Log("拆除");

    }
}
