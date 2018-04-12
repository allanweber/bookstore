using AutoMapper;
using YAPMT.Domain.CommandHandlers.Commands.Project;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Framework.CommandHandlers;
using YAPMT.Framework.Specifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using YAPMT.Domain.Specifications;

namespace YAPMT.Domain.CommandHandlers
{
    public class ProjectCommandHandler :
        IRequestHandler<ProjectInsertCommand, ICommandResult>,
        IRequestHandler<ProjectUpdateCommand, ICommandResult>,
        IRequestHandler<ProjectDeleteCommand, ICommandResult>
    {
        public ProjectCommandHandler(IMapper mapper, IProjectRepository projectRepository)
        {
            this.Mapper = mapper;
            this.ProjectRepository = projectRepository;
        }

        public IMapper Mapper { get; }
        public IProjectRepository ProjectRepository { get; }

        public async Task<ICommandResult> Handle(ProjectInsertCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<ProjectInsertCommand, Project>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            await this.ProjectRepository.InsertAsync(entity);

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<ProjectUpdateCommand, Project>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            StringRequiredSpec stringRequired = new StringRequiredSpec(nameof(entity.Id));
            if (!stringRequired.IsSatisfiedBy(entity.Id.ToString()))
            {
                result.Result = stringRequired.Description;
                return result;
            }

            await this.ProjectRepository.UpdateAsync(entity);


            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.ProjectRepository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Projeto não encontrado" };
            }

            StringRequiredSpec stringRequired = new StringRequiredSpec(nameof(entity.Id));
            if (!stringRequired.IsSatisfiedBy(entity.Id.ToString()))
            {
                ICommandResult result = new FailureResult();
                result.Result = stringRequired.Description;
                return result;
            }

            await this.ProjectRepository.DeleteAsync(entity);

            return new SuccessResult();
        }

        private ICommandResult validate(Project entity)
        {
            ICommandResult result = new FailureResult();
            StringRequiredSpec stringRequired = new StringRequiredSpec(nameof(entity.Name));

            if (!stringRequired.IsSatisfiedBy(entity.Name))
            {
                result.Result = stringRequired.Description;
                return result;
            }

            MaxLenghtSpec maxLenghtSpec = new MaxLenghtSpec(nameof(entity.Name), 70);
            if (!maxLenghtSpec.IsSatisfiedBy(entity.Name))
            {
                result.Result = maxLenghtSpec.Description;
                return result;
            }

            ProjectSameNameSpec projectSameNameSpec = new ProjectSameNameSpec(entity);
            var project = this.ProjectRepository.GetAsync(projectSameNameSpec).Result;
            if(project !=null)
            {
                result.Result = projectSameNameSpec.Description;
                return result;
            }

            return result;
        }
    }
}
