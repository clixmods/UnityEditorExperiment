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
public class DataPersistentHandler : MonoBehaviour
{
    [Tooltip("Contains all scriptableObject compatible with DataPersistentSystem, required to be referenced here.")]
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
            if ( interfacesOnObject.Contains(typeof(ISave)) )
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

    [ContextMenu("Save")]
    public void SaveAll()
    {
        var instanceSaveable = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveInstance>();
        // Instance
        foreach (ISaveInstance saveInstance in instanceSaveable) 
        {
            Save(saveInstance, saveInstance.SaveID.ToString());
        }
        // Asset
        foreach (var dataPersistent in scriptableObjectSaveables)
        {
            Save(dataPersistent, dataPersistent.name);
        }
    }
    [ContextMenu("Load All")]
    public void LoadAll()
    {
        var instancesSaveable = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveInstance>();
        // Instance
        foreach (var instanceSaveable in instancesSaveable)
        {
            Load(instanceSaveable, instanceSaveable.SaveID.ToString());
        }
        // Asset
        foreach (var dataPersistent in scriptableObjectSaveables)
        {
            Load(dataPersistent, dataPersistent.name);
        }
    }
    private static void Save(ISave save, string fileName)
    {
        save.OnSave(out var gamedata);
        string dataPath = Path.Combine(Application.persistentDataPath, ("data/" +fileName ));
        string jsonData = JsonUtility.ToJson(gamedata, true);
        byte[] byteData;
        byteData = Encoding.ASCII.GetBytes(jsonData);
        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));
        }
        File.WriteAllBytes(dataPath, byteData);
        Debug.Log("Save data to: " + dataPath);
    }
    private static void Load(ISave save, string fileName)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, ("data/" + fileName));
        if (!Directory.Exists(Path.GetDirectoryName(dataPath)))
        {
            Debug.LogWarning("File or path does not exist! " + dataPath);
            return;
        }
        byte[] jsonDataAsBytes = null;
        if (File.Exists(dataPath))
        {
            jsonDataAsBytes = File.ReadAllBytes(dataPath);
            Debug.Log("<color=green>Loaded all data from: </color>" + dataPath);
            string jsonData;
            jsonData = Encoding.ASCII.GetString(jsonDataAsBytes);
            Debug.Log(jsonData);
            save.OnLoad(jsonData);
        }
    }
}