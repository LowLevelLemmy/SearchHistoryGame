using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Internet Searches:

// HistorySearchTermsJson myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class Search
{
    public string wavFileName { get; set; }
    public string caption { get; set; }
    public List<string> keywords { get; set; }
    public string query => HistoryParser.GetQueryString(this);
}

public class HistorySearchTermsJson
{
    public List<Search> Searches { get; set; }
}


//Games
public class JsonGamesClass
{
    public List<string> games { get; set; }
}


//Games
public class JsonStatKeywordsClass
{
    public List<string> StatKeywords { get; set; }
}