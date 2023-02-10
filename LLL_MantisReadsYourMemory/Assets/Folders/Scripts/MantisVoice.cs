using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// using EasyButtons;

//Gets assosiated audio clips and plays them
public class MantisVoice : MonoBehaviour
{
    [SerializeField] AudioSource audSrc;
    [SerializeField] UI_InstancesPanel instancesPanel;

    List<AudioClip> historyAudClips = new List<AudioClip>();

    void OnEnable()
    {
        Invoke("PlayVoiceClips", 2.5f);
    }

    // [Button]
    void PlayVoiceClips()
    {
        // play all voice clips (main part of the whole "game")
        StartCoroutine(VoiceAllClips());
    }

    IEnumerator VoiceAllClips()
    {
        if (!WeHaveAtLeastOneGameMatch() && !WeHaveAtLeastOneHistoryMatch())
        {
            yield return StartCoroutine(PlayAudClip(GetAudioFromPath("WeFoundNothing")));
            Application.OpenURL("https://www.youtube.com/channel/UCYwRPV5Wi6C00HE3Yl2Qlqg");
            Application.Quit();
            yield break;
        }

        //yield return StartCoroutine(PlayAudClip(GetAudioFromPath("intro")));

        //yield return StartCoroutine(PlayGameClips());

        //yield return StartCoroutine(PlayAudClip(GetAudioFromPath("MiddleSegway")));

        yield return StartCoroutine(PlayHistoryClips());

        yield return new WaitForSecondsRealtime(3.0f);

        yield return StartCoroutine(PlayAudClip(GetAudioFromPath("outro")));

        Application.OpenURL("https://www.youtube.com/channel/UCYwRPV5Wi6C00HE3Yl2Qlqg");
        Application.Quit();
    }

    // [Button]
    void StartHistoryClipsCourotine()
    {
        StartCoroutine(PlayHistoryClips());
    }

    IEnumerator PlayHistoryClips()
    {
        List<KeywordResult> searchesMatched = HistoryParser.GetSearchTermsOfAllBrowsers();
        searchesMatched.Shuffle();

        if (searchesMatched.Count == 0)
        {
            yield return StartCoroutine(PlayAudClip(GetAudioFromPath("NoHistory")));    // Wow your browser history is clean! Too clean.
        }

        foreach (var kr in searchesMatched)  // Play History aud Clips
        {
            AudioClip audClip = GetAudioFromPath(kr.wavFileName);
            if (!audClip)
            {
                print("MISSING AUDIO CLIP: " + kr.wavFileName);
                continue;
            }
            instancesPanel.SetTexts(kr.caption, kr.instances);
            instancesPanel.Animate(0.3f, audClip.length * .9f);
            yield return StartCoroutine(PlayAudClip(audClip));
        }
    }

    bool WeHaveAtLeastOneHistoryMatch()
    {
        if (HistoryParser.GetSearchTermsOfAllBrowsers().Count == 0)
        {
            return false;
        }
        return true;
    }

    bool WeHaveAtLeastOneGameMatch()
    {
        if (GetAllGameAudioClips().Count == 0)
        {
            return false;
        }
        return true;
    }

    IEnumerator PlayGameClips()
    {
        List<AudioClip> gameAudClips = GetAllGameAudioClips();
        gameAudClips.Shuffle();

        if (gameAudClips.Count == 0)
        {
            yield return StartCoroutine(PlayAudClip(GetAudioFromPath("NoGames")));
        }

        foreach (var audClip in gameAudClips)  // Play History aud Clips
        {
            yield return StartCoroutine(PlayAudClip(audClip));
        }
    }

    IEnumerator PlayAudClip(AudioClip audClip)
    {
        if (audClip)
        {
            audSrc.clip = audClip;
            audSrc.Play();
            yield return new WaitWhile(() => audSrc.isPlaying);
            yield return new WaitForSeconds(0.5f);
        }
    }

    AudioClip GetAudioFromPath(string audName)   //without the .wav
    {
        string wavPath = Application.streamingAssetsPath + "/" + audName + ".wav";

        if (File.Exists(wavPath))
        {
            AudioClip audClip = new WWW(wavPath).GetAudioClip(false, true, AudioType.WAV);
            return audClip;
        }
        else
        {
            print("MISSING AUDIO CLIP: " + audName);
            return null;
        }
    }

    // [Button]
    void GetAllHistoryAudioClips()
    {
        List<KeywordResult> searchesMatched = new List<KeywordResult>();

        for (int i = 0; i < searchesMatched.Count; ++i)
        {
            string wavPath = Application.streamingAssetsPath + "/" + searchesMatched[i].wavFileName + ".wav";

            if (File.Exists(wavPath))
            {
                AudioClip audClip = new WWW(wavPath).GetAudioClip(false, true, AudioType.WAV);
                historyAudClips.Add(audClip);
            }
            else
            {
                print("MISSING AUDIO CLIP: " + searchesMatched[i].wavFileName);
            }
        }
    }

    // [Button]
    List<AudioClip> GetAllGameAudioClips()
    {
        List<AudioClip> gameAudClips = new List<AudioClip>();
        List<GameItem> gamesInstalled = GamesFinder.GetGamesInstalled();

        for (int i = 0; i < gamesInstalled.Count; ++i)
        {
            string wavPath = Application.streamingAssetsPath + "/" + gamesInstalled[i].wavName + ".wav";

            if (File.Exists(wavPath))
            {
                AudioClip audClip = new WWW(wavPath).GetAudioClip(false, true, AudioType.WAV);
                gameAudClips.Add(audClip);
            }
            else
            {
                print("MISSING AUDIO CLIP: " + gamesInstalled[i]);
            } 
        }
        return gameAudClips;
    }
}
