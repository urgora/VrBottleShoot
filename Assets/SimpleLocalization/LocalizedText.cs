using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            GetComponent<Text>().text = LocalizationManager.Localize(LocalizationKey);
        }
        private void Localize2()
        {
            GetComponent<Text>().text = LocalizationManager.Localize(LocalizationKey);
        }
        public void doupdatetext(string t,string k=null)
        {
            LocalizationKey = t;
            LocalizationManager.LocalizationChanged -= Localize;
            Start();
            GetComponent<Text>().text = GetComponent<Text>().text + k;
        }
    }
}