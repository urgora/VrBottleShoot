using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class shopmanager : MonoBehaviour
{
    //[SerializeField]
    //public Text availableitem;
    [SerializeField]
    public TextMeshProUGUI purchaseditem;
    public GameObject buypanel, sucess, fail;

    public string[] skus = new[] { "Fullvertion" };
    void Start()
    {

        if (!PlayerPrefs.HasKey("demo"))
        {
            string purchasedetail = "demo";
            PlayerPrefs.SetString("demo", purchasedetail);
        }

      //  PlayerPrefs.SetString("demo", "lvlunlocked");
        //  Getprice();
        GetPurchase();
    }

    void Getprice()
    {
        IAP.GetProductsBySKU(skus).OnComplete(GetPricesCallback);
    }
    private void GetPricesCallback(Message<ProductList> msg)
    {
        if (msg.IsError) return;
        foreach(var prod in msg.GetProductList())
        {
          //  availableitem.text+= $"{ prod.Name}- { prod.FormattedPrice}\n";

        }
    }
    void GetPurchase()
    {
        IAP.GetViewerPurchases().OnComplete(GetPurchasesCallback);
    }

    private void GetPurchasesCallback(Message<PurchaseList> msg)
    {
        if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            purchaseditem.text += $"{ purch.Sku}-{purch.GrantTime}\n";
            if (purch.Sku == "Fullvertion")
            {
                string purchasedetail = "lvlunlocked";
                PlayerPrefs.SetString("demo", purchasedetail);

              
            }

            //string purchasedetail = purch.Sku;
            //PlayerPrefs.SetString("demo", purchasedetail);
            //buypanel.SetActive(false);
        }
    }

    public void Buycube()
    {
//#if UNITY_EDITOR
//        PlayerPrefs.SetString("demo", "lvlunlocked");
//        buypanel.SetActive(false);
//        FindObjectOfType<levelselection>().deletechilds();
//        FindObjectOfType<levelselection>().levelshow();
//#endif
          IAP.LaunchCheckoutFlow(sku: "Fullvertion").OnComplete(BuyCubeCallback);
         // Invoke("buysucess", 4);
    }
    void BuyCubeCallback(Message<Purchase> msg)
    {
        if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            // purchaseditem.text += $"{ purch.Sku}-{purch.GrantTime}\n";
            if (purch.Sku == "Fullvertion")
            {
                string purchasedetail = "lvlunlocked";
                PlayerPrefs.SetString("demo", purchasedetail);
                if (PlayerPrefs.GetInt("levelcompleted")==4)
                {
                    PlayerPrefs.SetInt("levelcompleted", 5);
                }
                sucess.SetActive(true);
                flurryinstance.instance.levelstatus("In_APP_Purchase__sucess");
            }
            else
            {
               // fail.SetActive(true);
            }
        }

      
    }
    public void buysucess()
    {
        levelselection lv = FindObjectOfType<levelselection>();
        lv.deletechilds();
        lv.levelshow();
        sucess.SetActive(false);
        fail.SetActive(false);
        // SceneManager.LoadScene("main");

    }


}
