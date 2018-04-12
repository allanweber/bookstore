using System.Collections.Generic;
using YAPMT.Framework.Entities;

namespace YAPMT.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {

        }

        public Project(string name)
        {
            this.Name = name;
            this.PathName = this.Name.ToLower().Replace(" ", string.Empty);
        }

        public string Name { get; private set; }

        public string PathName { get; private set; }

        public IEnumerable<Assignment> Tasks { get; private set; }
    }
}
