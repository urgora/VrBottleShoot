using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider musicslider, soundslider;
    public Toggle musictoggle, soundtoggle;
    public Toggle controlsToggle,touchcontrol;
    public bool _gameplayscene =false;


    void Start()
    {

        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
            SoundManager.MuteSFX(false);

            SoundManager.SetVolumeSFX(1f);
            PlayerPrefs.SetFloat("svol", 1f);
            soundtoggle.isOn = true;
            soundslider.value = PlayerPrefs.GetFloat("svol");
        }

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            SoundManager.MuteMusic(false);

            SoundManager.SetVolumeMusic(1f);
            PlayerPrefs.SetFloat("mvol", 1f);
            musictoggle.isOn = true;
            musicslider.value = PlayerPrefs.GetFloat("mvol");
        }

        if (!PlayerPrefs.HasKey("UIcontrols")) {
            PlayerPrefs.SetInt("UIcontrols", 1);
            Datamanager._instance._thisGameData.UIcontrols = true;
        }
        else
        {
            Datamanager._instance._thisGameData.UIcontrols = true ? PlayerPrefs.GetInt("UIcontrols") == 1 : PlayerPrefs.GetInt("UIcontrols") == 0;
        }
        updatetoggles();
    }

    private void OnEnable()
    {
        Invoke("updatetoggles", 1f);
    }

    
    public void updatetoggles() { 
            soundtoggle.isOn = true? PlayerPrefs.GetInt("sound")==1:PlayerPrefs.GetInt("sound")==0;
            soundChange(soundtoggle);
            SoundManager.SetVolumeSFX(PlayerPrefs.GetFloat("svol"));
            soundslider.value =SoundManager.GetVolumeSFX();
    
            musictoggle.isOn = true? PlayerPrefs.GetInt("music")==1:PlayerPrefs.GetInt("music")==0;
            musicChange(musictoggle);
            SoundManager.SetVolumeMusic(PlayerPrefs.GetFloat("mvol"));
            musicslider.value = SoundManager.GetVolumeMusic();

            controlsToggle.isOn =  true ? PlayerPrefs.GetInt("UIcontrols") == 1 : PlayerPrefs.GetInt("UIcontrols") == 0;
            touchcontrol.isOn = true ? controlsToggle.isOn == false : controlsToggle.isOn == true;
            //controlsChange(controlsToggle);
    }

        public void controlsChange(Toggle t)
            {
                if (t.isOn)
                {
                    Datamanager._instance._thisGameData.UIcontrols = true;
           
            PlayerPrefs.SetInt("UIcontrols", 1);
          

                }
                else
                {
                    Datamanager._instance._thisGameData.UIcontrols = false;
                    PlayerPrefs.SetInt("UIcontrols", 0);
                }
                if(_gameplayscene){
                    FindObjectOfType<GameManager>().updatecontrols();
                }
            } 

            public void soundChange(Toggle t)
        {
            if (t.isOn)
            {
                PlayerPrefs.SetInt("sound", 1);
                 SoundManager.MuteSFX(false);

            }
            else
            {
                PlayerPrefs.SetInt("sound", 0);
                SoundManager.MuteSFX(true);
            }
        }
        
        public void SoundVol(Slider s)
        {
        SoundManager.SetVolumeSFX(s.value);
        PlayerPrefs.SetFloat("svol", s.value);
        }

        public void musicvol(Slider s)
        {
        SoundManager.SetVolumeMusic(s.value);
        PlayerPrefs.SetFloat("mvol", s.value);
        }

        public void musicChange(Toggle t)
            {
                if (t.isOn)
                {
                    PlayerPrefs.SetInt("music", 1);
                    SoundManager.MuteMusic(false);

                }
                else
                {
                    PlayerPrefs.SetInt("music", 0);
                    SoundManager.MuteMusic(true);
                }
            }

    public void changeLanguage(Dropdown t) {
        if (LanguageManager._instance != null) {
            LanguageManager._instance.SetLocalization((SystemLanguage)int.Parse(t.captionText.text));
        }
    }
}
