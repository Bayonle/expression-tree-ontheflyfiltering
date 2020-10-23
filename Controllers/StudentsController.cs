using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mambuquery.api.Core.Features.Student.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace mambuquery.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {


        private readonly ILogger<StudentsController> _logger;
        private readonly IMediator _mediator;

        public StudentsController(ILogger<StudentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetStudents([FromBody]GetStudentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("fields")]
        public async Task<IActionResult> GetFields()
        {
            var query = new GetStudentFieldsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
