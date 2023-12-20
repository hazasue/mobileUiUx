using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class JsonManager
{
    public static string JSON_FILENAME_TRAVEL = "DataTable_Travel";
    public static string JSON_FILENAME_RECOMMEND_PLAN = "DataTable_PlanRecommend";
    public static string JSON_FILENAME_PLAN = "DataTable_Plan";

    public static void CreateJsonFile(string fileName, object obj)
    {
        // 데이터 폴더가 없다면 생성하기
        if (!File.Exists(Application.dataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/");
        }

        FileStream fileStream =
            new FileStream(Application.dataPath + "/Data/" + fileName + ".json", FileMode.OpenOrCreate);
        byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        fileStream.SetLength(0);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public static T LoadJsonFile<T>(string fileName)
    {
        if (!File.Exists(Application.dataPath + "/Data/" + fileName + ".json"))
        {
            Debug.Log(Application.dataPath + "/Data/" + fileName + ".json" + ":  Does not exist.");
            return default(T);
        }

        FileStream fileStream = new FileStream(Application.dataPath + "/Data/" + fileName + ".json", FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
