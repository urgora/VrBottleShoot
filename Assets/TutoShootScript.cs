using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedBlueGames.Tools.TextTyper;
using TMPro;
using Assets.SimpleLocalization;
public class TutoShootScript : MonoBehaviour
{

    public Animator firBtnAnimator;
    public Camera fpscam;
    public TextTyper TutoText1;
    public GameObject hand;
    public GameObject DownscrollHand;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.levelno.value == 1)
        {
            //firBtnAnimator.Play("FireAnim");



            RaycastHit hit;
            if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, 300))
            {
                //Debug.Log("Tutorial Ray : " + hit.transform.name);
                target tar = hit.transform.GetComponent<target>();
                if (!GameManager.instance.tutdone)
                {
                    if (Datamanager._instance._thisGameData.UIcontrols)
                    {
                        if (tar != null)
                        {
                            //Step 2
                            firBtnAnimator.SetBool("scale", true);
                            hand.SetActive(true);
                            if (DownscrollHand.activeInHierarchy)
                            {
                                DownscrollHand.SetActive(false);
                            }
                            TutoText1.GetComponent<LocalizedTextmeshPro>().doupdatetext("Tutorial Panel.Text1"); // GetComponent<TextMeshProUGUI>().text = "Click on FireButton";
                        }
                        else
                        {
                            //step 1
                            firBtnAnimator.SetBool("scale", false);
                            hand.SetActive(false);
                            TutoText1.GetComponent<LocalizedTextmeshPro>().doupdatetext("Tutorial Panel.Text2");      //GetComponent<TextMeshProUGUI>().text = "Aim the bottle";
                        }
                    }
                    else
                    {
                        if (tar != null)
                        {
                            //Step 2
                            //firBtnAnimator.SetBool("scale", true);
                            //hand.SetActive(false);
                            if (DownscrollHand.activeInHierarchy)
                            {
                                DownscrollHand.SetActive(false);
                            }
                            TutoText1.GetComponent<LocalizedTextmeshPro>().doupdatetext("Tutorial Panel.Text5"); // GetComponent<TextMeshProUGUI>().text = "Release to shoot";
                        }
                        else
                        {
                            //step 1
                            //firBtnAnimator.SetBool("scale", false);
                            //hand.SetActive(false);
                            TutoText1.GetComponent<LocalizedTextmeshPro>().doupdatetext("Tutorial Panel.Text2");     //GetComponent<TextMeshProUGUI>().text = "Aim the bottle";
                        }

                    }
                }
            }
        }
    }
}
