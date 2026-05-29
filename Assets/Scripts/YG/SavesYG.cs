using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int gold = 100;
        public int score = 0;
        public string lastOpenedLevelId = "";
        public string lastOpenedGoodId = "";
        public List<string> boughtSquads = new();
        public float soundUI = 1f;
        public float soundSFX = 1f;
    }
}
