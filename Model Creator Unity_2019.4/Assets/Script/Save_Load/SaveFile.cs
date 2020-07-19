using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveFile : MonoBehaviour
{
    private string saveFileName;

    public void OnClickFile_1()
    {
        saveFileName = System.DateTime.Now.ToString("MM/dd/yyyy \nHH:mm:ss");

        StoreSaveFileData(new SaveFilesData(true, saveFileName), ("/file1Data.save"));

        string path = Application.persistentDataPath + ("/saveFile1.save");
        StoreModelData(path);
    }
    public void OnClickFile_2()
    {
        saveFileName = System.DateTime.Now.ToString("MM/dd/yyyy \nHH:mm:ss");

        StoreSaveFileData(new SaveFilesData(true, saveFileName), ("/file2Data.save"));

        string path = Application.persistentDataPath + ("/saveFile2.save");
        StoreModelData(path);
    }
    public void OnClickFile_3()
    {
        saveFileName = System.DateTime.Now.ToString("MM/dd/yyyy \nHH:mm:ss");

        StoreSaveFileData(new SaveFilesData(true, saveFileName), ("/file3Data.save"));

        string path = Application.persistentDataPath + ("/saveFile3.save");
        StoreModelData(path);
    }
    public void OnClickFile_4()
    {
        saveFileName = System.DateTime.Now.ToString("MM/dd/yyyy \nHH:mm:ss");

        StoreSaveFileData(new SaveFilesData(true, saveFileName), ("/file4Data.save"));

        string path = Application.persistentDataPath + ("/saveFile4.save");
        StoreModelData(path);
    }
    public void OnClickFile_5()
    {
        saveFileName = System.DateTime.Now.ToString("MM/dd/yyyy \nHH:mm:ss");

        StoreSaveFileData(new SaveFilesData(true, saveFileName), ("/file5Data.save"));

        string path = Application.persistentDataPath + ("/saveFile5.save");
        StoreModelData(path);
    }
    public void OnClickFile_6()
    {
        saveFileName = System.DateTime.Now.ToString("MM/dd/yyyy \nHH:mm:ss");

        StoreSaveFileData(new SaveFilesData(true, saveFileName), ("/file6Data.save"));

        string path = Application.persistentDataPath + ("/saveFile6.save");
        StoreModelData(path);
    }

    public void SetFileName(TMPro.TextMeshProUGUI textBox)
    {
        textBox.text = saveFileName;
    }

    private void StoreModelData(string path)
    {
        var formatter = new BinaryFormatter();
        var writeStream = new FileStream(path, FileMode.Create);
        var binaryTemplate = new BinaryTemplate(Camera.main);

        formatter.Serialize(writeStream, binaryTemplate);
        writeStream.Close();
    }

    private void StoreSaveFileData(SaveFilesData fileData, string subPath)
    {
        string path = Application.persistentDataPath + subPath;
        
        var formatter = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, fileData);
        stream.Close();

    }

}
