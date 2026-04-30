using Battler.Battle.Armies;
using Battler.Battle.DragAndDrop;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battler.Battle.Squads
{
    public class Squad : MonoBehaviour
    {
        private List<Unit> _units = new();
        private List<Unit> _unitsAlive = new();
        private Army _enemyArmy;

        private bool _isBattleEnded;

        public event Action<Squad> Dead;
        public event Action<Squad, UnitDragger> DragStarted;

        public IReadOnlyCollection<Unit> Units => _units;

        public void KillAll()
        {
            for (int i = _unitsAlive.Count - 1; i >= 0; i--)
                _unitsAlive[i].TakeDamage(100);
        }

        private void OnEnable()
        {
            if (_unitsAlive.Count > 0)
                foreach (Unit unit in _unitsAlive)
                    Subscribe(unit);
        }

        private void OnDisable()
        {
            if (_unitsAlive.Count > 0)
                foreach (Unit unit in _unitsAlive)
                    Unsubscribe(unit);
        }

        public void AddUnit(Unit unit)
        {
            _units.Add(unit);
            _unitsAlive.Add(unit);
            Subscribe(unit);
        }

        public void Win()
        {
            _isBattleEnded = true;

            foreach (var unit in _unitsAlive)
                unit.Win();
        }

        public void Upgrade()
        {
            foreach (var unit in _unitsAlive)
                unit.Upgrade();
        }

        public List<Unit> GetAliveMembers()
        {
            return _unitsAlive;
        }

        public void Attack(Army army)
        {
            _enemyArmy = army;

            foreach (Unit unit in _unitsAlive)
                unit.SetTarget(_enemyArmy.GetTargets());
        }

        public void HideVisuals()
        {
            foreach (Unit unit in _units)
                unit.HideVisual();
        }

        public void ShowVisuals()
        {
            foreach (Unit unit in _units)
                unit.ShowVisual();
        }

        private void OnUnitDead(Unit unit)
        {
            _unitsAlive.Remove(unit);
            Unsubscribe(unit);

            if (_unitsAlive.Count == 0)
            {
                _isBattleEnded = true;
                Dead?.Invoke(this);
            }
        }

        private void OnUnitDragStarted(UnitDragger dragger)
        {
            DragStarted.Invoke(this, dragger);
        }

        private void OnUnitFree(Unit unit)
        {
            if (_isBattleEnded)
                return;

            unit.SetTarget(_enemyArmy.GetTargets());
        }

        private void Subscribe(Unit unit)
        {
            unit.Dead += OnUnitDead;
            unit.Free += OnUnitFree;
            unit.Dragger.DragStarted += OnUnitDragStarted;
        }

        private void Unsubscribe(Unit unit)
        {
            unit.Dead -= OnUnitDead;
            unit.Free -= OnUnitFree;
            unit.Dragger.DragStarted -= OnUnitDragStarted;
        }
    }
}