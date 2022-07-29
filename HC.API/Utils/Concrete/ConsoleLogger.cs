using HC.API.Utils.Interface;

namespace HC.API.Utils.Concrete
{
    public class ConsoleLogger : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
