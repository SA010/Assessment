using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sag.Framework.ExceptionHandlers;
using Sag.SampleBFF.Api.Application.Dtos;
using Sag.Service.Companies.Client;

namespace Sag.SampleBFF.Api.Api.Controllers
{
    public class Companies : BaseController
    {

        private readonly ISagCompanyServiceClient _companyClient;
        private readonly IMapper _mapper;

        public Companies(ISagCompanyServiceClient companyService, IMapper mapper)
        {
            _companyClient = companyService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint to get all companies
        /// </summary>
        [HttpGet(Name = nameof(GetCompaniesAsync))]
        [ProducesResponseType(typeof(ICollection<CompanyDto>), 200)]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            var companies = await _companyClient.GetCompaniesAsync();
            return Ok(companies);
        }

        /// <summary>
        /// Endpoint to get a company by id
        /// </summary>
        /// <param name="id">The id of the company</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id:guid}", Name = nameof(GetCompanyByIdAsync))]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyByIdAsync(Guid id)
        {
            var company = await _companyClient.GetCompanyByIdAsync(id);
            return Ok(company);
        }

        /// <summary>
        /// Endpoint to create a company
        /// </summary>
        /// <param name="dto">The company to create</param>
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpPost(Name = nameof(CreateCompanyAsync))]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyCreateDto dto)
        {
            var companyDto = _mapper.Map<CompanyCreateDto, CompanyDto>(dto);
            var newCompany = await _companyClient.CreateCompanyAsync(companyDto);
            return Ok(newCompany);
        }

        /// <summary>
        /// Endpoint to update an existing company
        /// </summary>
        /// <param name="id">The id of the company</param>
        /// <param name="dto">The company to update</param>
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [HttpPut("{id:guid}", Name = nameof(UpdateCompanyAsync))]
        public async Task<IActionResult> UpdateCompanyAsync(Guid id, [FromBody] CompanyDto company)
        {
            var updatedCompany = await _companyClient.UpdateCompanyAsync(id, company);
            return Ok(updatedCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyClient.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}
