using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class QuestionsManager : Singleton<QuestionsManager>
{

    public GameModel Configuration;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HintText;
    public int score = 0;
    public Text hintCount;
    public int hints = 3;
    public Transform IncorrectImage;
    public Transform CorrectImage;
    public Dictionary<string, Texture2D> imgDic;

    public static Action OnNewQuestionLoaded;
    public static Action OnAnswerProvided;
    public QuestionUI Question;
    private GameManager _gameManager;
    public InitAdsScript initAds;
    private string _currentCategory;



    private QuestionModel _currentQuestion;
   

    void Start()
    {
        initAds = new InitAdsScript();

        //imgDic = new Dictionary<string, Texture2D>();
        //string path = Application.dataPath;

        //path += "/Resources/Images";

        //filenames = Directory.GetFiles(path, "*.png");
        //foreach (string filename in filenames)
        //{

        //    string countryCode = ExtractCountryCode(filename);
        //    Texture2D countryImg = Resources.Load<Texture2D>("Images/" + countryCode);
        //    imgDic.Add(countryCode, countryImg);
        //}

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
        _currentQuestion = _gameManager.GetQuestionForCategory(_currentCategory);



        if (_currentQuestion != null)
        {
            Question.PopulateQuestion(_currentQuestion);

        }
        else
            Question.ClearQuestion();

        
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

        if (hints > 0)
        {
            SetHintText(_currentQuestion.Question);

            HintText.gameObject.SetActive(true);
            hints--;
        }
        else
        {
            initAds.requestRewarded();
            initAds.showGoogleRewardedVideo();
        }

    }
    void SetHintText(string text)
    {
        HintText.text = "Hint: "+ text.Substring(7, 2);
    }

}
