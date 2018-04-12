using System.Collections.Generic;
using System.Threading.Tasks;
using YAPMT.Domain.Entities;
using YAPMT.Framework.Repositories;

namespace YAPMT.Domain.Repositories
{
    public interface IAssignmentRepository: IRepository<Assignment>
    {
        Task<IEnumerable<Assignment>> GetAllByProject(int projectId);
    }
}
