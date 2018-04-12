using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YAPMT.Domain.CommandHandlers.Commands.Assignment;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Framework.Constants;

namespace YAPMT.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class AssignmentController : Controller
    {
        public AssignmentController(IMapper mapper, IMediator mediator, IAssignmentRepository assignmentRepository)
        {
            Mapper = mapper;
            Mediator = mediator;
            AssignmentRepository = assignmentRepository;
        }

        public IMapper Mapper { get; }
        public IMediator Mediator { get; }
        public IAssignmentRepository AssignmentRepository { get; }

        [HttpGet]
        [Route("{projectId:int}")]
        public async Task<IActionResult> Get(int projectId)
        {
            var tasks = await this.AssignmentRepository.GetAllByProject(projectId);

            return Ok(tasks);
        }

        [HttpPost]
        [Route("{projectId:int}")]
        public async Task<IActionResult> Post(int projectId, [FromBody] AssignmentInsertCommand request)
        {
            var entity = this.Mapper.Map<Assignment>(request);



            return Ok();
        }
    }
}