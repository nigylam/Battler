using UnityEngine;

public class BattlePauseState : GameState
{
    public BattlePauseState(GameStateMachine stateMachine, GameContext context) : base(stateMachine, context) { }

    public override void Enter()
    {
        Context.BattlePauseMenu.gameObject.SetActive(true);
        Context.BattlePauseMenu.Resume += OnResumeClick;
        Context.BattlePauseMenu.Quit += OnQuitClick;
    }

    public override void Exit()
    {
        Context.BattlePauseMenu.Resume -= OnResumeClick;
        Context.BattlePauseMenu.Quit -= OnQuitClick;
        Context.BattlePauseMenu.gameObject.SetActive(false);
    }

    private void OnResumeClick()
    {
        Context.Battle.ResumeGame();
        StateMachine.PopState();
    }

    private void OnQuitClick()
    {
        StateMachine.ChangeState(GameStateType.LevelMap);
    }
}
