using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTicker.Service.Services;
using StockTicker.Service.Models;

namespace StockTicker.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyDBService _companyDBService;
        public CompanyController(ICompanyDBService companyDBService)
        {
            _companyDBService = companyDBService;
        }

        [HttpGet("GetAllCompanies")]
        [ProducesResponseType(typeof(IEnumerable<Company>), 200)]
        public Task<IEnumerable<Company>> GetAllCompanies() => _companyDBService.GetAllCompaniesAsync();

        // GET api/company/GetCompanyId
        [HttpGet("GetCompanyById/id={id}", Name = nameof(GetCompanyById))]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            Company company = await _companyDBService.FindCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(company);
            }
        }

        // GET api/company/GetCompanyByIsin
        [HttpGet("GetCompanyByIsin/isin={isin}", Name = nameof(GetCompanyByIsin))]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanyByIsin(string isin)
        {
            Company company = await _companyDBService.FindCompanyByIsinAsync(isin);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(company);
            }
        }

        // POST api/values
        [HttpPost("PostCompany/")]
        [ProducesResponseType(typeof(Company), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostCompany([FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest();
            }

            try
            {
                await _companyDBService.AddNewCompanyAsync(company);
                return CreatedAtRoute(nameof(GetCompanyById), new { id = company.CompanyId }, company);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("PutCompany/")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCompany([FromBody] Company company)
        {
            if (company.CompanyId.GetType() != typeof(int))
            {
                return BadRequest();
            }

            Company existingCompany = _companyDBService.FindCompanyByIsinAsync(company.Isin).Result;

            if (existingCompany == null)
            {
                return NotFound();
            }

            await _companyDBService.UpdateExsistingCompanyAsync(company);
            return new NoContentResult();
        }
    }
}
