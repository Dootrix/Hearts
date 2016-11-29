namespace Hearts.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
