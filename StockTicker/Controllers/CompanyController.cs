using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StockTicker.Service.Data.Models;
using StockTicker.Service.Data.Services;
using StockTicker.Domain.Queries.Data;
using StockTicker.Domain.Commands.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace StockTicker.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICompanyDBService _companyDBService;

        public CompanyController(IMediator mediator, ICompanyDBService companyDBService)
        {
            _mediator = mediator;
            _companyDBService = companyDBService;
        }

        [HttpGet("GetAllCompanies")]
        [ProducesResponseType(typeof(IEnumerable<Company>), 200)]
        public Task<IEnumerable<Company>> GetAllCompanies() => _mediator.Send(new GetAllCompaniesQuery());

        // GET api/company/GetCompanyId
        [HttpGet("GetCompanyById/id={id}", Name = nameof(GetCompanyById))]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            Company company = await _mediator.Send(new GetCompanyByIdQuery(id));
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
            Company company = await _mediator.Send(new GetCompanyByIsinQuery(isin));
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
        [Authorize]
        [HttpPost("PostPart/")]
        [ProducesResponseType(typeof(Company), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostPart(/*[FromBody]*/ string part)
        {
            if (part == null)
            {
                Debug.WriteLine("part == null");
                return BadRequest();
            }
            return new EmptyResult();
        }

        // POST api/values
        [Authorize]
        [HttpPost("PostCompany/")]
        [ProducesResponseType(typeof(Company), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostCompany([FromBody] Company company)
        {
            if (company == null)
            {
                Console.WriteLine("company == null");
                return BadRequest();
            }

            try
            {
                await _mediator.Send(new CreateCompanyCommand(company));
                return CreatedAtRoute(nameof(GetCompanyById), new { id = company.CompanyId }, company);
            }
            catch(Exception ex)
            {
                Console.WriteLine("BadRequest");
                return BadRequest(ex);
            }
        }

        [Authorize]
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

            Company existingCompany = _mediator.Send(new GetCompanyByIsinQuery(company.Isin)).Result;

            if (existingCompany == null)
            {
                return NotFound();
            }

            await _mediator.Send(new UpdateCompanyCommand(company));
            return new NoContentResult();
        }
    }
}
