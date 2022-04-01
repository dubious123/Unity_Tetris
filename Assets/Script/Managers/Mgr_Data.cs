using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Mgr_Data
{
    string _savePath { get { return Application.persistentDataPath + "/Save.txt"; } } 
    byte[] saveFile;
    public void ReadSave(ref int highScore)
    {
        if (!File.Exists(_savePath))
            CreateNewSaveFile();
        saveFile = File.ReadAllBytes(_savePath);
        highScore = BitConverter.ToInt32(saveFile, 2);
    }
    void CreateNewSaveFile()
    {
        saveFile = new byte[1] { 0b_0000_0000 };
        File.WriteAllBytes(_savePath, saveFile);
    }
    public void Save(string name, int HighScore)
    {
        byte[] byte_name = Encoding.Unicode.GetBytes(name);
        byte[] byte_score = BitConverter.GetBytes(HighScore);
        byte[] header = BitConverter.GetBytes((ushort)(byte_name.Length + byte_score.Length));
        int nameL = byte_name.Length;
        int scoreL = byte_score.Length;
        int headerL = header.Length;
        byte[] save = new byte[nameL + scoreL + headerL];
        int count = 0;
        int i;
        for (i = 0; i < headerL; i++)
            save[i] = header[i];
        count += i;
        for (i = 0; i < scoreL; i++)
            save[count + i] = byte_score[i];
        count += i;
        for (i = 0; count < nameL; count++)
            save[count + i] = byte_name[i];

        File.WriteAllBytes(_savePath, save);
    }
}
