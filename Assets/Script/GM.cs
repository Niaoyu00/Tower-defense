using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public Text SpeedText;
    public Text StopText;
    public GameObject endUi;
    public Text endMsg;
    public static GM gm;
    private EnemyCreate enemyCreate;
    private void Awake()
    {
        SpeedText.text = "����";
        StopText.text = "��ͣ";
        gm = this;
        enemyCreate = GetComponent<EnemyCreate>();
    }
    public void Win()
    {
        endUi.SetActive(true);
        endMsg.text = "�� ʤ";
    }
    public void Failed()
    {
        endUi.SetActive(true);
        endMsg.text = "ʧ ��";
        enemyCreate.Stop();
        StartCoroutine("Pause");
    }
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }
    public void OnBtnRetry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);//�������ڻ�Ծ�ĳ����������¼���
    }
    public void OnBtnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ChangeSpeed()
    {

        switch (Time.timeScale)
        {
            case 1:
                Time.timeScale++;
                SpeedText.text = "����";
                break;
            case 2:
                Time.timeScale++;
                SpeedText.text = "����";
                break;
            case 3:
                Time.timeScale = 1;
                SpeedText.text = "����";
                break;
            default:
                break;
        }

    }
    public void StopSpeed()
    {

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SpeedText.text = "����";
            StopText.text = "��ͣ";
        }
        else if (Time.timeScale != 0)
        {
            StopText.text = "����";
            Time.timeScale = 0;

        }

    }

}
