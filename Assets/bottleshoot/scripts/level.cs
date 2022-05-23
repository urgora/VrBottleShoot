using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class level : MonoBehaviour
{
    public float noofbottle,Bulletcount,timer;
    public bool gameovercheck;
    Gamemanager gm;
    private void Awake()
    {
        // FindObjectOfType<Gamemanager>().totalbt = Bulletcount;
        gm = FindObjectOfType<Gamemanager>();
        gm.totalbt = Bulletcount;
        gm.totalbottle = noofbottle;
        gm.startobjectivepanelupdate(Bulletcount.ToString(), noofbottle.ToString(),timer.ToString());
    }
    private void Update()
    {
        if(gm.gamestarted)
        {
            if (noofbottle <= 0 && !gameovercheck)
            {
                //StartCoroutine(gameoverwin());
                //gameovercheck = true;
                Invoke("conditionchecking", 2f);
                gameovercheck = true;
                gm.canshoot = false;
                //  gm.gamestarted = false;
            }


            else  if ((Bulletcount <= 0 || timer <= 0) && !gameovercheck)
            {

                Invoke("conditionchecking", 2f);
                gameovercheck = true;
              
            }
            else
            {
                timerecounter();
            }
            Gamemanager.timer = timer;
            Gamemanager.bulletcount = Bulletcount;
            Gamemanager.currentbottlecount = noofbottle;
        }
     
      
    }


    public void conditionchecking()
    {
        if (noofbottle <= 0)
        {
            StartCoroutine(gameoverwin());
            gameovercheck = true;
        }
        else
        {
            StartCoroutine(gameoverloss());
            gameovercheck = true;
        }
    }
    public void timerecounter()
    {
        timer -= Time.deltaTime;
    }
    IEnumerator gameoverwin()
    {
        yield return new WaitForSeconds(3);
       gm.Gamedone();
    }
    IEnumerator gameoverloss()
    {
        yield return new WaitForSeconds(3);
        gm.gamefalied();
    }
}
