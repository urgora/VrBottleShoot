using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.SimpleLocalization;
public class LevelManager : MonoBehaviour
{
    [Header("Define number of Worlds & Levels")]
    public ScrollRect WorldsParentObject;
    public GameObject WorldPrefab,levelPrefab,levelspanelprefab;

    public GameData _GD;
    public Sprite[] _worldimg;
    public GameObject WorldPanel;
    public List<ScrollRect> levelspanel;
    public GameObject LevelScrollsParent;

    // Start is called before the first frame update
    void Start()
    {
        _GD = Datamanager._instance._thisGameData;
        for (int i = 0; i < _GD._worlds.Count; i++)
        {

            GameObject worldBtn = Instantiate(WorldPrefab, gameObject.transform.parent) as GameObject;
            worldBtn.transform.parent = WorldsParentObject.content;
            ///world unlock basing on datamanager value stored

            worldBtn.GetComponent<WorldPick>().worldno = _GD._worlds[i].worldno;
            //.GetComponent<LocalizedTextmeshPro>().doupdatetext("ObjectivePanel.Objective");
            //worldBtn.GetComponent<WorldPick>().WorldTitle.text = _GD._worlds[i].worldname;
            worldBtn.GetComponent<WorldPick>().WorldTitle.GetComponent<LocalizedTextmeshPro>().doupdatetext("WorldsPanel_name"+i);
            worldBtn.GetComponent<WorldPick>()._wrldImg.sprite = _worldimg[i];
            worldBtn.GetComponent<WorldPick>().worldselection = WorldPanel;
            if (!_GD._worlds[i]._islocked)
            {
                worldBtn.GetComponent<WorldPick>().Lock.gameObject.SetActive(false);
            }
            else
            {
                worldBtn.GetComponent<WorldPick>().Lock.gameObject.SetActive(true);
            }

            ///level panel generation
            if (!_GD._worlds[i]._islocked)
            {
                GameObject LvlPanel = Instantiate(levelspanelprefab, LevelScrollsParent.transform) as GameObject;
                LvlPanel.SetActive(false);
                levelspanel.Add(LvlPanel.GetComponent<ScrollRect>());
                GenerateLevels(_GD._worlds[i].worldno);
            }
        }
    }

    public void GenerateLevels(int index)
    {
        for (int j = 0; j < _GD._worlds[index].Levels.Count; j++)
        {
            if (Camera.main.aspect > 1.5)//mobiles
            {
                levelspanel[index].content.gameObject.GetComponent<GridLayoutGroup>().constraintCount = 6;
            }
            else
            {
                levelspanel[index].content.gameObject.GetComponent<GridLayoutGroup>().constraintCount = 5;
            }
            GameObject _level = Instantiate(levelPrefab, levelspanel[index].content);
            LevelButtonPick _LBPicked = _level.GetComponent<LevelButtonPick>();
            _LBPicked.levelno = _GD._worlds[index].Levels[j].levelno;
            _LBPicked.worldno = _GD._worlds[index].worldno + 2;
            _LBPicked.UIUpdate();
            _LBPicked.completed.gameObject.SetActive(_GD._worlds[index].Levels[j]._levelstatus == levelStatus.completed ? true : false);
            _LBPicked.Lock.gameObject.SetActive(_GD._worlds[index].Levels[j]._levelstatus==levelStatus.Locked?true:false);
            //based on star count show star img active "forloop"
            for(int i = 0; i < _GD._worlds[index].Levels[j].starsCollected; i++)
            {
                _LBPicked.starImgs[i].SetActive(true);
            }

        }
    }

    public void Levelclick(LevelButtonPick LBP)
    {
        if (LBP.Lock.gameObject.activeInHierarchy)
        {
           // LBP._lock.Play();
        }
        else
        {
            WorldPanel.SetActive(false);
            levelspanel[LBP.worldno - 1].gameObject.SetActive(true);
        }
    }
}



