using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuestionUI : MonoBehaviour
{
    
    
    private void Start()
    {
        
        
    }


    public RawImage QuestionImage;
    public Texture2D img;
    public TextMeshProUGUI Answer1Label;

    public TextMeshProUGUI Answer2Label;

    public TextMeshProUGUI Answer3Label;

    public TextMeshProUGUI Answer4Label;

    public void PopulateQuestion(QuestionModel questionModel)
    {
        print("@:: "+ questionModel.Question);
        img = Resources.Load<Texture2D>(questionModel.Question);
        QuestionImage.texture = img;
        Answer1Label.text = questionModel.Answer1;
        Answer2Label.text = questionModel.Answer2;
        Answer3Label.text = questionModel.Answer3;
        Answer4Label.text = questionModel.Answer4;
    }

    public void ClearQuestion()
    {
        SceneManager.LoadScene("Main");

    }

}
