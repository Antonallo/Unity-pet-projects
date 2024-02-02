using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;
        private float timeToNextWave;

        private EnemyWaveManager m_manager;

        private void Start()
        {
            m_manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        public void CallWave()
        {
            m_manager.ForceNextWave();
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
            bonusAmount.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }
    }
}
