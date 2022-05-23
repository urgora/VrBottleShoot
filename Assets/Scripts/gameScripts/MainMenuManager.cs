using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Assets.SimpleLocalization;

public class MainMenuManager : MonoBehaviour
{
    #region  MAINSCENEBLOCK
    /// <summary>
    /// Main Scene UI System
    /// </summary>
    private Stack<GameObject> activePanels;
    private Stack<GameObject> inactivePanels;
    private int lastactivepanel = 0;
    //[Tooltip("OrderOfPanels : MainMenu, Career, Inventory, Store, Options, Exit")]
    [Header("UI Panels")]
    public List<GameObject> _panel;
    public TextMeshProUGUI _subHeader;
    public TextMeshProUGUI _totalCoins;
    public GameObject _backBtn,Avatar;

    public GameObject LoadingScreen;
    public AudioClip ButtonClick;

    private void Awake()
    {
        activePanels = new Stack<GameObject>();
        inactivePanels = new Stack<GameObject>();
        ActivatePanel(0);
        _panel[4] = StoreManager._instance.canvas;
        _panel[4].SetActive(false);
        UpdateCoins(0);
    }
    private void Start()
    {
        _SenSlider.value = Datamanager._instance._thisGameData.sensitivity;
        OnSesitivityChange(_SenSlider);
    }
    public void UpdateCoins(int t)
    {
        Datamanager._instance.CoinsUpdate(t);
        _totalCoins.text = Datamanager._instance._thisGameData.TotalCoinsCollected+"";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            OnBackPressed();
        }
    }

    public void OnBackPressed() {
        SoundManager.PlaySFX(ButtonClick,false,0);
        if (_panel[0].activeInHierarchy)
        {
            ActivatePanel(_panel.Count - 1);
        }
        else
        {
            if (activePanels.Count > 0)
            {
                activePanels.Peek().SetActive(false);
                activePanels.Pop();
            }
            if (inactivePanels.Count > 0)
            {
                GameObject obj = inactivePanels.Peek();
                obj.SetActive(true);
                //if (obj.GetComponent<TweenObj>() != null)
                //{
                //    obj.GetComponent<TweenObj>().Call_TweenIn();
                //}
                activePanels.Push(inactivePanels.Peek());
                inactivePanels.Pop();
            }
            
            if (_panel[0].activeInHierarchy)
            {
                _subHeader.gameObject.SetActive(false);
                Avatar.SetActive(true);
                _backBtn.SetActive(false);
            }
            else
            {
                _subHeader.gameObject.SetActive(true);
                if (_panel[1].activeInHierarchy)
                {
                    subtxtupdate(1);
                }
                Avatar.SetActive(false);
                _backBtn.SetActive(true);
            }
        }
    }

    public TextMeshProUGUI senvalue;
    public Slider _SenSlider;
    public void OnSesitivityChange(UnityEngine.UI.Slider _slider)
    {
        if (Datamanager._instance != null)
        {
            Datamanager._instance._thisGameData.sensitivity = _slider.value;
            PlayerPrefs.SetFloat("sensitivity", _slider.value);
        }
        senvalue.text = ((_slider.value-1) * 100).ToString("0");
    }

    public void subtxtupdate(int panelID)
    {
        if (panelID == 1)//levelselection,inventory,store,option,exit
        {
            //_subHeader.text = "WORLD SELECTION";
            _subHeader.GetComponent<LocalizedTextmeshPro>().doupdatetext("Subheader.WorldSelection");
        }
        else if (panelID == 2)
        {
            //_subHeader.text = "LEVEL SELECTION";
            _subHeader.GetComponent<LocalizedTextmeshPro>().doupdatetext("Subheader.LevelSelection");
        }
        else if (panelID == 3)
        {
            // _subHeader.text = "INVENTORY";
            _subHeader.GetComponent<LocalizedTextmeshPro>().doupdatetext("Subheader.WorldSelection");
        }
        else if (panelID == 4)
        {
            _subHeader.text = "STORE";
            _subHeader.GetComponent<LocalizedTextmeshPro>().doupdatetext("Subheader.Store");
        }
        else if (panelID == 5)
        {
            //_subHeader.text = "OPTIONS";
            _subHeader.GetComponent<LocalizedTextmeshPro>().doupdatetext("Subheader.options");
        }
        else if (panelID == 6)
        {
            //_subHeader.text = "QUIT GAME?";
            _subHeader.GetComponent<LocalizedTextmeshPro>().doupdatetext("Subheader.quit");
        }
        //FirebaseEvents.instance.LogFirebaseEvent("Funnel_Event", "Scene" + SceneManager.GetActiveScene().buildIndex, _subHeader.text);
    }

    public void ActivatePanel(int panelID) {
        if (panelID >= 0) {
            SoundManager.PlaySFX(ButtonClick,false,0);
            lastactivepanel = panelID;
            if (activePanels.Count > 0)
            {
                activePanels.Peek().SetActive(false);
                inactivePanels.Push(activePanels.Peek());
            }
            subtxtupdate(panelID);

            ///////////
            if (panelID == 0)
            {
                _subHeader.gameObject.SetActive(false);
                Avatar.SetActive(true);
                _backBtn.SetActive(false);
            }
            else
            {
                if (panelID == 6)
                {
                   // AdManager._instance.ShowExitAd();
                }

                _subHeader.gameObject.SetActive(true);
                Avatar.SetActive(false);
                _backBtn.SetActive(true);
            }


            _panel[panelID].SetActive(true);
            //if (_panel[panelID].GetComponent<TweenObj>() != null)
            //{
            //    _panel[panelID].GetComponent<TweenObj>().Call_TweenIn();
            //}
            activePanels.Push(_panel[panelID]);
        }
    }

        public void QuitYes()
        {
        Application.Quit();
        }

        public void RateCTA(){
            #if UNITY_ANDROID
                Application.OpenURL("https://play.google.com/store/apps/details?id="+Application.identifier);            
            #elif UNITY_IOS
                Application.OpenURL("https://play.google.com/store/apps/details?id="+Application.identifier);            
            #endif
        }

        public void OpenURL_CTA(string _url){
            #if UNITY_ANDROID
                Application.OpenURL("https://play.google.com/store/apps/developer?id=Sigma+App+Labs");            
            #elif UNITY_IOS
                Application.OpenURL(_url);            
#endif
    }
    #endregion

}
