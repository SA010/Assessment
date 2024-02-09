using AutoMapper;

namespace Sag.Service.Companies.Application.Mappings
{
    public static class MappingExtensions
    {
        public static void AddOrUpdateOrRemove<T, TDto>(this ResolutionContext context, ISet<T> entitySet, IEnumerable<TDto> dtoCollection)
            where T : Entity
            where TDto : Dto<T>
        {
            var dtos = dtoCollection.ToList();

            foreach (var dto in dtos)
            {
                if (dto.Id == Guid.Empty)
                {
                    entitySet.Add(context.Mapper.Map<T>(dto));
                }
                else
                {
                    context.Mapper.Map(dto, entitySet.SingleOrDefault(c => c.Id == dto.Id));
                }
            }

            foreach (var entity in entitySet.Where(entity => dtos.All(x => x.Id != entity.Id)))
            {
                entitySet.Remove(entity);
            }
        }
    }
}
