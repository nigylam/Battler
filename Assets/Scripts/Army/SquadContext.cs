
public struct SquadContext
{
    public Squad Squad;
    public SquadPlan Plan;
    public (int x, int y) StartCell;

    public SquadContext(Squad squad, SquadPlan plan, (int x, int y) startCell)
    {
        Squad = squad;
        Plan = plan;
        StartCell = startCell;
    }
}
