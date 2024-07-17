using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonAbility : MonoBehaviour
{
    private SaveAllData _saveAll;
    [SerializeField] private Button bntAbility;
    [SerializeField] private GameObject bgrAbility;
    [SerializeField] private Button exitAbility;

    [SerializeField] private Button bntHome;
    [SerializeField] private Button bntRestart;
    [SerializeField] private Button bntExitGame;
    [SerializeField] private Button setting;
    [SerializeField] private Button bntSave;
    [SerializeField] private Button bntLoad;
    private bool _isCheckMenu;

    private void Awake()
    {
        this._saveAll = FindFirstObjectByType<SaveAllData>();
        this.bntAbility.onClick.AddListener(this.OnClickAbility);
        this.exitAbility.onClick.AddListener(this.ExitAbility);
        this.bntHome.onClick.AddListener(this.ClickHome);
        this.bntRestart.onClick.AddListener(this.ClickRestart);
        this.bntExitGame.onClick.AddListener(this.ClickExit);
        this.setting.onClick.AddListener(Setting);
        this.bntSave.onClick.AddListener(this.ClickSaveGame);
        this.bntLoad.onClick.AddListener(this.ClickLoadGame);
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
        Time.timeScale = 1;
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
        StartCoroutine(SettingCoroutine());
    }

    private IEnumerator SettingCoroutine()
    {
        if (this._isCheckMenu)
        {
            TurnController.Instance.menu.SetActive(true);
            yield return new WaitForSeconds(1f);
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

    private void ClickSaveGame()
    {
        this._saveAll.SaveDataGame();
    }

    private void ClickLoadGame()
    {
        Time.timeScale = 1;
        TurnController.Instance.menu.SetActive(false);
        this._saveAll.LoadAllData();
    }

}
