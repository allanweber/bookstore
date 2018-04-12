using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using YAPMT.Domain.CommandHandlers.Commands.Project;
using YAPMT.Domain.Dtos;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Framework.Constants;
using YAPMT.Framework.Controllers;

namespace YAPMT.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class ProjectController :
        BaseCrudController<
            IProjectRepository,
            Project,
            ProjectInsertCommand,
            ProjectUpdateCommand,
            ProjectDeleteCommand,
            ProjectDto>
    {
        public ProjectController(IMapper mapper, IMediator mediator, IProjectRepository projectRepository)
            :base(mapper, mediator, projectRepository)
        {
        }
    }
}