using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using Newtonsoft.Json;
using EasyButtons;

public class TestDB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Do();
    }

    void Do()
    {
        string connection = "URI=file:" + @"C:\Users\cc4li\Desktop\History";
        print(connection);
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        //IDbCommand cmnd_read = dbcon.CreateCommand();
        //IDataReader reader;

        //string query = GetQueryFromKeyword(keyword);

        //cmnd_read.CommandText = query;
        //reader = cmnd_read.ExecuteReader();

        //int count = 0;
        //while (reader.Read())
        //    ++count;

        //if (count > 0)
        //{
        //    StatKeywordResult kr = new StatKeywordResult(keyword, count);
        //    searchTermsWeMatch.Add(kr);
        //}
    }
}
