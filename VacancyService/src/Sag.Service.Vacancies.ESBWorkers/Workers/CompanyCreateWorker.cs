using Sag.Framework.ESB.Enums;
using Sag.Framework.ESB.Interfaces;
using Sag.Service.Vacancies.Application.Interfaces;

namespace Sag.Service.Vacancies.ESBWorkers.Workers
{
    public class CompanyCreateWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITopicService _topicService;

        public CompanyCreateWorker(IServiceProvider serviceProvider, ITopicService topicService)
        {
            _serviceProvider = serviceProvider;
            _topicService = topicService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _topicService.SubscribeToTopicAsync<CompanyDto>(Framework.Applications.CompanyService, "Company", ESBAction.Create,
                async companyDto =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var createCompanyByEventService = scope.ServiceProvider.GetRequiredService<ICreateCompanyEventService>();
                    await createCompanyByEventService.CreateCompanyEventAsync(companyDto.Id, companyDto.Name, companyDto.DisplayName, stoppingToken);
                }, stoppingToken);
        }
    }
}
