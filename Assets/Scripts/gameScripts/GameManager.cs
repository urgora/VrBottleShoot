using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using RedBlueGames.Tools.TextTyper;
using Assets.SimpleLocalization;

public class GameManager : MonoBehaviour
{

    //public static int TotalBullets = 0;
    public static int TotalBottles = 0;

    public Transform PlayerObj;

    public int CurrentLevel;
    public int GivenBottles;
    public int GivenBullets;
    public int initTime;


    internal int initbullets, initbottles;

    [Header("LevelsData")]
    public LevelData[] levels;
    public GameObject[] levelset;

    public Floatvariable levelno;
    public Floatvariable worldno;

    [Header("Canvas UI Elements")]
    public TextMeshProUGUI RemainingBullets;
    public TextMeshProUGUI _totalCoins, Coinsgained, RemainingBottles, Result, MagazineCounter, ClockCounter, _totalCoinsatPause;

    [Header("Canvas UI Panels")]
    public GameObject GameOverPanel;
    public GameObject pausepanel;
    public GameObject winpanel, failpanel;
    public GameObject[] StarstoDisplay;
    public GameObject NextBtn;
    internal int starscollected;

    [Header("CTimerObj")]
    public timer TimerController;

    [Header("ReviveElements")]
    public CanvasGroup ReviveBtnGrp;
    public CanvasGroup[] restBtns;
    public Image ReviveFiller;
    public Image NextFiller;

    [Header("SFXClips")]
    public AudioClip coinsUpdate;
    public AudioClip gamewinAudio, gameLoseAudio, ContinueAudio, ExtraTimeAudio, starAudio;
    public AudioClip[] gamestartflow;
    public string[] gamestartflowlabels;
    public AudioClip alright, Excellent, combo;
    public AudioClip ButtonClick;

    public TextTyper hint1,GLLabel;//hint2

    public negun Gunselected;
    public static GameManager instance;

    [Header("Objective")]
    public GameObject ObjectivePanel;
    public TextMeshProUGUI Cbullets, Ctime, Cbottles, levelno_label;

    public GameObject DoublerewardBtn;
    //public bool isDisplayed = false;

    [Header("UI COntrols")]
    public GameObject[] UIControlbutton;


    [Header("hints")]
    public Animation mcount;
    public Animation ccount;

    [Header("Tut_Elements")]
    public CanvasGroup TutorialPanel;
    public TextTyper introtut;

    public Animation Hinttut;
    public void Awake()
    {
        instance = this;
        if (levels[levelno.value - 1] != null)
        {
            CurrentLevel = levels[levelno.value - 1].levelno;
            GivenBottles = levels[levelno.value - 1].BottlesCount;
            GivenBullets = levels[levelno.value - 1].BulletsCount;
            initbottles = GivenBottles;
            initbullets = GivenBullets;
            initTime = levels[levelno.value - 1].TimeCount;

            PlayerObj.rotation = levels[levelno.value - 1].PlayerNode.rotation;
            PlayerObj.position = levels[levelno.value - 1].PlayerNode.position;
        }

        for (int i = 0; i < levelset.Length; i++)
        {
            if ((levelno.value - 1) == i)
            {
                levelset[i].SetActive(true);
            }
            else
            {
                levelset[i].SetActive(false);
            }
        }
        duration = 7;


        MagazineCounter.text = Datamanager._instance._thisGameData.mcounter + "";
        ClockCounter.text = Datamanager._instance._thisGameData.ccounter + "";
        StartCoroutine(dolevelobjnoUpdate());
        reviveRoutine = null;
    }

    IEnumerator dolevelobjnoUpdate()
    {
        levelno_label.GetComponent<LocalizedTextmeshPro>().doupdatetext("ObjectivePanel.Objective");
        yield return new WaitForEndOfFrame();
        levelno_label.text = levelno_label.text +" "+ levelno.value.ToString();
    }

    void OnApplicationQuit()
    {
        //Debug.Log("Application ending after " + Time.time + " seconds");
       // FirebaseEvents.instance.LogFirebaseEvent("ApplicationQuit_from_game", "forfiet", "level_"+CurrentLevel);
    }

    public void updatecontrols()
    {
        if (Datamanager._instance._thisGameData.UIcontrols)
        {
            for (int i = 0; i < UIControlbutton.Length; i++)
            {
                UIControlbutton[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < UIControlbutton.Length; i++)
            {
                UIControlbutton[i].SetActive(false);
            }
        }
    }
    public GameObject Loadingscreen;
    // Use this for initialization
    IEnumerator Start()
    {
        TimerController.lvlTime = this.initTime;
        GameOverPanel.SetActive(false);
        closestore.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => storeopen(false));

        objectivesshow();
        TotalBottles = GivenBottles;
        RemainingBullets.text = GivenBullets + " / " + initbullets;
        RemainingBottles.text = GivenBottles + " LEFT";
        //        hint2.TypeText("Welcome to the world "+(worldno.value+1)+"/Level "+levelno.value, -2f);
        yield return new WaitForEndOfFrame();
    }

    void objectivesshow()
    {
        Cbottles.text = "" + initbottles;
        Cbullets.text = "" + initbullets;
        int minutes = Mathf.FloorToInt(initTime / 60F);
        int seconds = Mathf.FloorToInt(initTime - minutes * 60);
        Ctime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        ObjectivePanel.SetActive(true);
    }

    public void onstartGame()
    {
        ObjectivePanel.SetActive(false);

        StartCoroutine(gamelauncher());
       // Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart, new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevel, "Level" + levelno.value));
    }

    IEnumerator gamelauncher()
    {
        yield return new WaitForSeconds(0.1f);
        ///321 start audios
        GLLabel.GetComponent<CanvasGroup>().alpha = 1f;
        for (int i = 0; i < gamestartflow.Length; i++)
        {
            GLLabel.TypeText(gamestartflowlabels[i], -2f);
            SoundManager.PlaySFX(gamestartflow[i], false, 0);
            yield return new WaitForSeconds(0.6f);
        }
        GLLabel.GetComponent<CanvasGroup>().alpha = 0f;
        Datamanager._instance._gameStateNow = GameState.gameStart;
        TimerController.StartGame();
        if (levelno.value == 1 && worldno.value == 0)
        {

            TutorialPanel.alpha = 1.0f;
            yield return new WaitForSeconds(0.1f);
            Hinttut.Play("hintpop1");
            StartCoroutine(typertextlocalisation("Tutorial Panel.Text3", introtut));
            //introtut.TypeText("Swipe down and aim the bottle", -2f);
        }
    }

    IEnumerator  typertextlocalisation(string key,TextTyper KT)
    {
        KT.GetComponent<LocalizedTextmeshPro>().doupdatetext(key);
        yield return new WaitForEndOfFrame();
        string t = KT.GetComponent<TextMeshProUGUI>().text;
        KT.GetComponent<TextMeshProUGUI>().text = "";
        KT.TypeText(t, -2f);
    }
    public bool tutdone = false;
    public Animator firBtnAnimator;
    public GameObject hand;
    public IEnumerator closetut()
    {
        tutdone = true;
        // introtut.TypeText("Now you are Ready to Play", -2f);
        StartCoroutine(typertextlocalisation("Tutorial Panel.Text4", introtut));
        yield return new WaitForSeconds(1.25f);
        Hinttut.Play("hintpop2");
        yield return new WaitForSeconds(0.5f);
        TutorialPanel.alpha = 0.0f;
        firBtnAnimator.SetBool("scale", false);
        hand.SetActive(false);
    }
    bool hintm = false;
    public bool hintc = false;
    public void BulletsUpdate()
    {

        if (GivenBullets > 0)
        {
            GivenBullets -= 1;
        }
        Invoke("GameoverCheck", 0.1f);
        RemainingBullets.text = GivenBullets + " / " + initbullets;
        if (GivenBullets < GivenBottles && !hintm && Datamanager._instance._thisGameData.mcounter > 0)
        {
            hintm = true;
            ///blink button magazine
            mcount.Play("blink");
        }
    }

    public void blinkclockcounter()
    {
        if (!hintc && Datamanager._instance._thisGameData.ccounter > 0)
        {
            hintc = true;
            ///blink button magazine
            ccount.Play();
        }
    }

    //[HideInInspector]
    public bool _aimpos = false;

    public void Aim()
    {
        if (this.Gunselected != null)
        {
            Animator t = this.Gunselected.GunArm;
            if (t.GetBool("Aim") == false)
            {
                t.SetBool("Aim", true);
            }
            else
            {
                t.SetBool("Aim", false);
            }
            _aimpos = t.GetBool("Aim");
            t.GetComponent<negun>().changeFOV();
        }
    }

    public void Shoot()
    {
        if (this.Gunselected != null)
        {
            this.Gunselected.Shoot();
        }
    }

    public void rewardMagazines()
    {
        if (Datamanager._instance._thisGameData.TotalCoinsCollected > 500)
        {
            SoundManager.PlaySFX(starAudio, false, 0);
            Datamanager._instance._thisGameData.mcounter += 1;
            MagazineCounter.text = Datamanager._instance._thisGameData.mcounter + "";
            Datamanager._instance._thisGameData.TotalCoinsCollected -= 500;
            Datamanager._instance.dataSave();
          //  AdManager._instance.boosterreward(0);
        }
        else
        {
            storeopen(true);
        }
    }

    public void rewardClocks()
    {
        if (Datamanager._instance._thisGameData.TotalCoinsCollected > 500)
        {
            SoundManager.PlaySFX(starAudio, false, 0);
            Datamanager._instance._thisGameData.ccounter += 1;
            ClockCounter.text = Datamanager._instance._thisGameData.ccounter + "";
            Datamanager._instance._thisGameData.TotalCoinsCollected -= 500;
            Datamanager._instance.dataSave();
           // AdManager._instance.boosterreward(1);
        }
        else
        {
            storeopen(true);
        }
    }
    public void AddMagazineBooster()
    {
        if (Datamanager._instance._thisGameData.mcounter > 0)
        {
            Datamanager._instance._thisGameData.mcounter -= 1;
            mcount.Play("blinkidle");
            hintm = false;
            MagazineCounter.text = Datamanager._instance._thisGameData.mcounter + "";
            addMagazine();
          //  FirebaseEvents.instance.LogFirebaseEvent("BoosterUsed", "Booster", "BType_Magazine");
        }
    }

    public void AddClockBooster()
    {
        if (Datamanager._instance._thisGameData.ccounter > 0)
        {
            Datamanager._instance._thisGameData.ccounter -= 1;
            ccount.Play("blinkidle");
            hintm = false;
            ClockCounter.text = Datamanager._instance._thisGameData.ccounter + "";
            addClock();
           // FirebaseEvents.instance.LogFirebaseEvent("BoosterUsed", "Booster", "BType_Clock");
        }
    }
    public void UpdateCoins(int t)
    {
        Datamanager._instance.CoinsUpdate(t);
        _totalCoins.text = Datamanager._instance._thisGameData.TotalCoinsCollected + "";
    }

    int getbulletsleft()
    {
        return (initbullets - GivenBullets);
    }

    public void BottlesUpdate()
    {

        if (GivenBottles > 0)
        {
            GivenBottles -= 1;
        }
        Invoke("GameoverCheck", 0.1f);
        RemainingBottles.text = GivenBottles + " LEFT";
    }

    // Update is called once per frame
    public void GameoverCheck()
    {

        if (!GameOverPanel.activeInHierarchy)
        {
            if (GivenBottles == 0)
            {
                gameOverPanel(true);
            }
            else
            {
                if (GivenBullets == 0)
                {
                    gameOverPanel(false);
                }
            }
        }
    }

    public void addMagazine()
    {
        this.GivenBullets += 6;
        RemainingBullets.text = GivenBullets + " / " + initbullets;
    }

    public void addClock()
    {
        this.TimerController.lvlTime += 20;
    }

    public void onrewardvideoSuccess()
    {
        //if (AdManager._instance.rewardTypeToUnlock == RewardType.doublereward)
        //{
        //    if (AdManager._instance.rewardedvideosuccess)
        //    {
        //        StartCoroutine(DoubleCoinsGainedText());
        //    }
        //    AdManager._instance.rewardedvideosuccess = false;
        //}
        //else if (AdManager._instance.rewardTypeToUnlock == RewardType.ExtraAddClock)
        //{
        //    Datamanager._instance._thisGameData.ccounter += 1;
        //    AdManager._instance.boosterreward(1);
        //    Datamanager._instance.dataSave();
        //    AdManager._instance.rewardedvideosuccess = false;
        //}
        //else if (AdManager._instance.rewardTypeToUnlock == RewardType.ExtraAddMagazine)
        //{
        //    Datamanager._instance._thisGameData.mcounter += 1;
        //    AdManager._instance.boosterreward(0);
        //    Datamanager._instance.dataSave();
        //    AdManager._instance.rewardedvideosuccess = false;
        //}
        //else if (AdManager._instance.rewardTypeToUnlock == RewardType.revivenow)
        //{
        //    ReviveSuccess();
        //}
      //  FirebaseEvents.instance.LogFirebaseEvent("Reward_Success", "Type", "Type_" + (int)AdManager._instance.rewardTypeToUnlock);
    }

    IEnumerator DoubleCoinsGainedText()
    {
        int t = (GetStarCount() * 50);
        int k = 0;
        int p = Datamanager._instance._thisGameData.TotalCoinsCollected;
        while (k < t)
        {
            if (k % 3 == 0)
            {
                SoundManager.PlaySFX(coinsUpdate, false, 0);
            }
            k += 3;
            p += 3;
            if (k > t)
            {
                int j = t - k;
                k = t;
                p = (p - 3) + j;
            }
            Coinsgained.text = "" + (t + k);
            //if (p != (Datamanager._instance._thisGameData.TotalCoinsCollected + t))	
            //{	
            //    p = Datamanager._instance._thisGameData.TotalCoinsCollected + t;	
            //}	
            _totalCoins.text = p + "";
            yield return null;
        }
        Datamanager._instance._thisGameData.TotalCoinsCollected += GetStarCount() * 50;
        _totalCoins.text = Datamanager._instance._thisGameData.TotalCoinsCollected + "";
        Datamanager._instance.dataSave();
    }

    public void PauseResume()
    {
        if (Datamanager._instance._gameStateNow == GameState.gameStart)
        {
            pausepanel.SetActive(true);
            updatecontrols();
            _totalCoinsatPause.text = Datamanager._instance._thisGameData.TotalCoinsCollected + "";
            Datamanager._instance._gameStateNow = GameState.pausePanel;
            if (TimerController._timerunning != null)
            {
                TimerController.stoptime();
            }
           // FirebaseEvents.instance.LogFirebaseEvent("Funnel_Event", "Scene" + SceneManager.GetActiveScene().buildIndex, "Pause_btn");
        }
        else if (Datamanager._instance._gameStateNow == GameState.pausePanel)
        {
            // SoundManager.PlaySFX(ContinueAudio, false, 0.2f);
            pausepanel.SetActive(false);
            StartCoroutine(gamelauncher());
        }
    }



    public GameObject closestore;

    public void storeopen(bool t)
    {
        closestore.SetActive(t);
        StoreManager._instance.canvas.SetActive(t);
    }

    public void callReward(int t)
    {
       // AdManager._instance.rewardTypeToUnlock = (RewardType)(t);
        if (reviveRoutine!=null)
        {
            StopCoroutine(reviveRoutine);
        }
        //if (creviveinbtw != null)
        //{
        //    StopCoroutine(creviveinbtw);
        //}

#if UNITY_EDITOR	
     //   AdManager._instance.rewardedvideosuccess = true;
        onrewardvideoSuccess();
#endif
        //call reward Video	
      //  AdManager._instance.ShowRewardedVideo();
    }
    public void gameOverPanel(bool result)
    {
        if (result)//win
        {
            //Win Data Functionality
            StartCoroutine(saveLevelSuccess());
           // Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd, new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevel, "Level" + levelno.value));
        }
        else
        { //Revive	
            //if (AdManager._instance.rewardBasedVideo.IsLoaded())
            //{
            //    reviveRoutine = StartCoroutine(ReviveState());
            //   // FirebaseEvents.instance.LogFirebaseEvent("level_Revived", "level", "Level_ " + levelno.value);
            // }
            // else
            // {
            //    if (GivenBullets == 0 || TimerController.lvlTime == 0)
            //    {
            //        creviveinbtw = StartCoroutine(cancelReviveInbtw());
            //    }
            // }
        }
    }

    
    Coroutine reviveRoutine;

    
    IEnumerator saveLevelSuccess()
    {
        yield return new WaitForSeconds(0.5f);
        _totalCoins.text = Datamanager._instance._thisGameData.TotalCoinsCollected + "";
        //nextbtn in active
        SoundManager.PlaySFX(gamewinAudio, false, 0.0f);
        Datamanager._instance._gameStateNow = GameState.gameFinish;
        Result.GetComponent<LocalizedTextmeshPro>().doupdatetext("GameOverPanel.LevelFinish");
        NextBtn.SetActive(false);
        NextBtn.GetComponent<Button>().interactable = false;
        DoublerewardBtn.SetActive(false);
        GameOverPanel.SetActive(true);
        winpanel.SetActive(true);

        Datamanager._instance._thisGameData._worlds[worldno.value].Levels[levelno.value - 1]._levelstatus = levelStatus.completed;
        if (Datamanager._instance._thisGameData._worlds[worldno.value].Levels.Count == levelno.value)
        {
            Datamanager._instance._thisGameData._worlds[worldno.value + 1]._islocked = false;
            Datamanager._instance._thisGameData._worlds[worldno.value + 1].Levels[0]._levelstatus = levelStatus.current;
        }
        else if (Datamanager._instance._thisGameData._worlds[worldno.value].Levels.Count > levelno.value)
        {
            Datamanager._instance._thisGameData._worlds[worldno.value].Levels[levelno.value]._levelstatus = levelStatus.current;
        }
        StarCountUpdate(GetStarCount());
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < GetStarCount(); i++)
        {
            SoundManager.PlaySFX(starAudio, false, 0);
            StarstoDisplay[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        //Coinsgained.text = "" + (GetStarCount() * 50);
        /*int t = (GetStarCount() * 50);
        int p = Datamanager._instance._thisGameData.TotalCoinsCollected;
        while (t > 0)
        {
            if (t % 10 == 0)
            {
                SoundManager.PlaySFX(coinsUpdate, false, 0);
            }
            t -= 10;
            p += 10;
            if (t < 0)
            {
                t = 0;
            }
            
            Coinsgained.text = "" + t;
            _totalCoins.text = p + "";
            yield return null;
        }*/

        int t = (GetStarCount() * 50);
        int k = 0;
        int p = Datamanager._instance._thisGameData.TotalCoinsCollected;
        while (k < t)
        {
            if (k % 3 == 0)
            {
                SoundManager.PlaySFX(coinsUpdate, false, 0);
            }
            k += 3;
            p += 3;
            if (k > t)
            {
                int j = t - k;
                k = t;
                p = (p - 3) + j;
            }
            Coinsgained.text = "" + k;
            //if( p != (Datamanager._instance._thisGameData.TotalCoinsCollected + t))	
            //{	
            //    p = Datamanager._instance._thisGameData.TotalCoinsCollected + t;	
            //}
            _totalCoins.text = p + "";
            yield return null;
        }

        Datamanager._instance._thisGameData.TotalCoinsCollected += (GetStarCount() * 50);
      //  AdManager._instance.ShowGameWinInterstitial();
        Datamanager._instance.dataSave();
        yield return new WaitForSeconds(0f);
        NextBtn.SetActive(true);
        DoublerewardBtn.SetActive(true);
        //DoublerewardBtn.SetActive(false);

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            NextFiller.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            //normalizedTime += Time.deltaTime * 8f / duration;
            yield return null;
        }
        NextBtn.GetComponent<Button>().interactable = true;
        NextFiller.gameObject.SetActive(false);
        ////next btn active
        //DoublerewardBtn.SetActive(false);	
    }


        public void StarCountUpdate(int starcount)
    {
        int _starCount = Datamanager._instance._thisGameData._worlds[worldno.value].Levels[levelno.value - 1].starsCollected;
        if (_starCount < starcount)
        {
            Datamanager._instance._thisGameData._worlds[worldno.value].Levels[levelno.value - 1].starsCollected = starcount;
        }
    }

    int GetStarCount()
    {
        int stars = 1;
        if (GivenBullets >= ((initbullets - initbottles)))
        {
            stars = 3;
        }
        else if (GivenBullets >= ((initbullets - initbottles) - 2))
        {
            stars = 2;
        }
        return stars;
    }

    float duration = 0;
    IEnumerator ReviveState()
    {
        //Result.text = "REVIVE";	
        Result.GetComponent<LocalizedTextmeshPro>().doupdatetext("GameOverPanel.Revive");
        Datamanager._instance._gameStateNow = GameState.gameRevive;
        _totalCoins.text = Datamanager._instance._thisGameData.TotalCoinsCollected + "";
        yield return new WaitForSeconds(0.5f);
        GameOverPanel.SetActive(true);
        failpanel.SetActive(true);
        ReviveBtnGrp.interactable = true;
        Debug.LogWarning("Revive");
        for (int i = 0; i < restBtns.Length; i++)
        {
            restBtns[i].interactable = false;
            restBtns[i].alpha = 0.5f;

        }
        float normalizedTime = 1;
        while (normalizedTime > 0f)
        {
            ReviveFiller.fillAmount = normalizedTime;
            normalizedTime -= Time.deltaTime / duration;
            yield return null;
        }
        if (GivenBullets == 0 || TimerController.lvlTime == 0)
        {
            creviveinbtw = StartCoroutine(cancelReviveInbtw());
        }
    }
    Coroutine creviveinbtw;

    public IEnumerator cancelReviveInbtw()
    {
            Datamanager._instance._gameStateNow = GameState.gameFail;
            GameOverPanel.SetActive(true);
            failpanel.SetActive(true);
            _totalCoins.text = Datamanager._instance._thisGameData.TotalCoinsCollected + "";
            ReviveBtnGrp.interactable = false;
            ReviveBtnGrp.gameObject.SetActive(false);
            for (int i = 0; i < restBtns.Length; i++)
            {
                restBtns[i].interactable = true;
                restBtns[i].alpha = 1f;
            }
            Result.GetComponent<LocalizedTextmeshPro>().doupdatetext("GameOverPanel.LevelFail");
            SoundManager.PlaySFX(gameLoseAudio, false, 0.0f);
            yield return new WaitForSeconds(0.75f);

          //  FirebaseEvents.instance.LogFirebaseEvent("level_Failed", "level", "Level_ " + levelno.value);

            if (GivenBullets == 0)
            {
               // FirebaseEvents.instance.LogFirebaseEvent("nomoresbulletslevel_failed", "failed", "Level_" + levelno.value);
            }
            else
            {
               // FirebaseEvents.instance.LogFirebaseEvent("nomoreTimeLeftlevel_failed", "failed", "Level_" + levelno.value);
            }

         //   AdManager._instance.ShowGameFailInterstitial();
    }

    public void nextlevel()
    {
        SoundManager.PlaySFX(ButtonClick, false, 0);

        if (Datamanager._instance._thisGameData._worlds[worldno.value].Levels.Count > levelno.value)
        {
            levelno.SetValue(levelno.value + 1);
        }
        else if (Datamanager._instance._thisGameData._worlds[worldno.value].Levels.Count == levelno.value)
        {
            if (worldno.value == 0)
            {
                if (Datamanager._instance.totalworldstarsCollected(1) >= Datamanager._instance.World2RequiredStars)
                {
                    levelno.SetValue(1);
                    worldno.SetValue(worldno.value + 1);
                }
                else
                {
                    //throw no enough stars
                }
            }
            else if (worldno.value == 1)
            {
                if (Datamanager._instance.totalworldstarsCollected(2) >= Datamanager._instance.World3RequiredStars)
                {
                    levelno.SetValue(1);
                    worldno.SetValue(worldno.value + 1);
                }
                else
                {
                    //throw no enough stars
                }
            }
            //levelno.SetValue(1);
            //worldno.SetValue(worldno.value + 1);
        }
        Loadingscreen.SetActive(true);
       // FirebaseEvents.instance.LogFirebaseEvent("Funnel_Event", "Scene" + SceneManager.GetActiveScene().buildIndex, "Next_btn");

        if (worldno.value > Datamanager._instance._thisGameData._worlds.Count)
        {
            worldno.SetValue(1);
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            SceneManager.LoadSceneAsync(worldno.value + 2);
        }

    }

    public void changeHint2()
    {
        String txt = "";
        if ((initbullets - GivenBullets) == (initbottles - GivenBottles))
        {
            if (GivenBottles == initbottles - 1)
            {
                txt = "Alright!";
                SoundManager.PlaySFX(alright, false, 0.2f);
            }
            else if (GivenBottles == initbottles - 2)
            {
                txt = "COMBO!!";
                SoundManager.PlaySFX(combo, false, 0.2f);
            }
            else if (GivenBottles == initbottles - 3)
            {
                txt = "Excellent!";
                SoundManager.PlaySFX(Excellent, false, 0.2f);
            }
            else
            {
                if (GivenBottles > GivenBullets)
                {
                    if (GivenBullets > 1)
                    {
                        txt = GivenBullets + " Bullets left!";
                    }
                    else
                    {
                        txt = GivenBullets + " Bullet left!";
                    }
                }
                else if (GivenBottles == GivenBullets)
                {
                    txt = "Don't miss bullet!!";
                }
                else if (GivenBullets > GivenBottles)
                {
                    if (GivenBottles > 1)
                    {
                        txt = GivenBottles + " Bottles left!";
                    }
                    else
                    {
                        txt = GivenBottles + " Bottle left!";
                    }
                }
            }
        }
        /*else{
                txt = "GOOD SHOT";
                SoundManager.PlaySFX(alright,false);
        }*/
        else
        {
            if (GivenBottles > GivenBullets)
            {
                if (GivenBullets > 1)
                {
                    txt = GivenBullets + " Bullets left!";
                }
                else
                {
                    txt = GivenBullets + " Bullet left!";
                }
            }
            else if (GivenBottles == GivenBullets)
            {
                txt = "Don't miss bullet!!";
            }
            else if (GivenBullets > GivenBottles)
            {
                if (GivenBottles > 1)
                {
                    txt = GivenBottles + " Bottles left!";
                }
                else
                {
                    txt = GivenBottles + " Bottle left!";
                }
            }
        }
        //hint2.TypeText(txt, -2f);
        hint1.GetComponent<CanvasGroup>().alpha = 1f;
        hint1.TypeText(txt, -2f);
    }

    public void reload()
    {
        SoundManager.PlaySFX(ButtonClick, false, 0);
        levelno.SetValue(levelno.value);
        Loadingscreen.SetActive(true);
      //  FirebaseEvents.instance.LogFirebaseEvent("Funnel_Event", "Scene" + SceneManager.GetActiveScene().buildIndex, "Retry_btn");
     //   FirebaseEvents.instance.LogFirebaseEvent("level_Retry", "level", "Level_ " + levelno.value);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SoundManager.PlaySFX(ButtonClick, false, 0);
        Loadingscreen.SetActive(true);
        //FirebaseEvents.instance.LogFirebaseEvent("Funnel_Event", "Scene" + SceneManager.GetActiveScene().buildIndex, "Home_btn");
        SceneManager.LoadSceneAsync(1);
    }

    public void ReviveSuccess()
    {
        //if (AdManager._instance.rewardedvideosuccess)
        //{
        //    SoundManager.PlaySFX(ExtraTimeAudio, false, 0.5f);
        //    Datamanager._instance._gameStateNow = GameState.gameStart;
        //    GivenBullets = initbullets;
        //    //TimerController.lvlTime += (int)(initTime / 2);
        //    TimerController.lvlTime = (int)(initTime);
        //    GameOverPanel.SetActive(false);
        //    failpanel.SetActive(false);
        //    RemainingBullets.text = GivenBullets + " / " + initbullets;
        //    StopAllCoroutines();
        //    TimerController.StartGame();
        //  //  FirebaseEvents.instance.LogFirebaseEvent("level_ReviveSuccess", "level", "Level_ " + levelno.value);
        // //   FirebaseEvents.instance.SetUserProperty("star_count", "" + Datamanager._instance.totalstarsCollected());
        //    AdManager._instance.rewardedvideosuccess = false;
        //}
        //else
        //{
        //    if (GivenBullets == 0 || TimerController.lvlTime == 0)
        //    {
        //        creviveinbtw = StartCoroutine(cancelReviveInbtw());
        //    }
        //}
    }


}

[Serializable]
public class LevelData
{
    public int levelno;
    public int BottlesCount;
    public int BulletsCount;
    public Transform PlayerNode;
    public int TimeCount = 0;
}

