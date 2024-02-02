using UnityEngine;


namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";
        public static string InstantDeatgTag = "QuickDeath";

        [SerializeField] private float m_VelocityDamageModifier;

        [SerializeField] private float m_DamageConstant;

        [SerializeField] private Player m_Player;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;
            if (collision.transform.tag == InstantDeatgTag) Player.Instance.NumLives = 0;


            var destructible = transform.root.GetComponent<Destructible>();

            if(destructible != null)
            {
                destructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}
