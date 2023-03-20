using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using _2DGame.Scripts.Save;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class DataPersistentManager : MonoBehaviour
{
    public List<ScriptableObjectSaveable> scriptableObjectSaveables;

#if UNITY_EDITOR
    private void OnValidate()
    {
        ScriptableObject[] scriptableObjects = GetAssets<ScriptableObject>();
        scriptableObjectSaveables = new List<ScriptableObjectSaveable>();
        for (int i = 0; i < scriptableObjects.Length; i++)
        {
            if (scriptableObjects[i] == null) continue;
 
            var interfacesOnObject = scriptableObjects[i].GetType().GetInterfaces();
            if ( interfacesOnObject.Contains(typeof(ISaveData)) )
            {
                Debug.Log($"Component to save found in {scriptableObjects[i].name}");
                scriptableObjectSaveables.Add((ScriptableObjectSaveable)scriptableObjects[i]);
            }
        }

        

    }
    public static T[] GetAssets<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        int count = guids.Length;
        T[] a = new T[count];
        for (int i = 0; i < count; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }
#endif
    private void Awake()
    {
        LoadAll();
    }
    
    private void OnDestroy()
    {
        SaveAll();
    }

    [ContextMenu("Save")]
    private void SaveAll()
    {
        foreach (var dataPersistent in scriptableObjectSaveables)
        {
            dataPersistent.OnSave(out var gamedata);
            // string dataStr =  JsonUtility.ToJson(gamedata);
            // get the data path of this save data
            string dataPath = Path.Combine(Application.persistentDataPath, ("data/" + dataPersistent.name));
            string jsonData = JsonUtility.ToJson(gamedata, true);
            byte[] byteData;
        
            byteData = Encoding.ASCII.GetBytes(jsonData);

            // create the file in the path if it doesn't exist
            // if the file path or name does not exist, return the default SO
            if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
            }
            File.WriteAllBytes(dataPath, byteData);
            Debug.Log("Save data to: " + dataPath);

        }
    }
    
    [ContextMenu("Load All")]
    private void LoadAll()
    {
        foreach (var dataPersistent in scriptableObjectSaveables)
        {
            // get the data path of this save data
            string dataPath = Path.Combine(Application.persistentDataPath, ("data/" + dataPersistent.name));

            // if the file path or name does not exist, return the default SO
            if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
            {
                Debug.LogWarning("File or path does not exist! " + dataPath);
                continue;
            }

            // load in the save data as byte array
            byte[] jsonDataAsBytes = null;
            
              jsonDataAsBytes = File.ReadAllBytes(dataPath);
               Debug.Log("<color=green>Loaded all data from: </color>" + dataPath);
                
            // convert the byte array to json
            string jsonData;

            // convert the byte array to json
            jsonData = Encoding.ASCII.GetString(jsonDataAsBytes);
            Debug.Log(jsonData);

            // convert to the specified object type
            GameData returnedData = JsonUtility.FromJson<GameData>(jsonData);
            
            dataPersistent.OnLoad(jsonData);
        }
    }
}