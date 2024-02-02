using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using SpaceShooter;
using System.Collections;
using Unity.VisualScripting;

namespace TowerDefence
{
    public class Abilities : MonoSingleton<Abilities>
    {

        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost = 5;
            public int Cost => m_Cost;
            [SerializeField] private int m_Damage = 2;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private UpgradeAsset requiredUpgrade;
            [SerializeField] private int requiredUpgradeLevel;
            public bool IsAvailable() => !requiredUpgrade ||
            requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);

            public void Use() 
            {
                if (TDPlayer.Instance.Gold >= Cost)
                {
                    Instance.m_FireButton.interactable = true;
                    TDPlayer.Instance.ChangeGold(-Cost);
                    if (TDPlayer.Instance.Gold < Cost)
                        Instance.m_FireButton.interactable = false;
                }
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
                
            }
        }

        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_Cost = 10;
            public int Cost => m_Cost;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 7f;
            [SerializeField] private UpgradeAsset requiredUpgrade;
            [SerializeField] private int requiredUpgradeLevel;
            public bool IsAvailable() => !requiredUpgrade ||
            requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
            public void Use() 
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                if (TDPlayer.Instance.Gold >= Cost)
                {
                    Instance.m_TimeButton.interactable = true;
                    TDPlayer.Instance.ChangeGold(-Cost);
                    if (TDPlayer.Instance.Gold < Cost)
                        Instance.m_TimeButton.interactable = false;
                }
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                }
                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    if (TDPlayer.Instance.Gold >= Cost)
                        Instance.m_TimeButton.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());                            
            }
        }
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        private void Start()
        {
            m_FireButton.interactable = m_FireAbility.IsAvailable();
            m_TimeButton.interactable = m_TimeAbility.IsAvailable();
        }
    }
}
