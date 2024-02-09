namespace Sag.Service.Companies.Api.Specs.Util
{
    public sealed class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public ILogger CreateLogger(string categoryName)
        {
            var logger = new XUnitLogger();
            _disposables.Add(logger);
            return logger;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}