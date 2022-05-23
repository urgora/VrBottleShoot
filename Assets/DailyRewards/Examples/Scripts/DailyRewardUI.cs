/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.SimpleLocalization;
using System.Collections;
/* 
 * Daily Reward Object UI representation
 */
namespace NiobiumStudios
{
    /** 
     * The UI Representation of a Daily Reward.
     * 
     *  There are 3 states:
     *  
     *  1. Unclaimed and available:
     *  - Shows the Color Claimed
     *  
     *  2. Unclaimed and Unavailable
     *  - Shows the Color Default
     *  
     *  3. Claimed
     *  - Shows the Color Claimed
     *  
     **/
    public class DailyRewardUI : MonoBehaviour
    {
        public bool showRewardName;

        [Header("UI Elements")]
        public TextMeshProUGUI textDay;                // Text containing the Day text eg. Day 12
        public TextMeshProUGUI textReward;             // The Text containing the Reward amount
        public Image imageRewardBackground; // The Reward Image Background
        public Image imageReward;           // The Reward Image
        public Sprite colorClaim;            // The Color of the background when claimed
        public Sprite colorUnclaimed;       // The Color of the background when not claimed
        public Sprite colorclaimed;       // The Color of the background when not claimed

        [Header("Internal")]
        public int day;

        [HideInInspector]
        public Reward reward;

        public DailyRewardState state;

        // The States a reward can have
       

        void Awake()
        {
            colorUnclaimed = imageRewardBackground.sprite;
        }

        public void Initialize()
        {
            textDay.GetComponent<LocalizedTextmeshPro>().doupdatetext("Panel Daily Rewards.Day");
            textDay.text = textDay.text + " " + day.ToString();
            //textDay.text = string.Format("Day {0}", day.ToString());
            if (reward.reward > 0)
            {
                if (showRewardName)
                {
                    textReward.text = reward.reward + " " + reward.unit;
                }
                else
                {
                    textReward.text = reward.reward.ToString();
                }
            }
            else
            {
                textReward.text = reward.unit.ToString();
            }
            imageReward.sprite = reward.sprite;
        }

        // Refreshes the UI
        public void Refresh()
        {
            switch (state)
            {
                case DailyRewardState.UNCLAIMED_AVAILABLE:
                    imageRewardBackground.sprite = colorClaim;
                    imageRewardBackground.GetComponent<RectTransform>().localScale = new Vector3(2.5f,2.75f,1f);
                    break;
                case DailyRewardState.UNCLAIMED_UNAVAILABLE:
                    imageRewardBackground.sprite = colorUnclaimed;
                    imageRewardBackground.GetComponent<RectTransform>().localScale = Vector3.one;
                    break;
                case DailyRewardState.CLAIMED:
                    imageRewardBackground.sprite = colorclaimed;                    
                    imageRewardBackground.GetComponent<RectTransform>().localScale = Vector3.one;
                    break;
            }
        }
    }

     public enum DailyRewardState
        {
            UNCLAIMED_AVAILABLE,
            UNCLAIMED_UNAVAILABLE,
            CLAIMED
        }
}