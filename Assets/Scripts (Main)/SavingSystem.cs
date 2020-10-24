using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingSystem <TypeOfFile> where TypeOfFile: class
 
{
  public void SaveAsBinary(TypeOfFile fileType, string nameOfFile)
  {
      if (!Directory.Exists(Application.persistentDataPath + "/SaveFiles")){Directory.CreateDirectory(Application.persistentDataPath + "/SaveFiles");}

      string path = Application.persistentDataPath + "/SaveFiles" + "/" + nameOfFile;
      BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(path)) { File.Delete(path); }
      FileStream file = new FileStream(path,FileMode.Create);
      bf.Serialize(file,fileType);
      file.Close();

  }

  public void SaveAsJson(TypeOfFile fileType, string nameOfFile)
  {
        if (!Directory.Exists(Application.persistentDataPath + "/SaveFiles")) { Directory.CreateDirectory(Application.persistentDataPath + "/SaveFiles"); }
       
        string path = Application.persistentDataPath + "/SaveFiles" + "/" + nameOfFile;
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TypeOfFile));
        FileStream file = new FileStream(path,FileMode.Create);
        jsonSerializer.WriteObject(file,fileType);
        file.Close();
  }

  public TypeOfFile LoadAsBinary(string nameOfFile)
  {
        try {
        string path = Application.persistentDataPath + "/SaveFiles" + "/" + nameOfFile;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(path, FileMode.Open);
     TypeOfFile ret =  bf.Deserialize(file) as TypeOfFile;
     file.Close();
     return ret;
        }
        catch {Debug.Log("No path found");return null;}
     
  }

    public TypeOfFile LoadAsJson(string nameOfFile)
    {
      try {
        string path = Application.persistentDataPath + "/SaveFiles" + "/" + nameOfFile;
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TypeOfFile));
        FileStream file = new FileStream(path, FileMode.Open);
        TypeOfFile ret = jsonSerializer.ReadObject(file) as TypeOfFile;
        file.Close();
        return ret;
      }
      catch {Debug.Log("No path found"); return null; }

    }



}



