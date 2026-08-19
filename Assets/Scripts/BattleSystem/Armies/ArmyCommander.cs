using Battler.BattleSystem.DragAndDrop;
using Battler.BattleSystem.Squads;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Armies
{
    public class ArmyCommander
    {
        private readonly Army _army;
        private readonly Field _field;
        private readonly List<SquadFieldContext> _spawnedSquads = new();
        private readonly List<SquadFieldContext> _survivedSquads = new();

        public event Action<SquadFieldContext, UnitDragger> DragStarted;

        public IReadOnlyCollection<SquadFieldContext> SurvivedSquads => _survivedSquads;

        public ArmyCommander(Army army, Field field)
        {
            _army = army;
            _field = field;
        }

        public void Attack()
        {
            _army.Attack();
        }

        public void ClearLevel()
        {
            ClearRound();
            _survivedSquads.Clear();
        }

        public void ClearRound()
        {
            GetSurvived();
            _army.Clear();
            _field.Clear();
            ClearSpawned();
        }

        public void Add(Squad squad, SquadPlan plan, (int x, int y) startCell, bool createUpgraded)
        {
            if (squad == null)
                throw new ArgumentNullException(nameof(squad));

            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            var squadContext = new SquadFieldContext(new SquadContext(plan, createUpgraded), squad, startCell);
            _spawnedSquads.Add(squadContext);
            _army.Add(squad);
            squad.DragStarted += OnDragStarted;
        }

        public void UpgradeSurvived()
        {
            GetSurvived();

            if (_survivedSquads.Count > 0)
            {
                foreach (SquadFieldContext squadContext in _survivedSquads)
                {
                    squadContext.Upgrade();
                    squadContext.Squad.Upgrade();
                }
            }
        }

        public void Remove(SquadFieldContext context)
        {
            _spawnedSquads.Remove(context);
            _army.Remove(context.Squad);
            context.Squad.DragStarted -= OnDragStarted;
        }

        public void PlayWin()
        {
            _army.PlayWin();
        }

        public void UpdateSquadPosition(SquadFieldContext context, (int x, int y) startCell)
        {
            context.UpdatePosition(startCell);
        }

        private void GetSurvived()
        {
            _survivedSquads.Clear();

            foreach (var squad in _army.Squads)
                foreach (var squadContext in _spawnedSquads)
                    if (squad == squadContext.Squad)
                        _survivedSquads.Add(squadContext);
        }

        private void ClearSpawned()
        {
            foreach (SquadFieldContext squadContext in _spawnedSquads)
            {
                squadContext.Squad.DragStarted -= OnDragStarted;
                UnityEngine.Object.Destroy(squadContext.Squad.gameObject);
            }

            _spawnedSquads.Clear();
        }

        private void OnDragStarted(Squad squad, UnitDragger dragger)
        {
            DragStarted?.Invoke(Get(squad), dragger);
        }

        private SquadFieldContext Get(Squad squad)
        {
            SquadFieldContext squadContext = null;

            foreach (var context in _spawnedSquads)
            {
                if (context.Squad == squad)
                    squadContext = context;
            }

            return squadContext;
        }
    }
}