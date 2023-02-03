using System.Collections.Generic;
using UnityEngine;
using System.IO;

 using Newtonsoft.Json;

public class GamesFinder
{
    static string steamDir = @"C:\Program Files (x86)\Steam\steamapps\common\";
    static string epicDir = @"C:\Program Files\Epic Games\";
    static string ubisoftDir = @"C:\Program Files (x86)\Ubisoft\Ubisoft Game Launcher\games\";

    public static List<GameItem> GetGamesInstalled()
    {
        List<GameItem> gamesInstalled = new List<GameItem>();
        var games = GetGamesList();

        foreach (var game in games)
        {
            if (CheckIfGameExists(game.gameDir))
                gamesInstalled.Add(game);
        }
        return gamesInstalled;
    }

    public static List<GameItem> GetGamesList()
    {
        var sr = new StreamReader(Application.streamingAssetsPath + "/" + "games.json");
        JsonGamesClass myDeserializedClass = JsonConvert.DeserializeObject<JsonGamesClass>(sr.ReadToEnd());
        sr.Close();
        return myDeserializedClass.games;
    }

    static bool CheckIfGameExists(string game)
    {
        if (Directory.Exists(steamDir + game))
            return true;

        if (Directory.Exists(ubisoftDir + game))
            return true;

        if (Directory.Exists(epicDir + game))
            return true;

        if (Directory.Exists(game))
            return true;

        return false;
    }
}
