using System.Diagnostics;

namespace MvcSite
{
    public class LogThings : ILogThings
    {
        public void Log(string message, params object[] arguments)
        {
            Debug.WriteLine(message, arguments);
        }
    }
}