using QBForge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QBForge.Extensions.Linq.Expressions
{
	internal static class ExpressionExtensions
	{
		public static Expression<Action<object, object?>>? MakeCommonSetter(this MemberInfo memberInfo)
		{
			if (memberInfo is PropertyInfo propertyInfo)
			{
				var setMethodInfo = propertyInfo.SetMethod;
				if (setMethodInfo == null)
				{
					return null;
				}

				var documentParameter = Expression.Parameter(typeof(object), "item");
				var valueParameter = Expression.Parameter(typeof(object), "value");
				var memberSelector = Expression.Lambda<Action<object, object?>>(
					Expression.Call(
						Expression.Convert(documentParameter, propertyInfo.DeclaringType!),
						setMethodInfo,
						Expression.Convert(valueParameter, propertyInfo.PropertyType)
					),
					documentParameter,
					valueParameter
				);

				return memberSelector;
			}
			else if (memberInfo is FieldInfo fieldInfo)
			{
				if (fieldInfo.IsInitOnly)
				{
					return null;
				}

				var documentParameter = Expression.Parameter(typeof(object), "item");
				var valueParameter = Expression.Parameter(typeof(object), "value");
				var field = Expression.Field(Expression.Convert(documentParameter, fieldInfo.DeclaringType!), fieldInfo);
				var value = Expression.Convert(valueParameter, fieldInfo.FieldType);
				var body = Expression.Assign(field, value);

				return Expression.Lambda<Action<object, object?>>(body, documentParameter, valueParameter);
			}
			else if (memberInfo == null)
			{
				throw new ArgumentNullException(nameof(memberInfo));
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public static (string paramName, string memberName) GetMemberName(this LambdaExpression memberSelector, bool allowEmptyMemberName = false)
		{
			if (memberSelector == null) throw new ArgumentNullException(nameof(memberSelector));

			string? memberName = null;
			string? paramName = null;
			var currentExpression = memberSelector.Body;

			while (true)
			{
				if (currentExpression.NodeType == ExpressionType.Parameter)
				{
					paramName = ((ParameterExpression)currentExpression).Name;
					break;
				}
				else if (currentExpression.NodeType == ExpressionType.MemberAccess)
				{
					var memberExpression = (MemberExpression)currentExpression;

					if (memberName == null)
					{
						memberName = memberExpression.Member.Name;
					}
					else
					{
						throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
					}

					//if (memberExpression.Expression?.NodeType == ExpressionType.MemberAccess)
					//{
					currentExpression = memberExpression.Expression;
					//}
				}
				else if (currentExpression.NodeType == ExpressionType.Convert || currentExpression.NodeType == ExpressionType.ConvertChecked)
				{
					currentExpression = ((UnaryExpression)currentExpression).Operand;
				}
				else if (currentExpression.NodeType == ExpressionType.Invoke)
				{
					currentExpression = ((InvocationExpression)currentExpression).Expression;
				}
				else
				{
					throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
				}
			}

			if (paramName == null)
			{
				throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
			}

			if (memberName == null)
			{
				return allowEmptyMemberName
					? (paramName, string.Empty)
					: throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
			}

			return (paramName, memberName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DataEntry ToDataEntry(this LambdaExpression memberSelector)
		{
			var (paramName, memberName) = memberSelector.GetMemberName(false);
			return new DataEntry(paramName == "_" ? null : paramName, memberName);
		}

		public static (string paramName, string memberPath) GetMemberPath(this LambdaExpression memberSelector, bool allowEmptyMemberPath = false)
		{
			if (memberSelector == null) throw new ArgumentNullException(nameof(memberSelector));

			List<string>? memberNames = null;
			string? firstMemberName = null;
			string? paramName = null;
			var currentExpression = memberSelector.Body;

			while (true)
			{
				if (currentExpression.NodeType == ExpressionType.Parameter)
				{
					paramName = ((ParameterExpression)currentExpression).Name;
					break;
				}
				else if (currentExpression.NodeType == ExpressionType.MemberAccess)
				{
					var memberExpression = (MemberExpression)currentExpression;

					if (firstMemberName == null)
					{
						firstMemberName = memberExpression.Member.Name;
					}
					else if (memberNames == null)
					{
						memberNames = new List<string>(8)
						{
							firstMemberName, memberExpression.Member.Name
						};
					}
					else
					{
						memberNames.Add(memberExpression.Member.Name);
					}

					//if (memberExpression.Expression?.NodeType == ExpressionType.MemberAccess)
					//{
					currentExpression = memberExpression.Expression;
					//}
				}
				else if (currentExpression.NodeType == ExpressionType.Convert || currentExpression.NodeType == ExpressionType.ConvertChecked)
				{
					currentExpression = ((UnaryExpression)currentExpression).Operand;
				}
				else if (currentExpression.NodeType == ExpressionType.Invoke)
				{
					currentExpression = ((InvocationExpression)currentExpression).Expression;
				}
				else
				{
					throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
				}
			}

			if (paramName == null)
			{
				throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
			}

			if (memberNames == null)
			{
				if (firstMemberName == null)
				{
					return allowEmptyMemberPath
						? (paramName, string.Empty)
						: throw new ArgumentException("Wrong member selector: " + memberSelector.ToString(), nameof(memberSelector));
				}

				return (paramName, firstMemberName);
			}

			memberNames.Reverse();

			return (paramName, string.Join(".", memberNames));
		}
	}
}