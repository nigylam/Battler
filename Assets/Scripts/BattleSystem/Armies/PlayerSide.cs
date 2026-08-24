using Battler.BattleSystem.DragAndDrop;
using Battler.UI.BattleView;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Battler.BattleSystem.Armies
{
    public class PlayerSide : Side
    {
        [SerializeField] private BattleMenu _battleMenu;
        [SerializeField] private DragVisualSpawner _dragVisualSpawner;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _thisCanvas;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _startDragSound;
        [SerializeField] private AudioClip _menuPlaceSound;

        private SquadPlacer _placer;
        private PlayerArmyDeployer _deployer;

        public event Action ReadyForRound;
        public event Action SquadsEnded;

        protected override ArmyDeployer ArmyDeployer => _deployer;

        public override void StartLevel(GameContext context)
        {
            base.StartLevel(context);
            BattleSquadKeeper battleSquadKeeper = new(context.SquadKeeper);
            _battleMenu.SetSquads(battleSquadKeeper);
            _deployer.Set(battleSquadKeeper);
        }

        public override async UniTask SpawnSquads()
        {
            await _deployer.RespawnSurvived();
        }

        public override async UniTask PlayWin()
        {
            Commander.UpgradeSurvived();
            await base.PlayWin();
        }

        public override void EndLevel()
        {
            base.EndLevel();
            _deployer.DisablePlacing();
        }

        public void EnablePlacing()
        {
            _deployer.EnablePlacing();
        }

        protected override void OnAwake()
        {
            _placer = new SquadPlacer(_camera, _groundMask, _thisCanvas);

            _deployer = new PlayerArmyDeployer
            (
                _battleMenu, 
                _placer, 
                Field, 
                Commander, 
                _dragVisualSpawner, 
                Creator, 
                transform,
                _audioSource,
                _startDragSound,
                _menuPlaceSound
            );
        }

        protected override void Enable()
        {
            _deployer.DeploymentFinished += OnDeploymentFinished;
            _deployer.SquadsEnded += OnSquadsEnded;
        }

        protected override void Disable()
        {
            _deployer.DeploymentFinished -= OnDeploymentFinished;
            _deployer.SquadsEnded -= OnSquadsEnded;
        }

        private void OnDeploymentFinished()
        {
            _deployer.DisablePlacing();
            ReadyForRound?.Invoke();
        }

        private void OnSquadsEnded()
        {
            SquadsEnded.Invoke();
        }
    }
}