using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonAbility : MonoBehaviour
{
    [SerializeField] private Button bntAbility;
    [SerializeField] private GameObject bgrAbility;
    [SerializeField] private Button exitAbility;

    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntRestart;
    [SerializeField] private Button bntExitGame;
    [SerializeField] private Button setting;
    private bool _isCheckMenu;

    private void Awake()
    {
        this.bntAbility.onClick.AddListener(this.OnClickAbility);
        this.exitAbility.onClick.AddListener(this.ExitAbility);
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntRestart.onClick.AddListener(this.ClickRestart);
        this.bntExitGame.onClick.AddListener(this.ClickExit);
        this.setting.onClick.AddListener(this.Setting);
    }
    private void Start()
    {
        this._isCheckMenu = true;
    }

    public void OnClickAbility()
    {
        this.bgrAbility.SetActive(true);
    }

    public void ExitAbility()
    {
        this.bgrAbility.SetActive(false);
    }
    private void ClickHome()
    {
        SceneManager.LoadScene("Menu");
    }
    private void ClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    private void ClickExit()
    {
        Application.Quit();
    }

    private void Setting()
    {
        if (this._isCheckMenu)
        {
            TurnController.Instance.menu.SetActive(true);
            Time.timeScale = 0;
            this._isCheckMenu = false;
        }
        else
        {
            Time.timeScale = 1;
            TurnController.Instance.menu.SetActive(false);
            this._isCheckMenu = true;
        }
    }

}
