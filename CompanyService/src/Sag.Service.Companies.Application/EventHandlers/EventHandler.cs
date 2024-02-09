using Sag.Framework.ESB.Enums;
using Sag.Framework.ESB.Interfaces;

namespace Sag.Service.Companies.Application.EventHandlers
{
    public class EventHandler<TEntity, TDto> : IEventHandler<TEntity, TDto>
        where TEntity : Entity
        where TDto : Dto<TEntity>, new()
    {
        private readonly ITopicService _topicService;

        public EventHandler(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public void Publish(TDto dto, ESBAction action)
        {
            _topicService.SendToTopic(typeof(TEntity).Name, action, dto);
        }
    }
}
