using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PanelEvents.OnPanelManagerInitialized += ShowMainScreen;
    }

    private void OnDisable()
    {
        PanelEvents.OnPanelManagerInitialized -= ShowMainScreen;
    }

    private void ShowMainScreen()
    {
       
        PanelManager.Instance.ShowPanel("MenuScreen");
    }
}
