using Battler.Battle.DragAndDrop;
using Battler.Battle.Squads;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.Battle.Armies
{
    public class ArmyCommander
    {
        private readonly Army _army;
        private readonly Field _field;

        private List<SquadFieldContext> _spawnedSquads = new();
        private List<SquadFieldContext> _survivedSquads = new();

        public event Action SurvivedUpgraded;
        public event Action FieldCleared;
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
            _army.Clear();
            _field.Clear();
            ClearSpawned();
            _survivedSquads.Clear();
        }

        public void ClearRound()
        {
            _army.Clear();
            _field.Clear();
            ClearSpawned();
            FieldCleared?.Invoke();
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
            if (_survivedSquads.Count > 0)
                foreach (SquadFieldContext squad in _survivedSquads)
                    squad.Upgrade();

            SurvivedUpgraded?.Invoke();
        }

        public void GetSurvived()
        {
            _survivedSquads.Clear();

            foreach (var squad in _army.AliveSquads)
                foreach (var squadContext in _spawnedSquads)
                    if (squad == squadContext.Squad)
                        _survivedSquads.Add(squadContext);
        }

        public void Remove(SquadFieldContext context)
        {
            _spawnedSquads.Remove(context);
            _army.Remove(context.Squad);
            context.Squad.DragStarted -= OnDragStarted;
        }

        public void UpdateSquadPosition(SquadFieldContext context, (int x, int y) startCell)
        {
            context.UpdatePosition(startCell);
        }

        private void ClearSpawned()
        {
            foreach (SquadFieldContext squadContext in _spawnedSquads)
            {
                squadContext.Squad.DragStarted -= OnDragStarted;
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