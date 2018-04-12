using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Framework.Repositories;

namespace YAPMT.Infrastructure.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Assignment>> GetAllByProject(int projectId)
        {
            throw new NotImplementedException(); ;
        }
    }
}
