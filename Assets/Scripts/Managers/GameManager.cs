using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    public GameModel Configuration;
    private string _currentCategory;
    string[] filenames;
    public Dictionary<string, Texture2D> imgDic;
    public List<string> CountryList;


    private List<string> _pickCountries = new List<string>();
    private List<int> _askedQuestionIndex = new List<int>();
    private List<QuestionModel> qList = new List<QuestionModel>(); 
    private System.Random random;

    private void Start()
    {
       
        LoadCountries();
        pick4Countries();
        _currentCategory = "Europe";
    

        imgDic = new Dictionary<string, Texture2D>();
        string path = Application.dataPath;

        path += "/Resources/Images";

        filenames = Directory.GetFiles(path, "*.png");
        foreach (string filename in filenames)
        {

            string countryCode = ExtractCountryCode(filename);
            Texture2D countryImg = Resources.Load<Texture2D>("Images/" + countryCode);
            imgDic.Add(countryCode, countryImg);
        }

        init_Questions();
    }

    public QuestionModel GetQuestionForCategory(string categoryName)
    {
     
        CategoryModel categoryModel = Configuration.Categories.FirstOrDefault(category => category.CategoryName == categoryName);
        if (categoryModel != null )
        {

            int randomIndex = Random.Range(0, categoryModel.Questions.Count);
            while (categoryModel.Questions.Count > _askedQuestionIndex.Count && _askedQuestionIndex.Contains(randomIndex))
                randomIndex = Random.Range(1, categoryModel.Questions.Count);


          
            
            if (_askedQuestionIndex.Count > categoryModel.Questions.Count)
                return null;

            _askedQuestionIndex.Add(randomIndex);
            return categoryModel.Questions[randomIndex];

        }
     
        
        return null;
    }
    public void init_Questions()
    {
        CategoryModel cm = new CategoryModel
        {
            CategoryName = "Europe",
            Questions = qList
        };
        _askedQuestionIndex.Clear();
        Configuration.Categories.Clear();
        Configuration.Categories.Add(cm);

    }


    void LoadCountries()
    {
        TextAsset file = Resources.Load<TextAsset>("Countries");
        List<string> lines = new List<string>(file.text.Split('\n'));
        for (int i = 0; i < lines.Count-1; i++)
        {
            
                CountryList.Add(lines[i]);

        }
      
    }

    void pick4Countries()
    {
      
        
        //int index;
        List<int> pickedCountries = new List<int>();
        for (int j = 0; j < 10; j++)
        {
            List<string> randomFour = new List<string>();

            for (int i = 0; i < 4; i++)
            {

               int index = Random.Range(0, CountryList.Count);
                while (CountryList.Count > pickedCountries.Count && pickedCountries.Contains(index))
                    index = Random.Range(1, CountryList.Count);


                pickedCountries.Add(index);
                randomFour.Add(CountryList[index]);


            }

            int randomIndex = Random.Range(0, randomFour.Count);

            qList.Add(new QuestionModel
            {
                Question = "Images/" + randomFour[randomIndex].Substring(0, 2),
                Answer1 = randomFour[0].Substring(3, randomFour[0].Length - 3),
                Answer2 = randomFour[1].Substring(3, randomFour[1].Length - 3),
                Answer3 = randomFour[2].Substring(3, randomFour[2].Length - 3),
                Answer4 = randomFour[3].Substring(3, randomFour[3].Length - 3),
                CorrectAnswerIndex = randomIndex+1
            });
        }
    }


    public void SetCurrentCategory(string categoryName)
    {

        _currentCategory = categoryName;
    }

    public string GetCurrentCategory()
    {
        return _currentCategory;
    }

    private string ExtractCountryCode(string filename)
    {

        return filename.Substring(filename.Length - 6, 2);
    }
}
