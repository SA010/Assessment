using Sag.Framework.ESB.Enums;
using Sag.Framework.ESB.Interfaces;
using Sag.Service.Vacancies.Application.Interfaces;

namespace Sag.Service.Vacancies.ESBWorkers.Workers
{
    public class CompanyUpdateWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITopicService _topicService;

        public CompanyUpdateWorker(IServiceProvider serviceProvider, ITopicService topicService)
        {
            _serviceProvider = serviceProvider;
            _topicService = topicService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _topicService.SubscribeToTopicAsync<CompanyDto>(Framework.Applications.CompanyService, "Company", ESBAction.Update,
                async companyDto =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var updateCompanyByEventService = scope.ServiceProvider.GetRequiredService<IUpdateCompanyEventService>();
                    await updateCompanyByEventService.UpdateCompanyEventAsync(companyDto.Id, companyDto.Name, companyDto.DisplayName, stoppingToken);
                }, stoppingToken);
        }
    }
}
