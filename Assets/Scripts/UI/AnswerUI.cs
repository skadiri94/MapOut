using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class AnswerUI : MonoBehaviour
{
    public Image CorrectImg;
    public Image IncorrectImg;
    public int AnswerIndex;
    private bool _canBeClicked = true;

    void OnEnable()
    {
      
        QuestionsManager.OnNewQuestionLoaded += ResetValues;
        QuestionsManager.OnAnswerProvided += AnswerProvided;

    }

    void OnDisable()
    {
  
        QuestionsManager.OnNewQuestionLoaded -= ResetValues;
        QuestionsManager.OnAnswerProvided -= AnswerProvided;
    }

    public void OnAnswerClicked()
    {
        if (_canBeClicked)
        {

            bool result = QuestionsManager.Instance.AnswerQuestion(AnswerIndex);
            if (result)
            {
              
                CorrectImg.DOFade(1, 0.5f);

            }
            else
            {
                IncorrectImg.DOFade(1, 0.5f);

            }

            
        }

    }

    void AnswerProvided()
    {
        _canBeClicked = false;
   
    }

   public void ResetValues()
    {
      
        CorrectImg.DOFade(0, 0.5f);
        IncorrectImg.DOFade(0, 0.5f);

        _canBeClicked = true;
    }
}
