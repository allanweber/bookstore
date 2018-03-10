using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bookstore.Framework.Specifications
{
    public class MaxLenghtSpec : BaseSpecification<string>
    {
        public MaxLenghtSpec(string fieldName, int maxLenght)
        {
            this.FieldName = fieldName;
            this.MaxLenght = maxLenght;
        }

        private string FieldName { get; set; }
        private int MaxLenght { get; set; }

        public override string Description
            => $"O campo {this.FieldName} não pode conter mais do que {this.MaxLenght} caracteres";

        protected override Expression<Func<string, bool>> GetFinalExpression()
            => fieldValue => fieldValue.Length <= this.MaxLenght;
    }
}
