using Sag.Framework.ESB.Enums;

namespace Sag.Service.Companies.Application.EventHandlers
{
    public interface IEventHandler<TEntity, in TDto>
        where TEntity : Entity
        where TDto : Dto<TEntity>, new()
    {
        void Publish(TDto dto, ESBAction action);
    }
}
