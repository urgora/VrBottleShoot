using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextmeshPro : MonoBehaviour
    {
        public string LocalizationKey;
        public TMP_FontAsset _Eng_latin, _Russian,_TrueEng;
        public Material _engLatMat, _russianMat,_trueEngmat;
        public void Start()
        {
            _Eng_latin = Resources.Load("English_Latin_SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
            _Russian = Resources.Load("Russian_SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
            _TrueEng = Resources.Load("Rajdhani-Regular SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;

            _engLatMat = _Eng_latin.material;
            _russianMat = _Russian.material;
            _trueEngmat = _TrueEng.material;
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            if (this.gameObject != null)
            {
                if (LocalizationManager.FONTID == 0)
                {
                    GetComponent<TextMeshProUGUI>().font = _Eng_latin;
                    GetComponent<TextMeshProUGUI>().fontSharedMaterial = _engLatMat;
                }
                else if (LocalizationManager.FONTID == 1)
                {
                    GetComponent<TextMeshProUGUI>().font = _Russian;
                    GetComponent<TextMeshProUGUI>().fontSharedMaterial = _russianMat;

                }
                else if (LocalizationManager.FONTID == 2)
                {
                    GetComponent<TextMeshProUGUI>().font = _TrueEng;
                    GetComponent<TextMeshProUGUI>().fontSharedMaterial = _trueEngmat;

                }
                GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(LocalizationKey);
            }
        }

     

        public void doupdatetext(string t)
        {
            LocalizationKey = t;
            LocalizationManager.LocalizationChanged -= Localize;
            Start();
        }
    }
}