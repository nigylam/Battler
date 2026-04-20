using System;
using System.Collections;
using UnityEngine;

namespace Battler.Battle.Armies
{
    public abstract class Side : MonoBehaviour
    {
        [SerializeField] private Army _army;
        [SerializeField] private Field _field;
        [SerializeField] private SquadCreator _creator;

        private float _roundPause = 1f;
        private Coroutine _setRound;

        public event Action ReadyForRound;
        public event Action RoundEnded;
        public event Action WinRound;

        protected Field Field => _field;
        protected ArmyCommander Commander { get; private set; }
        protected SquadCreator Creator => _creator;
        protected abstract ArmyDeployer ArmyDeployer { get; }

        private void Awake()
        {
            Commander = new ArmyCommander(_army, _field);
            OnAwake();
        }

        private void OnEnable()
        {
            _army.WinRound += OnWinRound;
            Commander.FieldCleared += OnFieldCleared;
            ArmyDeployer.DeploymentFinished += OnDeploymentFinished;
            Enable();
        }

        private void OnDisable()
        {
            _army.WinRound -= OnWinRound;
            Commander.FieldCleared -= OnFieldCleared;
            ArmyDeployer.DeploymentFinished -= OnDeploymentFinished;
            Disable();
        }

        public void PrepareToRound()
        {
            SetRoundBeforePause();

            if (_setRound != null)
                StopCoroutine(_setRound);

            _setRound = StartCoroutine(RoundSetting());
        }

        public void StartRound()
        {
            Commander.Attack();
        }

        public void EndRound()
        {
            EndRoundPhase1();
        }

        public virtual void EndLevel()
        {
            Commander.Clear();
        }

        public virtual void StartLevel(GameContext context) { }

        protected virtual void OnAwake() { }
        protected virtual void Enable() { }
        protected virtual void Disable() { }
        protected virtual void SetRoundBeforePause() { }
        protected abstract void SetRoundAfterPause();

        protected virtual void EndRoundPhase1()
        {
            Commander.GetSurvived();
        }

        protected void EndRoundPhase2()
        {
            Commander.ClearField();
        }

        private void OnDeploymentFinished()
        {
            ReadyForRound?.Invoke();
        }

        private void OnWinRound()
        {
            WinRound?.Invoke();
        }

        private void OnFieldCleared()
        {
            RoundEnded?.Invoke();
        }

        private IEnumerator RoundSetting()
        {
            float time = 0;

            while (time < _roundPause)
            {
                time += Time.deltaTime;
                yield return null;
            }

            SetRoundAfterPause();
        }
    }
}