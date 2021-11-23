using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class LinqExtensions
    {
        public static List<string> GetExpressionReturnMembersNames<T>(this Expression<Func<T, object?>> expression)
        {
            List<string> ret = new();
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Constant:
                    return ret;
                case ExpressionType.New:
                    foreach (var value in ((NewExpression)expression.Body).Members!)
                    {
                        ret.Add(value.Name);
                    }
                    break;
                case ExpressionType.Convert:
                    ret.Add(((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name);
                    break;
                case ExpressionType.MemberAccess:
                    ret.Add(((MemberExpression)(expression.Body)).Member.Name);
                    break;
                default:
                    throw new NotImplementedException("Not implemented expression type");
            }
            return ret;
        }
    }
}
