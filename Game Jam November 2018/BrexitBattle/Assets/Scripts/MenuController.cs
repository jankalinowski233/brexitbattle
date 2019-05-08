using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject settingsPanel;
    public GameObject creditPanel;

    public void showCredits()
    {
        creditPanel.SetActive(true);
    }

    public void hideCredits()
    {
        creditPanel.SetActive(false);
    }
    public void SettingsActive()
    {
        settingsPanel.SetActive(true);
    }

    public void HidePanel()
    {
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
