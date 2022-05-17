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
        SpeedText.text = "正常";
        StopText.text = "暂停";
        gm = this;
        enemyCreate = GetComponent<EnemyCreate>();
    }
    public void Win()
    {
        endUi.SetActive(true);
        endMsg.text = "获 胜";
    }
    public void Failed()
    {
        endUi.SetActive(true);
        endMsg.text = "失 败";
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
        SceneManager.LoadScene(1);//加载正在活跃的场景，：重新加载
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
                SpeedText.text = "二倍";
                break;
            case 2:
                Time.timeScale++;
                SpeedText.text = "三倍";
                break;
            case 3:
                Time.timeScale = 1;
                SpeedText.text = "正常";
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
            SpeedText.text = "正常";
            StopText.text = "暂停";
        }
        else if (Time.timeScale != 0)
        {
            StopText.text = "继续";
            Time.timeScale = 0;

        }

    }

}
