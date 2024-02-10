using Sag.Framework.ExceptionHandlers;
using Sag.Service.Vacancies.Application.Interfaces;
using Sag.Service.Vacancies.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Sag.Service.Vacancies.Api.Controllers
{
    public class VacanciesController : BaseController
    {
        private readonly IVacancyService _vacancyService;
       
        public VacanciesController( IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        /// <summary>
        /// Create vacancy
        /// </summary>
        /// <param name="vacancy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>VacancyDto</returns>
        [ProducesResponseType(typeof(VacancyDto), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 409)]
        [HttpPost(Name = nameof(CreateVacancyAsync))]
        public async Task<IActionResult> CreateVacancyAsync([FromBody] VacancyDto vacancy, CancellationToken cancellationToken)
        {
            var createdVacancy = await _vacancyService.CreateAsync(vacancy, cancellationToken);
            return Ok(createdVacancy);
        }

        /// <summary>
        /// Update vacancy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacancy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>VacancyDto</returns>
        [ProducesResponseType(typeof(VacancyDto), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 409)]
        [HttpPut("{id:guid}", Name = nameof(UpdateVacancyAsync))]
        public async Task<IActionResult> UpdateVacancyAsync([FromRoute] Guid id, [FromBody] VacancyDto vacancy, CancellationToken cancellationToken)
        {
            var updatedVacancy = await _vacancyService.UpdateAsync(id, vacancy, cancellationToken);

            return Ok(await _vacancyService.GetAsync(updatedVacancy.Id, cancellationToken));
        }

        /// <summary>
        /// Delete vacancy
        /// </summary>
        /// <param name="id">VacancyId</param>
        /// <param name="cancellationToken"></param>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [HttpDelete("{id:guid}", Name = nameof(DeleteVacancyAsync))]
        public async Task<IActionResult> DeleteVacancyAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _vacancyService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
               
        /// <summary>
        /// Get vacancy by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>VacancyDto</returns>
        [ProducesResponseType(typeof(VacancyDto), 200)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}", Name = nameof(GetVacancyByIdAsync))]
        public async Task<IActionResult> GetVacancyByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyService.GetAsync(id, cancellationToken);
            return Ok(vacancy);
        }

        /// <summary>
        /// Get all vacancies
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of vacancyDtos</returns>
        [ProducesResponseType(typeof(IEnumerable<VacancyDto>), 200)]
        [HttpGet(Name = nameof(GetVacanciesAsync))]
        public async Task<IActionResult> GetVacanciesAsync(CancellationToken cancellationToken)
        {
            var vacancies = await _vacancyService.GetAsync(cancellationToken);
            return Ok(vacancies);
        }
    }
}
