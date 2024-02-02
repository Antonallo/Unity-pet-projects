using SpaceShooter;
using UnityEngine;
using System;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        [SerializeField] private Tower m_towerPrefab;
        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }
        private event Action<int> OnGoldUpdate;

        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }

        public event Action<int> OnLifeUpdate;

        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        [SerializeField] private int m_gold = 0;
        public int Gold => m_gold;
        public int GetGold() { return m_gold; }

        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        public void TryBuild(TowerAsset m_ta, Transform m_buildSite)
        {
            ChangeGold(-m_ta.goldCost);
            var tower = Instantiate(m_towerPrefab, m_buildSite.position, Quaternion.identity);
            tower.Use(m_ta);
            Destroy(m_buildSite.gameObject);
        }

        [SerializeField] private UpgradeAsset healthUpgrade;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
        }
    }
}
