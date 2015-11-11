namespace MvcSite
{
    public interface ILogThings
    {
        void Log(string message, params object[] arguments);
    }
}