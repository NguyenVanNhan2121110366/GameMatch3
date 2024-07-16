using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMenuController : MonoBehaviour
{
    [SerializeField] private Button bntStart;
    [SerializeField] private Button bntExit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.bntStart.onClick.AddListener(ClickStart);
        this.bntExit.onClick.AddListener(ClickExit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClickStart()
    {
        SceneManager.LoadScene("Game");
    }

    private void ClickExit()
    {
        Application.Quit();
    }

}
