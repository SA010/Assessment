namespace Sag.Service.Vacancies.Api.Specs.Util
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public HttpLoggingHandler(HttpMessageHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("======================================================================");
            _logger.LogInformation("Request:");
            _logger.LogInformation(request.ToString());
            _logger.LogInformation("======================================================================");

            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation("======================================================================");
            _logger.LogInformation("Response:");
            _logger.LogInformation(response.ToString());
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation(await response.Content.ReadAsStringAsync(cancellationToken));
            }

            _logger.LogInformation("======================================================================");

            return response;
        }
    }
}