using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Mgr_Data
{
    string _savePath { get { return Application.persistentDataPath + "/Save.txt"; } } 
    string _settingPath { get { return Application.persistentDataPath + "/Setting.txt"; } }
    byte[] save;
    byte[] setting;
    public void ReadSave(Save_Game save)
    {
        if (!File.Exists(_savePath))
            CreateNewSaveFile();   
        byte[] buffer = File.ReadAllBytes(_savePath);
        //int count = 0;
        //while (count < buffer.Length)
        //{
        //    ushort header = BitConverter.ToUInt16(buffer, count);
        //    if (header <= 2) return;
        //    count += sizeof(ushort);
        //    int score = BitConverter.ToInt32(buffer, count);
        //    count += sizeof(int);
        //    string name = Encoding.Unicode.GetString(buffer, count,header - count);
        //    count = header;
        //    save.Record.Add(new Save_Game.RecordFormat(score, name));
        //}
        for(int i = 0; i < 10; i++)
        {
            int offset = i * 36;
            int count = 0;
            ushort header = BitConverter.ToUInt16(buffer, offset + count);
            if (header <= 2) return;
            count += sizeof(ushort);
            int score = BitConverter.ToInt32(buffer, offset + count);
            count += sizeof(int);
            string name = Encoding.Unicode.GetString(buffer, count, offset + header - count);
            save.Record.Add(new Save_Game.RecordFormat(score, name));
        }
    }
    public void ReadSetting(ref Save_Setting format)
    {
        if (!File.Exists(_settingPath))
            CreateNewSettingFile();
        setting = File.ReadAllBytes(_settingPath);
        
        format.Sound = BitConverter.ToSingle(setting,0);
    }
    public void SaveSetting(Save_Setting format)
    {
        byte[] sound = BitConverter.GetBytes(format.Sound);
        int soundL = sound.Length;
        byte[] setting = new byte[soundL];
        int count = 0;
        int i = 0;
        for (i = 0; i < soundL; i++)
            setting[count + i] = sound[i];
        count += i;
        File.WriteAllBytes(_settingPath, setting);
    }
    void CreateNewSaveFile()
    {
        save = new byte[2] { 0b_0000_0010, 0b_0000_0000 };
        File.WriteAllBytes(_savePath, save);
    }
    void CreateNewSettingFile()
    {
        var v = BitConverter.GetBytes(0.5f);
        setting = v;
        File.WriteAllBytes(_settingPath,setting);
    }
    public void Save(Save_Game save)
    {
        byte[] newSave = new byte[360];
        int i;
        //foreach (var s in save.Record)
        //{
        //    byte[] byte_name = Encoding.Unicode.GetBytes(s.Name);
        //    byte[] byte_score = BitConverter.GetBytes(s.Score);
        //    byte[] header = BitConverter.GetBytes((ushort)(byte_name.Length + byte_score.Length));
        //    int nameL = byte_name.Length;
        //    int scoreL = byte_score.Length;
        //    int headerL = header.Length;
        //    for (i = 0; i < headerL; i++)
        //        newSave[count + i] = header[i];
        //    count += i;
        //    for (i = 0; i < scoreL; i++)
        //        newSave[count + i] = byte_score[i];
        //    count += i;
        //    for (i = 0; count < nameL; count++)
        //        newSave[count + i] = byte_name[i];
        //    count += i;
        //}
        for(int j = 0; j < save.Record.Count; j++)
        {
            int offset = j * 36;
            int count = 0;
            byte[] byte_name = Encoding.Unicode.GetBytes(save.Record[j].Name);
            byte[] byte_score = BitConverter.GetBytes(save.Record[j].Score);
            byte[] header = BitConverter.GetBytes((ushort)(byte_name.Length + byte_score.Length));
            int nameL = byte_name.Length;
            int scoreL = byte_score.Length;
            int headerL = header.Length;
            for (i = 0; i < headerL; i++)
                newSave[count + offset + i] = header[i];
            count += i;
            for (i = 0; i < scoreL; i++)
                newSave[count + offset + i] = byte_score[i];
            count += i;
            for (i = 0; count < nameL; count++)
                newSave[count + offset + i] = byte_name[i];
            count += i;
        }

        File.WriteAllBytes(_savePath, newSave);
    }
    
}
