using System;
using System.Collections.Generic;
using System.Linq;

public class LevelProgress
{
    private Dictionary<LevelConfig, Level> _levels;

    public LevelProgress(List<LevelConfig> levels)
    {
        if (levels == null)
            throw new ArgumentNullException(nameof(levels));

        _levels = new Dictionary<LevelConfig, Level>();

        foreach (LevelConfig level in levels)
        {
            if (level == null)
                throw new ArgumentNullException(nameof(level));

            _levels.Add(level, new Level());
        }
    }

    public void SetCompleted(LevelConfig levelConfig)
    {
        if (levelConfig == null)
            throw new ArgumentNullException(nameof(levelConfig));

        if (_levels.Keys.Contains(levelConfig) == false)
            throw new InvalidOperationException(nameof(SetCompleted));

        _levels[levelConfig].SetCompleted();
    }

    public bool Completed(LevelConfig level)
        => _levels[level].Completed;
}
