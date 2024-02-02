using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        private int costNumber = 0;
        [SerializeField] private Text level, costText;
        [SerializeField] private Button buyButton;

        public void Initialise()
        {
            upgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);

            if (savedLevel >= asset.costByLevel.Length)
            {
                level.text += $"Lvl: {savedLevel} (Max)";
                buyButton.interactable = false;
                buyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyButton.transform.Find("Text").gameObject.SetActive(false);
                costText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                level.text = $"Lvl: {savedLevel + 1}";
                costNumber = asset.costByLevel[savedLevel];
                costText.text = costNumber.ToString();
            }
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialise();
        }

        public void CheckCost(int money)
        {
            buyButton.interactable = (money >= costNumber);
        }
    }
}
