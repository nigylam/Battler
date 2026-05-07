using Battler.BattleSystem.Squads;
using Battler.BattleSystem.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Armies
{
    public class Army : MonoBehaviour
    {
        [SerializeField] private Army _enemyArmy;

        private readonly List<Squad> _squads = new();

        public event Action Dead;

        public IReadOnlyCollection<Squad> Squads => _squads;

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
                if (gameObject.layer == 7)
                    KillAll();

            if (Input.GetKeyUp(KeyCode.D))
                if (gameObject.layer == 6)
                    KillAll();
        }

        private void KillAll()
        {
            for (int i = _squads.Count - 1; i >= 0; i--)
            {
                _squads[i].KillAll();
            }
        }

        public void Add(Squad squad)
        {
            _squads.Add(squad);
            squad.Dead += OnSquadDead;
        }

        public void Remove(Squad squad)
        {
            squad.Dead -= OnSquadDead;
            _squads.Remove(squad);
            Destroy(squad.gameObject);
        }

        public void Clear()
        {
            if (_squads.Count == 0)
                return;

            while (_squads.Count > 0)
            {
                Remove(_squads[0]);
            }
        }

        public void Attack()
        {
            foreach (var squad in _squads)
                squad.Attack(_enemyArmy);
        }

        public void Stop()
        {
            foreach(var squad in _squads)
                squad.Stop();
        }

        public List<Unit> GetTargets()
        {
            List<Unit> targets = new();

            foreach (var squad in _squads)
                targets.AddRange(squad.GetAliveMembers());

            return targets;
        }

        public void PlayWin()
        {
            foreach(var squad in _squads)
                squad.PlayWin();
        }

        private void OnSquadDead(Squad squad)
        {
            _squads.Remove(squad);
            squad.Dead -= OnSquadDead;

            if (_squads.Count == 0)
            {
                Dead?.Invoke();
            }
        }

        private void Subscribe()
        {
            foreach (var squad in _squads)
                squad.Dead += OnSquadDead;
        }

        private void Unsubscribe()
        {
            foreach (var squad in _squads)
            {
                squad.Dead -= OnSquadDead;
            }
        }
    }
}