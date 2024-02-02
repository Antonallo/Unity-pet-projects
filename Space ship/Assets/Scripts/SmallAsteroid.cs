using UnityEngine;

namespace SpaceShooter
{
    public class SmallAsteroid : MonoBehaviour
    {
        [SerializeField] private Destructible smallAsteroidPrefab;

        [SerializeField] private int m_NumSmallAsteroids;

        [SerializeField] private float m_RandomSpeed;

        public void SpawnSmallAsteroids()
        {
            for (int i = 0; i < m_NumSmallAsteroids; i++)
            {
                GameObject smallAsteroid = Instantiate(smallAsteroidPrefab.gameObject);
                smallAsteroid.transform.position = gameObject.transform.position + Random.onUnitSphere;

                Rigidbody2D rb = smallAsteroid.GetComponent<Rigidbody2D>();

                if (rb != null && m_RandomSpeed > 0)
                {
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
                }
            }

        }
    }
}
