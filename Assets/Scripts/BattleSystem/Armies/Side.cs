using Battler.BattleSystem.Squads;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Battler.BattleSystem.Armies
{
    public abstract class Side : MonoBehaviour
    {
        private const float UpgradeAnimationTime = 2f;

        [SerializeField] private Army _army;
        [SerializeField] private Field _field;
        [SerializeField] private SquadCreator _creator;

        public CancellationToken AsyncCancelToken { get; private set; }
        public bool IsDead { get; private set; }

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
            Subscribe();
            Enable();
        }

        private void OnDisable()
        {
            Unsubscribe();
            Commander.ClearLevel();
            Disable();
        }

        public abstract UniTask SpawnSquads();

        public virtual void StartLevel(GameContext context)
        {
            AsyncCancelToken = context.Battle.LevelToken;
            ArmyDeployer.SetToken(AsyncCancelToken);
        }

        public virtual async UniTask PlayWin()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(UpgradeAnimationTime), cancellationToken: AsyncCancelToken);
        }

        public void StartRound()
        {
            Commander.Attack();
            IsDead = false;
        }

        public virtual void EndLevel()
        {
            Commander.ClearLevel();
        }

        public void Stop()
        {
            _army.Stop();
        }

        public void ClearField()
        {
            Commander.ClearRound();
        }

        protected virtual void OnAwake() { }
        protected virtual void Enable() { }
        protected virtual void Disable() { }

        private void OnDead()
        {
            IsDead = true;
        }

        private void Subscribe()
        {
            _army.Dead += OnDead;
        }

        private void Unsubscribe()
        {
            _army.Dead -= OnDead;
        }
    }
}