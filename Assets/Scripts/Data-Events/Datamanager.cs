using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Datamanager : MonoBehaviour
{
    public GameData _thisGameData;
    public GameState _gameStateNow = GameState.menupanel;
    public bool test = false;
    public static Datamanager _instance;
    public int World2RequiredStars = 60;//30*2 min 2 stars per level
    public int World3RequiredStars = 60;//30*2 min 2 stars per level

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
        LoadData();
    }

    private void Start()
    {
        GameData _GD = _thisGameData;
        if (totalworldstarsCollected(1) >= World2RequiredStars)
        {
            _GD._worlds[1]._islocked = false;
        }
        else
        {
            _GD._worlds[1]._islocked = true;
        }

        /////
        ///
        if (totalworldstarsCollected(2) >= World3RequiredStars)
        {
            _GD._worlds[2]._islocked = false;
        }
        else
        {
            _GD._worlds[2]._islocked = true;
        }
    }


    private void LoadData()
    {

        if (PlayerPrefs.HasKey("GameData"))
        {
            _thisGameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("GameData"));
        }
        else
        {
            //initdata();
            _thisGameData.UIcontrols = true;
        }

        if (!PlayerPrefs.HasKey("removeAd"))
        {
            PlayerPrefs.SetInt("removeAd",0);
          }
        _thisGameData.removeAds = true?PlayerPrefs.GetInt("removeAd")==1:PlayerPrefs.GetInt("removeAd")==0;

        if (!PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("sensitivity", 1.8f);
        }
        _thisGameData.sensitivity = PlayerPrefs.GetFloat("sensitivity");
        dataSave();
    }

    public void CoinsUpdate(int t)
    {
        if (t > 0)
        {
            _thisGameData.TotalCoinsCollected += t;
        }
        else
        {
            _thisGameData.TotalCoinsCollected -= t;
        }
        dataSave();
    }

    public void dataSave()
    {
        string jsonData = JsonUtility.ToJson(_thisGameData, true);
        PlayerPrefs.SetString("GameData", jsonData);
    }

    public int totalstarsCollected()
    {
        int stars = 0;
        for (int k = 0; k < _thisGameData._worlds.Count; k++)
        {
            for (int j = 0; j < _thisGameData._worlds[k].Levels.Count; j++)
            {
                stars += _thisGameData._worlds[k].Levels[j].starsCollected;
            }
        }
        return stars;
    }
    public int totalworldstarsCollected(int wno)
    {
            int stars = 0;
            for (int j = 0; j < _thisGameData._worlds[wno].Levels.Count; j++)
            {
                stars += _thisGameData._worlds[wno].Levels[j].starsCollected;
            }
            return stars;
    }

    public void initdata()
    {
        _thisGameData = new GameData();
        _thisGameData._Guns = new List<GunData>();
        int[] gunsprices = new int[] { 1000, 1500, 2500 };
        for (int k = 0; k < 3; k++)
        {
            GunData _gun = new GunData();
            _gun.gunPrice = gunsprices[k];
            _gun.GunNo = k;
            if (k == 0)
            {
                _gun.gunStatus = GunStatus.Equipped;
            }
            else
            {
                _gun.gunStatus = GunStatus.Locked;
            }
            _thisGameData._Guns.Add(_gun);
        }

        _thisGameData._worlds = new List<World>();
        string[] worldnames = new string[] { "Desert", "Forest", "Snowy" };
        int[] worldLevels = new int[] { 25, 15, 10 };

        for (int i = 0; i < 3; i++)
        {
            World _world = new World();
            _world.worldno = i;
            _world.worldname = worldnames[i];
            if (i == 0)
            {
                _world._islocked = false;
            }
            else
            {
                _world._islocked = true;
            }
            _world.Levels = new List<Level>();
            for (int j = 0; j < _world.Levels.Count; j++)
            {
                Level _level = new Level();
                _level.levelno = j + 1;
                _level.starsCollected = 0;
                if (test && i == 0)
                {
                    if (j < 6)
                    {
                        _level._levelstatus = levelStatus.completed;
                    }
                    else if (j == 6)
                    {
                        _level._levelstatus = levelStatus.current;
                    }
                    else
                    {
                        _level._levelstatus = levelStatus.Locked;
                    }
                }
                else
                {
                    if (i == 0 && j == 0)
                    {
                        _level._levelstatus = levelStatus.current;
                    }
                    else
                    {
                        _level._levelstatus = levelStatus.Locked;
                    }
                }
                _world.Levels.Add(_level);
            }
            _thisGameData._worlds.Add(_world);
            
        }
        _thisGameData.UIcontrols = true;
    }

}


[Serializable]
public class GameData
{
    public int TotalCoinsCollected;
    public List<GunData> _Guns;
    public List<World> _worlds;
    public float sensitivity = 1;
    public bool UIcontrols = false;
    public bool removeAds = false;
    public int mcounter = 0;
    public int ccounter = 0;
}


[Serializable]
public class GunData
{
    public int GunNo;
    public int gunPrice;
    public GunStatus gunStatus;
}

[Serializable]
public class World
{
    public int worldno;
    public string worldname;
    public bool _islocked;
    public List<Level> Levels;
}


[Serializable]
public class Level
{
    public int levelno;
    public int starsCollected;
    public levelStatus _levelstatus;
}

public enum levelStatus
{
    current = 0,
    Locked = 1,
    completed = 2
}

public enum GunStatus
{
    Equipped = 0,
    Locked = 1,
    Unlocked = 2
}

public enum GameState
{
    menupanel,
    worldpanel,
    levelPanel,
    gameStart,
    pausePanel,
    gameRevive,
    gameFinish,
    gameFail
}