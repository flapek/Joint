namespace Joint.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}