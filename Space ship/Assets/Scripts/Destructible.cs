using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Destructable object on scene. Entity that has hitpoints
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Object is ignoring damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructable => m_Indestructible;

        public bool ChangeIndestructible
        {
            set { m_Indestructible = value; }
        }

        public TypeOfShip TypeOfShip { get; set; }

        /// <summary>
        /// Start amount of hitpoints
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Current hitpoints
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_HitPoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        #endregion

        #region Public API


        /// <summary>
        /// Applying damage to object
        /// </summary>
        /// <param name="damage"> Damage to object</param> 
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        #endregion

        /// <summary>
        /// Destruction of game object when hitpoints <= 0
        /// </summary>
        protected virtual void OnDeath()
        {          
            m_EventOnDeath?.Invoke();
            Destroy(gameObject);
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }


        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #region Score

        [SerializeField] int m_ScoreValue;

        public int ScoreValue => m_ScoreValue;

        #endregion
    }
}