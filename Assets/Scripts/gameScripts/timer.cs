using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Floatvariable lvl;
    //public bool isDisplayed = false;

    [HideInInspector]
    public int lvlTime =0;

    TextMeshProUGUI txt;

 
    public Coroutine _timerunning;
    public void StartGame()
    {
        txt = GetComponent<TextMeshProUGUI>();
         _timerunning = StartCoroutine(StartAction());
       /* if (lvl.value == 1 && isDisplayed == false)
        {
            GameManager.instance.TutorialPanel.SetActive(true);
            isDisplayed = true;
        }*/
    }


    public void stoptime()
    {
        this.StopCoroutine(_timerunning);
    }
  public IEnumerator StartAction()
   {
       if(lvlTime <= 0){
           lvlTime = 60;
       }
        txt.text = Mathf.Floor(lvlTime / 60) + " : " + lvlTime % 60;
        while (lvlTime > 0.5f){
            yield return new WaitForSeconds(1f);
            if(Datamanager._instance._gameStateNow == GameState.gameStart){
                lvlTime -= 1;
                int minutes = Mathf.FloorToInt(lvlTime / 60F);
                int seconds = Mathf.FloorToInt(lvlTime - minutes * 60);
               txt.text = string.Format("{0:0}:{1:00}", minutes, seconds);

                if (lvlTime <= 15f & !FindObjectOfType<GameManager>().hintc&&Datamanager._instance._thisGameData.ccounter>0)
                {
                    FindObjectOfType<GameManager>().blinkclockcounter();
                }
                //txt.text = Mathf.Floor(lvlTime / 60) + " : " + lvlTime % 60;
            }
            else if(Datamanager._instance._gameStateNow == GameState.gameRevive || Datamanager._instance._gameStateNow == GameState.gameFinish|| Datamanager._instance._gameStateNow == GameState.gameFail)
            {
                yield break;
            }      
       }
        GameManager.instance.gameOverPanel(false);

   }
}
