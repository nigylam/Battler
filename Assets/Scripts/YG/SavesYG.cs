using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int gold = 100;
        public int score = 0;
        public string lastOpenedLevelId = "";
        public string lastOpenedGoodId = "";
        public string boughtSquadsDictionary = "";
        public float soundUI = 1f;
        public float soundSFX = 1f;

        public void AddBoughtSquad(string squadPlanId)
        {
            Dictionary<string, int> boughtSquads = GetBoughtSquadsId();

            if(boughtSquads.ContainsKey(squadPlanId))
                boughtSquads[squadPlanId] += 1;
            else
                boughtSquads.Add(squadPlanId, 1);

            string newBoughtSquads = "";

            foreach(string squadPlan in boughtSquads.Keys)
            {
                newBoughtSquads += squadPlan;
                newBoughtSquads += ",";
                newBoughtSquads += boughtSquads[squadPlan];
                newBoughtSquads += ";";
            }

            boughtSquadsDictionary = newBoughtSquads;
        }

        public Dictionary<string, int> GetBoughtSquadsId()
        {
            Dictionary<string, int> dictionary = boughtSquadsDictionary
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(item => item.Split(','))
            .Where(parts => parts.Length == 2)
            .ToDictionary(
                parts => parts[0],
                parts => int.Parse(parts[1])
            );

            return dictionary;
        }
    }
}
