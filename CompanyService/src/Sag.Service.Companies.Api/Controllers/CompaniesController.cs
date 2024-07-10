using Sag.Framework.ExceptionHandlers;
using Sag.Service.Companies.Application.Dtos;
using Sag.Service.Companies.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Sag.Service.Companies.Api.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Endpoint to get all companies
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet(Name = nameof(GetCompaniesAsync))]
        [ProducesResponseType(typeof(ICollection<CompanyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompaniesAsync(CancellationToken cancellationToken = default)
        {
            // Add filter and pagination in a real-world scenario
            return Ok(await _companyService.GetAsync(cancellationToken));
        }

        /// <summary>
        /// Endpoint to get a company by id
        /// </summary>
        /// <param name="id">The id of the company</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = nameof(GetCompanyByIdAsync))]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await _companyService.GetByIdAsync(id, cancellationToken));
        }

        /// <summary>
        /// Endpoint to create a company
        /// </summary>
        /// <param name="dto">The company to create</param>
        /// <param name="cancellationToken"></param>
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpPost(Name = nameof(CreateCompanyAsync))]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyDto dto, CancellationToken cancellationToken)
        {
            return Ok(await _companyService.CreateAsync(dto, cancellationToken));
        }

        /// <summary>
        /// Endpoint to update an existing company
        /// </summary>
        /// <param name="id">The id of the company</param>
        /// <param name="dto">The company to update</param>
        /// <param name="cancellationToken"></param>
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpPut("{id:guid}", Name = nameof(UpdateCompanyAsync))]
        public async Task<IActionResult> UpdateCompanyAsync(Guid id, [FromBody] CompanyDto dto, CancellationToken cancellationToken)
        {
            dto.Id = id;
            return Ok(await _companyService.UpdateAsync(dto, cancellationToken));
        }

        /// <summary>
        /// Endpoint to delete an existing company
        /// </summary>
        /// <param name="id">The id of the company</param>
        /// <param name="cancellationToken"></param>
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [HttpDelete("{id:guid}", Name = nameof(DeleteCompanyAsync))]
        public async Task<IActionResult> DeleteCompanyAsync(Guid id, CancellationToken cancellationToken)
        {
            await _companyService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
