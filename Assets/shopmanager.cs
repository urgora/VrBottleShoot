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
    public GameObject buypanel;

    public string[] skus = new[] { "gora", "holy" };
    void Start()
    {

        if (!PlayerPrefs.HasKey("demo"))
        {
            string purchasedetail = "demo";
            PlayerPrefs.SetString("demo", purchasedetail);
        }
    
        
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
            if (purch.Sku == "levelunlocked")
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
          IAP.LaunchCheckoutFlow(sku: "levelunlocked").OnComplete(BuyCubeCallback);
          Invoke("buysucess", 4);
    }
    void BuyCubeCallback(Message<Purchase> msg)
    {
        //if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            // purchaseditem.text += $"{ purch.Sku}-{purch.GrantTime}\n";
            if (purch.Sku == "levelunlocked")
            {
                string purchasedetail = "lvlunlocked";
                PlayerPrefs.SetString("demo", purchasedetail);
            }
        }

      
    }
    public void buysucess()
    {
       
        SceneManager.LoadScene("main");

    }


}
