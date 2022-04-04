using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Save_Game 
{
    public List<RecordFormat> Record = new List<RecordFormat>();
    public struct RecordFormat
    {
        public ushort Header { get; }
        public string Name { get; }
        public int Score { get; }

        public RecordFormat(int score, string name)
        {
            Score = score;
            Name = name;
           
            Header = (ushort)(sizeof(int) + Encoding.Unicode.GetBytes(name).Length + sizeof(ushort));
        }
    }
    public int GetHighScore()
    {
        if (Record.Count <= 0)
            return 0;
        
        return Record.Max(element => element.Score);
    }
    public bool IsNewRecord(int score)
    {
        return Record.Count < 10 || Record.Min(x => x.Score) < score;
    }
    public void AddNewRecord(RecordFormat format)
    {
        Record.Add(format);
        Record.Sort( (x, y)  => x.Score > y.Score ? x.Score : y.Score);
        while (Record.Count > 10)
            Record.RemoveAt(10);
    }
}
