using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupBoost : Powerup
    {
        [SerializeField] private float m_accelerationMultiplier;

        [SerializeField] private float Timer;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.TimerOfBoost = Timer;

            if (ship.Accelerated == false)
            {
                ship.MaxLinearVelocity *= m_accelerationMultiplier;
                ship.ShipSpeedMultiplier = m_accelerationMultiplier;
                ship.Accelerated = true;
            }             
        }
    }
}
