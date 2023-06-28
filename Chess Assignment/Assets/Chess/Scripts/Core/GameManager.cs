using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject endMenu;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void EndGame(bool white)
    {
        Invoke(nameof(DelayedEndMenu),1f);
        if (white) endMenu.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "WHITE WINS";
        else endMenu.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "BLACK WINS";
    }
    void DelayedEndMenu()
    {
        endMenu.SetActive(true);
    }
    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
