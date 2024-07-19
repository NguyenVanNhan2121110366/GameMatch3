using UnityEngine;

public class SaveAllData : MonoBehaviour
{
    private AllDotController _alldots;
    private void Start()
    {
        this._alldots = FindFirstObjectByType<AllDotController>();
    }
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

    private void LoadArray()
    {
        for (var i = 0; i < this._alldots.Width; i++)
        {
            for (var j = 0; j < this._alldots.Height; j++)
            {
                if (this._alldots.AllDots[i, j] != null)
                {
                    
                }
            }
        }
    }
}
