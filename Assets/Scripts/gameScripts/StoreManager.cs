using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager _instance;
    public GameObject canvas;
    // Start is called before the first frame update
  public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int coins)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<MainMenuManager>().UpdateCoins(coins);
        }
        else
        {
            Datamanager._instance.CoinsUpdate(coins);
        }
    }

    public void removeads(){
         PlayerPrefs.SetInt("removeAd",1);
    }
}
