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

    //��ѡ�е���̨,Ҫ�������̨
    public TurretData beselectTurretData;//ui��ѡ�����̨
    //��ǰѡ�е���̨�������е���Ϸ����
    public Placed selectedPlaced;

    public Text moneyText;
    public Animator moneyAnimator;
    public float money = 500;
    public GameObject upgradeCanvas;
    private Animator upgradeCanvasAnimator;
    public Button btnUpgrade;

    public Material mPlaced;//��ȡ������
    public static bool AllowtoBuile = true;//�����Ƿ�ɽ���
    RaycastHit hit;

    private void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }


    //��Ǯ�䶯
    public void ChangeMoney(float change = 0)
    {
        money += change;
        moneyText.text = "��" + money;
    }
    private void Awake()
    {
        moneyText.text = "��" + money;
        mPlaced.color = Color.white;
        AllowtoBuile = false;
    }

    private void Update()
    {
        //GetMouseButtonDown(0) 0����������
        if (Input.GetMouseButtonDown(0))
        {
            //EventSystem.current.IsPointerOverGameObject()����ʾ�������ui�ϡ�
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {   //û�㵽ui������̨
                //����λ��ת��Ϊ����
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //���߼�� ��Χ1000 ������hit
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("placed"));
                if (isCollider)
                {
                    Placed placed = hit.collider.GetComponent<Placed>();//��÷��õ��ϵ���̨
                    if (placed.turretGo == null && AllowtoBuile == true)
                    {   //���Դ���
                        if (money >= beselectTurretData.cost)
                        {
                            ChangeMoney(-beselectTurretData.cost);//�ı�����ʾ
                            placed.Buildturret(beselectTurretData);//��������
                            AllowtoBuile = false;
                            mPlaced.color = Color.white;
                            laerToggle.SetIsOnWithoutNotify(false);
                            MissileToggle.SetIsOnWithoutNotify(false);
                            standardToggle.SetIsOnWithoutNotify(false);

                        }
                        else
                        {
                         //   Debug.Log("Ǯ����");
                            moneyAnimator.SetTrigger("Flick");
                        }
                    }
                    else if (placed.turretGo != null)
                    {
                        //�����������
                        //print("�����������");

                        //�ж�ui��̨��������̨�Ƿ���ͬһ����ͬ���Ļ�������ui��
                        if (placed == selectedPlaced && upgradeCanvas.activeInHierarchy)
                        {   //����ui,Э��
                            StartCoroutine(HideUpgradeUI());

                        }
                        else
                        {
                            //placed.isUpgrade��ȡisUpgrade���ж������Ƿ��������
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
            //�ɷ���λ��ɫ
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
            //�ɷ���λ��ɫ
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
            //�ɷ���λ��ɫ
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
        //����ͣ����ui�Ķ�����������״̬����ʹ�л������̨ʱ չʾ����
        StopCoroutine(HideUpgradeUI());
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);

        upgradeCanvas.transform.position = pos;
        btnUpgrade.interactable = !isDisableUpdgrade;
    }
    IEnumerator HideUpgradeUI()
    {
        //���Ŷ��� 
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {
        //������ť
        //�ж���������Ǯ�Ƿ�
        if (money >= selectedPlaced.turretData.costUpgraded)
        {
            ChangeMoney(-selectedPlaced.turretData.costUpgraded);//��Ǯ
            selectedPlaced.UpgradeTurret();
            StartCoroutine(HideUpgradeUI());
           // Debug.Log("����");
        }
        else
        {
           // Debug.Log("Ǯ����");
            moneyAnimator.SetTrigger("Flick");
        }


    }
    public void OnDestroyButtonDown()
    {
        //�����ť
        ChangeMoney((float)(+selectedPlaced.turretData.cost*0.5));
        selectedPlaced.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
        //Debug.Log("���");

    }
}
