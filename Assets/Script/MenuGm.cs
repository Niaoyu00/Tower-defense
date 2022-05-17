using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGm : MonoBehaviour
{
    public GameObject illUI;
    public void Illustrate() {
        illUI.SetActive(true);
    }
    public void Back()
    {
        illUI.SetActive(false);
    }
    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
