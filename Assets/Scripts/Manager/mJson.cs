using System.IO;
using LitJson;
using UnityEngine;

public enum JsonType
{
    JsonUtlity,
    LitJson,
}
public class mJson
{
    static mJson instance = new mJson();
    public static mJson Instance => instance;
    private mJson() { }

    public void SavaData(object data, string fileName, JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string str;
        if (type == JsonType.LitJson) str = JsonMapper.ToJson(data);
        else str = JsonUtility.ToJson(data);
        File.WriteAllText(path, str);
    }

    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (!File.Exists(path)) path = Application.streamingAssetsPath + "/" + fileName + ".json";
        if (!File.Exists(path)) return new T();

        string str = File.ReadAllText(path);
        
        if (type == JsonType.LitJson) return JsonMapper.ToObject<T>(str);
        else return JsonUtility.FromJson<T>(str);
    }
}