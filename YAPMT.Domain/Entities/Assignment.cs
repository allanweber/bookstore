using System;
using YAPMT.Framework.Entities;

namespace YAPMT.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public Assignment()
        {

        }

        public Assignment(string description, string user, DateTime dueDate, bool completed = false)
        {
            this.Description = description;
            this.User = user;
            this.DueDate = dueDate;
            this.Completed = completed;
        }

        public string Description { get; private set; }

        public string User { get; private set; }

        public DateTime DueDate { get; private set; }

        public bool Completed { get; private set; }

        public int ProjectId { get; set; }

        public Project Project { get; private set; }
    }
}
