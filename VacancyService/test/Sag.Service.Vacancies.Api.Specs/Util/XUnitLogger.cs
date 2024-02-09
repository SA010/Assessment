using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Sag.Service.Vacancies.Api.Specs.Util
{
    public sealed class XUnitLogger : ILogger, IDisposable
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var output = TestRunnerManager.GetTestRunner()?.ScenarioContext?.ScenarioContainer?.Resolve<ITestOutputHelper>();
            if (output != null)
            {
                var message = formatter(state, exception);
                output.WriteLine($"[{logLevel.ToString()[..4]}] {message}");
                if (exception != null)
                {
                    output.WriteLine("--- EXCEPTION " + exception.Message);
                    output.WriteLine(exception.StackTrace);
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return new XUnitLogger();
        }

        public void Dispose()
        {
            //Dispose required for 'BeginScope'
        }
    }
}
