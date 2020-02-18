using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Save Load Manager", fileName = "SaveLoadManager")]
public class SaveLoadManager : ScriptableObject {

    public SaveDataStruct saveData;

    [Serializable]
    public struct SaveDataStruct {
        public int currentLevel;
        public int currentLeveltest;

    }

    string path;

    public void SetPath() {
        path = Application.dataPath + "/Savefile.xml";

    }

    public void Save() {
        SetPath();

        XmlSerializer serializer = new XmlSerializer(typeof(SaveDataStruct));

        using (FileStream stream = new FileStream(path, FileMode.Create)) {
            serializer.Serialize(stream, saveData);
        }

    }

    public void Load() {
        SetPath();

        XmlSerializer serializer = new XmlSerializer(typeof(SaveDataStruct));

        using (FileStream stream = new FileStream(path, FileMode.Open)) {
            saveData = (SaveDataStruct)serializer.Deserialize(stream);
            
        }

        Debug.Log(saveData.currentLevel);
        Debug.Log(saveData.currentLeveltest);

    }

}
