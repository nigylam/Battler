public class Level
{
    public Level() {}

    public bool Completed {  get; private set; }
    public bool Opened { get; private set; }

    public void SetCompleted()
    {
        Completed = true;
    }

    public void SetOpened()
    {
        Opened = true;
    }
}
