using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.Battle.Armies
{
    public class Army : MonoBehaviour
    {
        [SerializeField] private Army _enemyArmy;

        private List<Squad> _squads = new();

        public event Action LoseRound;
        public event Action WinRound;
        public IReadOnlyCollection<Squad> AliveSquads => _squads;

        private void OnEnable()
        {
            _enemyArmy.LoseRound += OnEnemyArmyLose;

            foreach (var squads in _squads)
                squads.Dead += OnSquadDead;
        }

        private void OnDisable()
        {
            _enemyArmy.LoseRound -= OnEnemyArmyLose;

            foreach (var squads in _squads)
                squads.Dead -= OnSquadDead;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (gameObject.layer == 7)
                    KillAll();
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                Debug.Log("key up");

                if (gameObject.layer == 6)
                {
                    Debug.Log("kill");

                    KillAll();
                }
            }
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
            {
                squad.Attack(_enemyArmy);
            }
        }

        public List<Unit> GetTargets()
        {
            List<Unit> targets = new();

            foreach (var unit in _squads)
                targets.AddRange(unit.GetAliveMembers());

            return targets;
        }

        private void OnSquadDead(Squad squad)
        {
            _squads.Remove(squad);
            squad.Dead -= OnSquadDead;

            if (_squads.Count == 0)
                LoseRound?.Invoke();
        }

        private void OnEnemyArmyLose()
        {
            foreach (var unit in _squads)
                unit.Win();

            WinRound?.Invoke();
        }
    }
}