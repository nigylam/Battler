namespace Battler.Battle.Squads
{
    public class SquadFieldContext
    {
        private readonly SquadContext _context;

        public SquadFieldContext(SquadContext squadContext, Squad squad, (int x, int y) startCell)
        {
            _context = squadContext;
            Squad = squad;
            StartCell = startCell;
        }

        public SquadPlan Plan => _context.Plan;
        public bool CreateUpgraded => _context.CreateUpgraded;
        public Squad Squad { get; }
        public (int x, int y) StartCell { get; private set; }

        public void Upgrade()
        {
            _context.Upgrade();
        }

        public void UpdatePosition((int x, int y) startCell)
        {
            StartCell = startCell;
        }
    }
}