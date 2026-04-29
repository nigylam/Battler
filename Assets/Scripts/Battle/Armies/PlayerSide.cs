using Battler.Battle.DragAndDrop;
using Battler.UI.BattleView;
using UnityEngine;

namespace Battler.Battle.Armies
{
    public class PlayerSide : Side
    {
        [SerializeField] private BattleMenu _battleMenu;
        [SerializeField] private DragVisualSpawner _dragVisualSpawner;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _thisCanvas;

        private SquadPlacer _placer;
        private PlayerArmyDeployer _deployer;

        protected override ArmyDeployer ArmyDeployer => _deployer;

        public override void StartLevel(GameContext context)
        {
            BattleSquadKeeper battleSquadKeeper = new(context.SquadKeeper);
            _battleMenu.SetSquads(battleSquadKeeper);
            _deployer.Set(battleSquadKeeper);
        }

        protected override void OnAwake()
        {
            _placer = new SquadPlacer(_camera, _groundMask, _thisCanvas);
            _deployer = new PlayerArmyDeployer(_battleMenu, _placer, Field, Commander, _dragVisualSpawner, Creator, transform);
        }

        protected override void Enable()
        {
            _deployer.DeploymentFinished += OnDeploymentFinished;
            Commander.SurvivedUpgraded += OnSurvivedUpgraded;
        }

        protected override void Disable()
        {
            _deployer.DeploymentFinished -= OnDeploymentFinished;
            Commander.SurvivedUpgraded -= OnSurvivedUpgraded;
        }

        protected override void SetRoundBeforePause()
        {
            _deployer.EnablePlacing();
        }

        protected override void SetRoundAfterPause()
        {
            _deployer.RespawnSurvived();
        }

        protected override void EndRoundPhase1()
        {
            base.EndRoundPhase1();
            Commander.UpgradeSurvived();
        }

        private void OnDeploymentFinished()
        {
            _deployer.DisablePlacing();
        }

        private void OnSurvivedUpgraded()
        {
            EndRoundPhase2();
        }
    }
}