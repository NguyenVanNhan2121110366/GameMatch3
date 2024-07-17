using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SaveData 
{
    public bool[] isCheckMenu = new bool[6];
    public float[] healPlayer = new float[6];
    public float[] healEnemy = new float[6];
    public float[] manaPlayer = new float[6];
    public float[] manaEnemy = new float[6];
}
public class GameData : MonoBehaviour
{
    private static GameData _instance;
    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameData>();
            }
            return _instance;
        }
    }
    public SaveData saveData;
    private void Awake()
    {
        
    }

    public void Save()
    {
        //Create one binary Formatter
        var binary = new BinaryFormatter();
        //Create Link File save data 
        FileStream file = File.Open(Application.persistentDataPath + "/Game.data", FileMode.OpenOrCreate);
        var saveDataGame = new SaveData();
        saveDataGame = saveData;
        //encodes data to binary 
        binary.Serialize(file, saveDataGame);
        //close file
        file.Close();
        Debug.Log("Save Game");

    }


    public void Load()
    {
        // Kiểm tra xem tệp tồn tại không
        string filePath = Application.persistentDataPath + "/Game.data";
        if (File.Exists(filePath))
        {
            // Mở tệp để đọc dữ liệu
            var binary = new BinaryFormatter();
            var file = File.Open(filePath, FileMode.Open);
            saveData = (SaveData)binary.Deserialize(file);
            file.Close();
        }
        else
        {
            // Nếu không tìm thấy tệp, thông báo và tạo dữ liệu mặc định
            Debug.LogWarning("Save file not found: " + filePath);
        }
    }
}
