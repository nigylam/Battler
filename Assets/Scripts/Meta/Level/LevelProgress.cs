using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelProgress
{
    private readonly Dictionary<LevelConfig, Level> _levels = new Dictionary<LevelConfig, Level>();
    private readonly List<LevelConfig> _levelConfigs = new();

    public LevelProgress(List<LevelConfig> levels)
    {
        if (levels == null)
            throw new ArgumentNullException(nameof(levels));

        _levelConfigs.AddRange(levels);

        foreach (LevelConfig level in levels)
        {
            if (level == null)
                throw new ArgumentNullException(nameof(level));

            _levels.Add(level, new Level());
        }

        _levels[_levelConfigs[0]].SetOpened();
    }

    public void SetCompleted(LevelConfig levelConfig)
    {
        if (levelConfig == null)
            throw new ArgumentNullException(nameof(levelConfig));

        if (_levels.Keys.Contains(levelConfig) == false)
            throw new InvalidOperationException(nameof(SetCompleted));

        _levels[levelConfig].SetCompleted();
        LevelConfig nextLevel = _levelConfigs[_levelConfigs.IndexOf(levelConfig) + 1];
        _levels[nextLevel].SetOpened();
    }

    public bool Completed(LevelConfig level)
        => _levels[level].Completed;

    public bool Opened(LevelConfig level)
        => _levels[level].Opened;
}
