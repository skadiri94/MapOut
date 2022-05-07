using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + QuestionsManager.Instance.score;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
