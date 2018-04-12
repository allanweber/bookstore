using System;
using System.Collections.Generic;
using System.Text;
using YAPMT.Framework.Dtos;

namespace YAPMT.Domain.Dtos
{
    public class AssignmentDto : IDto
    {
        public string Id { get; set; }

        public string Description { get;  set; }

        public string User { get;  set; }

        public DateTime DueDate { get;  set; }

        public bool Completed { get;  set; }
    }
}
