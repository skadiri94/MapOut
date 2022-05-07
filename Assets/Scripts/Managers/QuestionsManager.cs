using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionsManager : Singleton<QuestionsManager>
{

    public GameModel Configuration;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HintText;
    public GameObject currentScreen;
    public  long score = 0;
    public Text hintCount;
    bool hintNotUsed = true;
    private int hints;
    public Transform IncorrectImage;
    public Transform CorrectImage;
    public Dictionary<string, Texture2D> imgDic;

    public static Action OnNewQuestionLoaded;
    public static Action OnAnswerProvided;
    public QuestionUI Question;
    private GameManager _gameManager;
    //public InitAdsScript initAds;
    private string _currentCategory;



    private QuestionModel _currentQuestion;

    public UnityInterstitialAdsScript UnityAds;

    void Start()
    {
        hints = 1;

        //Cache a reference
        _gameManager = GameManager.Instance;

        _currentCategory = _gameManager.GetCurrentCategory();

        LoadNextQuestion();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        hintCount.text = hints.ToString();
        HintText.text = HintText.text;
    

    }

    public void LoadNextQuestion()
    {
        hintNotUsed = true;
        _currentQuestion = _gameManager.GetQuestionForCategory(_currentCategory);



        if (_currentQuestion != null)
        {
            Question.PopulateQuestion(_currentQuestion);

        }
        else
        {
            
            GameOver();
        }

        
        OnNewQuestionLoaded?.Invoke();
    }

    public bool AnswerQuestion(int answerIndex)
    {
        HintText.gameObject.SetActive(false);
        OnAnswerProvided?.Invoke();

        bool isCorrect = _currentQuestion.CorrectAnswerIndex == answerIndex;
        if (isCorrect)
        {
           
            score += 10;
            TweenResult(CorrectImage);
        }
        else
        {
            if (score - 5 > 0)
                score -= 5;

            TweenResult(IncorrectImage);

            GameOver();
        }

        
        return isCorrect;
    }

    void TweenResult(Transform resultTransform)
    {
        Sequence result = DOTween.Sequence();
        result.Append(resultTransform.DOScale(1, .5f).SetEase(Ease.OutBack));
        result.AppendInterval(1f);
        result.Append(resultTransform.DOScale(0, .2f).SetEase(Ease.Linear));
        result.AppendCallback(LoadNextQuestion);
    }

    public void ShowHint()
    {
        if (hintNotUsed)
        {

            if (hints > 0)
            {
                SetHintText(_currentQuestion.Question);

                HintText.gameObject.SetActive(true);
                hints--;
                hintNotUsed = false;
            }
            else
            {
                
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    InitAdsScript.Instance.showGoogleRewardedVideo();
                }
                else
                {
                    UnityRewardedAdsScript.FindObjectOfType<UnityRewardedAdsScript>().ShowAd();
                }


            }
        }

    }
    public void BackToMenu()
    {
        //PanelManager.Instance.HideLastPanel();
        currentScreen.SetActive(false);
        PanelManager.Instance.ShowPanel("MenuScreen");
    }

    public void GameOver()
    {
        GPGSManager.FindObjectOfType<GPGSManager>().PostScoreToLeaderboard(score);
        GPGSManager.FindObjectOfType<GPGSManager>().postAchievement(score);
        currentScreen.SetActive(false);
       PanelManager.Instance.ShowPanel("GameOverScreen");
    }
    void SetHintText(string text)
    {
        HintText.text = "Hint: "+ text.Substring(7, 2);
    }
    public void setHints(int val)
    {
        this.hints =+ val;
    }

}
