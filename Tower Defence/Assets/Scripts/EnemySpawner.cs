using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{

    public class EnemySpawner : Spawner
    {
        /// <summary>
        /// Ссылка на префаб врага
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path m_path;
        [SerializeField] private EnemyAsset[] m_EnemyAssets;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EnemyPrefab);
            e.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);
            e.GetComponent<TDPatrolController>().SetPath(m_path);
            return e.gameObject;
        }

    }
}