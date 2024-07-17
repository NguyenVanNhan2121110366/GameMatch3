using UnityEngine;

public class SaveAllData : MonoBehaviour
{

    public void SaveDataGame()
    {
        GameData.Instance.saveData.healPlayer[0] = Player.Instance.BloodCurrentScore;
        GameData.Instance.saveData.manaPlayer[0] = Player.Instance.PowerCurrentScore;
        GameData.Instance.saveData.healEnemy[0] = Enemy.Instance.BloodCurrentScore;
        GameData.Instance.saveData.manaEnemy[0] = Enemy.Instance.BloodCurrentScore;
        Debug.Log("Save success");
        GameData.Instance.Save();
    }

    public void LoadAllData()
    {
        Debug.Log("Load success");
        GameData.Instance.Load();
        Player.Instance.BloodCurrentScore = GameData.Instance.saveData.healPlayer[0];
        Player.Instance.PowerCurrentScore = GameData.Instance.saveData.manaPlayer[0];
        Enemy.Instance.BloodCurrentScore = GameData.Instance.saveData.healEnemy[0];
        Enemy.Instance.BloodCurrentScore = GameData.Instance.saveData.manaEnemy[0];
    }
}
