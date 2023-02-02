using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using System;
// using EasyButtons;

public class HistoryGetter
{
    public static string[] historyCopyLocs = {
        Application.streamingAssetsPath + "/Histories/" + "ChromeHistory",
        Application.streamingAssetsPath + "/Histories/" + "BraveHistory",
        Application.streamingAssetsPath + "/Histories/" + "operaHistory",
        Application.streamingAssetsPath + "/Histories/" + "edgeHistory"
    };

    public static string[] historyDefaultLocs = {
        @"C:\Users\" + GetUsername() + @"\AppData\Local\Google\Chrome\User Data\Default\History",
        @"C:\Users\" + GetUsername() + @"\AppData\Local\BraveSoftware\Brave-Browser\User Data\Default\History",
        @"C:\Users\" + GetUsername() + @"\AppData\Roaming\Opera Software\Opera GX Stable\History",
        @"C:\Users\" + GetUsername() + @"\AppData\Local\Microsoft\Edge\User Data\Default\History",
        //@"C:\Users\" + GetUsername() + @"\AppData\Local\Microsoft\Edge\User Data\Default\History",
    };

    public static List<string> historyCopies;

    static string GetUsername()
    {
        return Environment.UserName;
    }

    public static List<string> GetHistories()  // copies history files that exists and returns the string of dirs
    {
        historyCopies = new List<string>();

        //deletes history files if they exist in Streaming Assets:
        foreach (string path in historyCopyLocs)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        // Copy history files to streaming assets and add path to list
        for (int i = 0; i < historyDefaultLocs.Length; ++i)
        {
            if (System.IO.File.Exists(historyDefaultLocs[i]))
            {
                CopyHistoryFile(historyDefaultLocs[i], historyCopyLocs[i]);
                historyCopies.Add(historyCopyLocs[i]);
            }
        }
        return historyCopies;
    }

    public static void CopyHistoryFile(string from, string to)
    {
        string copyPath = Application.streamingAssetsPath + "/Histories/";
        if (!Directory.Exists(copyPath))
            Directory.CreateDirectory(copyPath);

        try
        {
            File.Copy(from, to, true);
        }
        catch (IOException iox)
        {
            Debug.Log(iox.Message);
        }
    }
}
