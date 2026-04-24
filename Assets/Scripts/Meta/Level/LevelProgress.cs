using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

namespace Battler.Meta
{
    public class LevelProgress
    {
        private readonly Dictionary<LevelConfig, Level> _levels = new Dictionary<LevelConfig, Level>();
        private readonly List<LevelConfig> _levelConfigs = new();

        public LevelProgress(List<LevelConfig> levels, List<LevelConfig> openedLevels)
        {
            if (levels == null)
                throw new ArgumentNullException(nameof(levels));

            if(openedLevels == null)
                throw new ArgumentNullException(nameof(openedLevels));

            _levelConfigs.AddRange(levels);

            foreach (LevelConfig level in levels)
            {
                if (level == null)
                    throw new ArgumentNullException(nameof(level));

                _levels.Add(level, new Level());
            }

            _levels[_levelConfigs[0]].SetOpened();

            foreach (LevelConfig level in openedLevels)
            {
                if(level == null)
                    throw new ArgumentNullException(nameof(level));

                _levels[level].SetOpened();
                int previousLevelIndex = _levelConfigs.IndexOf(level) - 1;

                if (previousLevelIndex >= 0)
                    _levels[_levelConfigs[previousLevelIndex]].SetCompleted();
            }
        }

        public bool AllLevelsCompleted { get; private set; }

        public void SetCompleted(LevelConfig levelConfig)
        {
            if (levelConfig == null)
                throw new ArgumentNullException(nameof(levelConfig));

            if (_levels.Keys.Contains(levelConfig) == false)
                throw new InvalidOperationException(nameof(SetCompleted));

            _levels[levelConfig].SetCompleted();

            int nextLevelIndex = _levelConfigs.IndexOf(levelConfig) + 1;

            if (nextLevelIndex < _levelConfigs.Count)
            {
                LevelConfig nextLevel = _levelConfigs[nextLevelIndex];
                _levels[nextLevel].SetOpened();
                YG2.saves.openedLevels.Add(nextLevel);
                YG2.SaveProgress();
            }
            else
            {
                AllLevelsCompleted = true;
            }
        }

        public bool Completed(LevelConfig level)
            => _levels[level].Completed;

        public bool Opened(LevelConfig level)
            => _levels[level].Opened;
    }
}