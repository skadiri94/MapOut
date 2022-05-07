using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System;
using UnityEngine.SocialPlatforms;


public class GPGSManager : MonoBehaviour
{
    
    public Text m_Message;
    public Button m_SignIn;


    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    .RequestIdToken()
    .RequestServerAuthCode(false)
    .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        m_SignIn.onClick.RemoveAllListeners();

        m_SignIn.onClick.AddListener(SignInGooglePlayGames);

        SignInGooglePlayGames();
    }



    public void SignInGooglePlayGames()
    {
        string playerName = string.Empty;

   

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            if (result == SignInStatus.Success)
            {
               
                playerName = PlayGamesPlatform.Instance.GetUserDisplayName();
            }

            else m_Message.text = "Login " + result.ToString() + " " + playerName;

            m_SignIn.onClick.AddListener(SignoutGooglePlay);
        });
    }

    private void SignoutGooglePlay()
    {
        PlayGamesPlatform.Instance.SignOut();
        m_Message.text = "User Signed Out";
        SignInGooglePlayGames();
    }

    public void postAchievement(long score)
    {
        if (score > 50)
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_starter_pack, 100.0f, (result) =>
            {
                if (result)
                {
                    Debug.Log("Progress Reported");
                }
                else
                {
                    Debug.LogWarning("Failed to report progress !");
                }
            });

        else if (score > 100)
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_king_pin, 100.0f, (result) =>
            {
                if (result)
                {
                    Debug.Log("Progress Reported");
                }
                else
                {
                    Debug.LogWarning("Failed to report progress !");
                }
            });

        else if (score > 200)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_super_charged, 100.0f, (result) =>
            {
                if (result)
                {
                    Debug.Log("Progress Reported");
                }
                else
                {
                    Debug.LogWarning("Failed to report progress !");
                }
            });

            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_the_explore, 100.0f, (result) =>
            {
                if (result)
                {
                    Debug.Log("Progress Reported");
                }
                else
                {
                    Debug.LogWarning("Failed to report progress !");
                }
            });

        }
    }


    public void ShowDefaultAchievementUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void PostScoreToLeaderboard(long score)
    {
     
        Social.ReportScore(score,GPGSIds.leaderboard_score, (bool success) =>
        {
            if (success)
            {
                m_Message.text = "Score added successfully";
            }
            else
            {
                m_Message.text = "Score not added";
            }
        });
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void QuitGame()
    {
        Application.Quit();

    }

}