using SpaceShooter;
using System;
using UnityEngine;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 4;
        private Turret[] turrets;
        private Destructible target = null;
        private int m_auraDamage = 0;

        [SerializeField] private UpgradeAsset towerUpgradeRange;
        [SerializeField] private UpgradeAsset towerUpgradeMagicAura;
        private void Awake()
        {
            var levelRange = Upgrades.GetUpgradeLevel(towerUpgradeRange);
            m_auraDamage = Upgrades.GetUpgradeLevel(towerUpgradeMagicAura);
            m_Radius += (levelRange * 0.5f);
        }

        private void Update()
        {
            if (target)
            {
                
                if (Vector3.Distance(target.transform.position, transform.position) <= m_Radius)
                {
                    target.ChangeHitPoints(m_auraDamage * Time.deltaTime);
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position - turret.transform.position;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }

            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        public void Use(TowerAsset m_ta)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = m_ta.sprite;
            turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(m_ta.turretProperties);
            }
            GetComponentInChildren<BuildSite>().SetBuildableTowers(m_ta.m_UpgradesTo);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}

