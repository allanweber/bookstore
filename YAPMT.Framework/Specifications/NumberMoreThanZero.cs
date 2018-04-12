using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace YAPMT.Framework.Specifications
{
    public class NumberMoreThanZero : BaseSpecification<int>
    {
        public NumberMoreThanZero(string fieldName)
        {
            this.FieldName = fieldName;
        }

        public override string Description =>
            $"O campo {this.FieldName} deve ser maior que zero.";

        public string FieldName { get; }

        protected override Expression<Func<int, bool>> GetFinalExpression()
            => intValue => intValue > 0;
    }
}
