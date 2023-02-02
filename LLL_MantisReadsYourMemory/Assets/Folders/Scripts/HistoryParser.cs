using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
 using Newtonsoft.Json;

public class KeywordResult
{
    public string wavFileName;
    public string caption;
    public int instances;
    public string query;

    public KeywordResult(Search search, int count)
    {
        wavFileName = search.wavFileName;
        query = search.query;
        instances = count;
        caption = search.caption;
    }

    public void Print()
    {
        Debug.Log("Keyword Result:\t" + wavFileName + "\t\t" + "Instances: " + instances + "\n" + query);
    }
}

public class HistoryParser
{
    public static List<KeywordResult> GetSearchTermsOfAllBrowsers()
    {
        List<string> historyLocations = HistoryGetter.GetHistories();
        List<KeywordResult> masterKeyWordMatches = new List<KeywordResult>();

        for (int i = 0; i < historyLocations.Count; ++i)
        {
            List<KeywordResult> searchTermsWeMeet = GetMatchedSearchTerms(historyLocations[i]);
            Debug.Log(searchTermsWeMeet);

            //AddToMasterMasterKRList(ref masterKeyWordMatches, ref searchTermsWeMeet);
        }

        return masterKeyWordMatches;
    }

    public static List<KeywordResult> GetMatchedSearchTerms(string path)
    {
        List<KeywordResult> searchTermsWeMatch = new List<KeywordResult>();
        var searchTerms = GetJSONSearchTerms();

        // Open Database
        string connection = "URI=file:" + path;
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        foreach (var searchTerm in searchTerms)
        {
            IDbCommand cmnd_read = dbcon.CreateCommand();
            IDataReader reader;

            string query = searchTerm.query;

            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            int count = 0;
            while (reader.Read())
                ++count;

            if (count > 0)
            {
                KeywordResult kr = new KeywordResult(searchTerm, count);
                searchTermsWeMatch.Add(kr);
            }
        }
        return searchTermsWeMatch;
    }

    static void AddToMasterMasterKRList(ref List<KeywordResult> masterList, ref List<KeywordResult> listToAddToMasterList)
    {
        foreach (var kr in listToAddToMasterList)
        {
            bool exists = false;
            foreach (var mkr in masterList)
            {
                if (kr.wavFileName == mkr.wavFileName)    // if already exists in Master List
                {
                    mkr.instances += kr.instances;
                    exists = true;
                    break;
                }
            }
            if (!exists)
                masterList.Add(kr);
        }
    }
    

    static List<string> GetQueryStringList()
    {
        List<string> queryStrings = new List<string>();

        var searchTerms = GetJSONSearchTerms();
        foreach (var searchTerm in searchTerms)
            queryStrings.Add(GetQueryString(searchTerm));
        
        return queryStrings;
    }


    public static string GetQueryString(Search search)
    {
        string baseQuery = "SELECT * FROM urls WHERE title LIKE ";
        var keywords = search.keywords;
        string query = baseQuery;

        query += "'%" + keywords[0] + "%'";

        for (int i = 1; i < keywords.Count; ++i)
        {
            query += " AND title like ";
            query += "'%" + keywords[i] + "%'";
        }
        return query;
    }

    public static List<Search> GetJSONSearchTerms()
    {
        var sr = new StreamReader(Application.streamingAssetsPath + "/" + "SearchTerms.json");
        HistorySearchTermsJson myDeserializedClass = JsonConvert.DeserializeObject<HistorySearchTermsJson>(sr.ReadToEnd());
        sr.Close();
        return myDeserializedClass.Searches;
    }
}
