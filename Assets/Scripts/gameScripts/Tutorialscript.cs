using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorialscript : MonoBehaviour
{
    public Text TutorialText;
      // Start is called before the first frame update
    void Start()
    {
        TutorialText.text = getText();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    string getText()
    {
        string txt = "Drag and Release to fire the bottles";
        /*if (Application.systemLanguage == SystemLanguage.French)
        {
            txt = "Touchez et faites glisser n'importe où sur l'écran ,, \n Relâchez pour tirer ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Arabic)
        {
            TutorialText.GetComponent<Text>().font = mFont;
            TutorialText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            txt = "المس واسحب في أي مكان على الشاشة ،، \n الافراج عن لاطلاق النار ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Dutch)
        {
            txt = "Tik en sleep ergens op het scherm ,,\nLaat los om te schieten ..";
        }
        else if (Application.systemLanguage == SystemLanguage.German)
        {
            txt = "Berühre und ziehe irgendwo auf dem Bildschirm ,,\nFreigabe um zu schießen ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Italian)
        {
            txt = "Tocca e trascina ovunque sullo schermo ,,\nRilascio per sparare ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Japanese)
        {

            TutorialText.GetComponent<Text>().font = mFont;
            TutorialText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            txt = "画面上の任意の場所に触れてドラッグ\n撮影するリリース。";
        }
        else if (Application.systemLanguage == SystemLanguage.Polish)
        {
            txt = "Dotknij i przeciągnij w dowolne miejsce na ekranie ,,\nZwolnij, aby strzelać ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            txt = "Toque e arraste em qualquer lugar na tela,\nSolte para atirar ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Russian)
        {
            txt = "Нажмите и перетащите в любом месте на экране ,,\nОтпустите, чтобы стрелять ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Spanish)
        {
            txt = "Toca y arrastra a cualquier parte de la pantalla,\nSuelte para disparar ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Turkish)
        {
            txt = "Ekranda herhangi bir yere dokunun ve sürükleyin.\nAteş etmek için serbest bırakın ..";
        }
        else if (Application.systemLanguage == SystemLanguage.Chinese)

        {
            TutorialText.GetComponent<Text>().font = mFont;
            TutorialText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            txt = "触摸并拖动屏幕上的任意位置，\n发布拍摄..";
        }
        else if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            txt = "Chạm và kéo bất cứ nơi nào trên màn hình ,,\nPhát hành để bắn ..";
        }*/
        return txt;
    }

    }
