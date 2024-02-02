using SpaceShooter;
using System;
using UnityEngine;

namespace TowerDefence
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        totalScore += levelScore - item.score;
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, completionData);
                    }
                }
            }
        }

        [SerializeField] private EpisodeScore[] completionData;
         private int totalScore;
        public int TotalScore { get { return totalScore; } }

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
            foreach (var episodeScore in completionData)
            {
                totalScore += episodeScore.score;
            }
        }

        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in completionData)
            {
                if (data.episode == m_episode)
                {
                    return data.score;
                }
            }
            return 0;
        }
    }
}
