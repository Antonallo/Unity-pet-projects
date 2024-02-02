using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public enum TypeOfShip
    {
        NotPlayer,
        Player
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Mass for RigidBody
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Pushing forward force
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotating force
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Max linear speed
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity { get { return m_MaxLinearVelocity; } set { m_MaxLinearVelocity = value; }}

        /// <summary>
        /// Max rotational speed. Degree/second
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// Saved link to Rigidbody
        /// </summary>
        private Rigidbody2D m_Rigid;

        private bool m_Accelerated = false;
        public bool Accelerated { get { return m_Accelerated; } set { m_Accelerated = value; } }

        private float m_TimerOfBoost;
        public float TimerOfBoost { get { return m_TimerOfBoost; } set { m_TimerOfBoost = value; } }

        private float m_ShipSpeedMultiplier;
        public float ShipSpeedMultiplier { get { return m_ShipSpeedMultiplier; } set { m_ShipSpeedMultiplier = value; } }

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        #region Public API

        /// <summary>
        /// Linear thrust control. -1.0 to +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotational thrust control. -1.0 to +1.0
        /// </summary>
        public float TorqueControl { get; set; }


        #endregion


        #region Unity Event
        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }


        private void FixedUpdate()
        {
            UpdateRigidBody();

            UpdateEnegyRegen();

            if (m_Accelerated == true)
            {                             
                m_TimerOfBoost -= Time.deltaTime;
              
                if (m_TimerOfBoost <= 0)
                {
                    m_MaxLinearVelocity /= m_ShipSpeedMultiplier;
                    m_Accelerated = false;
                }
            }
        }

        #endregion

        /// <summary>
        /// Method of adding forces to ship for movement
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce( -m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnegyRegen()
        {
            m_PrimaryEnergy += (float) m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }

        public bool DrawAmmo(int count)
        {
            if(count == 0)
                return true;

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }


    }
}


