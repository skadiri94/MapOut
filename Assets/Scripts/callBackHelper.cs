using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callBackHelper : MonoBehaviour
{

    AnswerUI master;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void iam(AnswerUI answerUI)
    {
        master = answerUI;
    }


    private void OnDisable()
    {
        //master.IveBeenDisabled(this);
    }
}
