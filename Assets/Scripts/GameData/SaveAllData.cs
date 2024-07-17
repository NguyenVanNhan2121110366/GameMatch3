using UnityEngine;

public class SaveAllData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.SaveDataGame();
        this.LoadAllData();
    }

    public void SaveDataGame()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameData.Instance.saveData.healPlayer[0] = Player.Instance.BloodCurrentScore;
            GameData.Instance.saveData.manaPlayer[0] = Player.Instance.PowerCurrentScore;
            GameData.Instance.saveData.healEnemy[0] = Enemy.Instance.BloodCurrentScore;
            GameData.Instance.saveData.manaEnemy[0] = Enemy.Instance.BloodCurrentScore;
            Debug.Log("Save success");
            GameData.Instance.Save();
        }

    }

    public void LoadAllData()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {

            Debug.Log("Load success");
            GameData.Instance.Load();
            Player.Instance.BloodCurrentScore = GameData.Instance.saveData.healPlayer[0];
            Player.Instance.PowerCurrentScore = GameData.Instance.saveData.manaPlayer[0];
            Enemy.Instance.BloodCurrentScore = GameData.Instance.saveData.healEnemy[0];
            Enemy.Instance.BloodCurrentScore = GameData.Instance.saveData.manaEnemy[0];
        }

    }
}
