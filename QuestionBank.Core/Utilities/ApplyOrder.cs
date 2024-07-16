using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Utilities
{
	public static class Helper
	{
		public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
		{
			methodName = methodName.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

			string[] props = property.Split('.');
			Type type = typeof(T);
			ParameterExpression arg = Expression.Parameter(type, "x");
			Expression expr = arg;
			foreach (string prop in props)
			{
				var _prop = prop;

				if (_prop == "createdDate")
				{
					_prop = "CreatedAt";
				}

				PropertyInfo pi = type.GetProperty(_prop);
				if (pi == null)
					pi = type.GetProperties().FirstOrDefault(x => x.Name.ToLower() == _prop.ToLower());
				expr = Expression.Property(expr, pi);
				type = pi.PropertyType;
			}
			Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
			LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
			object result = typeof(Queryable).GetMethods().Single(
				method => method.Name == methodName
				&& method.IsGenericMethodDefinition
				&& method.GetGenericArguments().Length == 2
				&& method.GetParameters().Length == 2)
				.MakeGenericMethod(typeof(T), type)
				.Invoke(null, new object[] { source, lambda });
			return (IOrderedQueryable<T>)result;
		}
	}
}
