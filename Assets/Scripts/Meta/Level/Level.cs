public class Level
{
    public bool Completed {  get; private set; }

    public Level() {}

    public void SetCompleted()
    {
        Completed = true;
    }
}
