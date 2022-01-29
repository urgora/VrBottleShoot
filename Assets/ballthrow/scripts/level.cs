using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    public float noofbottle,ballcount;
    public GameObject unbroken,balls;
    public bool gameovercheck;
    private void Awake()
    {
        noofbottle = unbroken.transform.childCount;
        ballcount = balls.transform.childCount;
    }
    private void Update()
    {
        noofbottle = unbroken.transform.childCount;
        ballcount = balls.transform.childCount;
        if (noofbottle<=0&&!gameovercheck)
        {
            StartCoroutine(gameover());
            gameovercheck = true;
        }
        if(ballcount<=0)
        {
            FindObjectOfType<Gamemanager>().gamefalied();
           
        }
    }
    IEnumerator gameover()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<Gamemanager>().Gamedone();
    }
}
