public class SquadContext
{
    public SquadContext(Squad squad, SquadPlan plan, (int x, int y) startCell)
    {
        Squad = squad;
        Plan = plan;
        StartCell = startCell;
        CreateUpgraded = false;
    }

    public Squad Squad { get; }
    public SquadPlan Plan { get; }
    public (int x, int y) StartCell { get; private set; }
    public bool CreateUpgraded { get; private set; }

    public void Upgrade()
    {
        CreateUpgraded = true;
        Squad.Upgrade();
    }

    internal void UpdatePosition((int x, int y) startCell)
    {
        StartCell = startCell;
    }
}
