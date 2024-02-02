using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefence
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resultImages;

        public bool IsComplete { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

        //[SerializeField] private Text m_text;
        public void LoadLevel()
        {    
            LevelSequenceController.Instance.StartEpisode(m_episode);                
        }

        public int Initialise()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_episode);
            resultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                resultImages[i].color = Color.white;
            }
            return score;
        }
    }
}
