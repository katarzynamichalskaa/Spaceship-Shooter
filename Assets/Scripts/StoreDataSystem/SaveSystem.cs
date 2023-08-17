using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class SaveSystem
{
   public static void SavePlayerMoney(MoneyManager moneyManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/money.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(moneyManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayerEquipment(ShopManager shopManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/equipment.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(shopManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerMoney(MoneyManager moneyManager)
    {
        string path = Application.persistentDataPath + "/money.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data =  formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(moneyManager);

            formatter.Serialize(stream, data);
            stream.Close();

            return null;
        }

    } 
    public static PlayerData LoadPlayerEquipment(ShopManager shopManager)
    {
        string path = Application.persistentDataPath + "/equipment.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(shopManager);

            formatter.Serialize(stream, data);
            stream.Close();

            return null;
        }

    }
}
