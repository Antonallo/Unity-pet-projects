using TMPro;
using UnityEngine;

namespace TowerDefence
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Life }
        public UpdateSource source = UpdateSource.Gold;
        private TMP_Text m_text;

        void Start()
        {
            m_text = GetComponent<TMP_Text>();
            switch (source)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
            }

        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }
    }
}
