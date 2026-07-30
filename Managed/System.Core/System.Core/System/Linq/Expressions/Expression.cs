using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Globalization;
using System.IO;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Linq.Expressions
{
	/// <summary>Provides the base class from which the classes that represent expression tree nodes are derived. It also contains static (Shared in Visual Basic) factory methods to create the various node types. This is an abstract class.</summary>
	// Token: 0x02000227 RID: 551
	public abstract class Expression
	{
		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Assign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DAE RID: 3502 RVA: 0x0002D838 File Offset: 0x0002BA38
		public static BinaryExpression Assign(Expression left, Expression right)
		{
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			TypeUtils.ValidateType(left.Type, "left", true, true);
			TypeUtils.ValidateType(right.Type, "right", true, true);
			if (!TypeUtils.AreReferenceAssignable(left.Type, right.Type))
			{
				throw Error.ExpressionTypeDoesNotMatchAssignment(right.Type, left.Type);
			}
			return new AssignBinaryExpression(left, right);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002D8AC File Offset: 0x0002BAAC
		private static BinaryExpression GetUserDefinedBinaryOperator(ExpressionType binaryType, string name, Expression left, Expression right, bool liftToNull)
		{
			MethodInfo methodInfo = Expression.GetUserDefinedBinaryOperator(binaryType, left.Type, right.Type, name);
			if (methodInfo != null)
			{
				return new MethodBinaryExpression(binaryType, left, right, methodInfo.ReturnType, methodInfo);
			}
			if (left.Type.IsNullableType() && right.Type.IsNullableType())
			{
				Type nonNullableType = left.Type.GetNonNullableType();
				Type nonNullableType2 = right.Type.GetNonNullableType();
				methodInfo = Expression.GetUserDefinedBinaryOperator(binaryType, nonNullableType, nonNullableType2, name);
				if (methodInfo != null && methodInfo.ReturnType.IsValueType && !methodInfo.ReturnType.IsNullableType())
				{
					if (methodInfo.ReturnType != typeof(bool) || liftToNull)
					{
						return new MethodBinaryExpression(binaryType, left, right, methodInfo.ReturnType.GetNullableType(), methodInfo);
					}
					return new MethodBinaryExpression(binaryType, left, right, typeof(bool), methodInfo);
				}
			}
			return null;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002D990 File Offset: 0x0002BB90
		private static BinaryExpression GetMethodBasedBinaryOperator(ExpressionType binaryType, Expression left, Expression right, MethodInfo method, bool liftToNull)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 2)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], left.Type) && Expression.ParameterIsAssignable(parametersCached[1], right.Type))
			{
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, binaryType, method.Name);
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, binaryType, method.Name);
				return new MethodBinaryExpression(binaryType, left, right, method.ReturnType, method);
			}
			if (!left.Type.IsNullableType() || !right.Type.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.Type.GetNonNullableType()) || !Expression.ParameterIsAssignable(parametersCached[1], right.Type.GetNonNullableType()) || !method.ReturnType.IsValueType || method.ReturnType.IsNullableType())
			{
				throw Error.OperandTypesDoNotMatchParameters(binaryType, method.Name);
			}
			if (method.ReturnType != typeof(bool) || liftToNull)
			{
				return new MethodBinaryExpression(binaryType, left, right, method.ReturnType.GetNullableType(), method);
			}
			return new MethodBinaryExpression(binaryType, left, right, typeof(bool), method);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		private static BinaryExpression GetMethodBasedAssignOperator(ExpressionType binaryType, Expression left, Expression right, MethodInfo method, LambdaExpression conversion, bool liftToNull)
		{
			BinaryExpression binaryExpression = Expression.GetMethodBasedBinaryOperator(binaryType, left, right, method, liftToNull);
			if (conversion == null)
			{
				if (!TypeUtils.AreReferenceAssignable(left.Type, binaryExpression.Type))
				{
					throw Error.UserDefinedOpMustHaveValidReturnType(binaryType, binaryExpression.Method.Name);
				}
			}
			else
			{
				Expression.ValidateOpAssignConversionLambda(conversion, binaryExpression.Left, binaryExpression.Method, binaryExpression.NodeType);
				binaryExpression = new OpAssignMethodConversionBinaryExpression(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.Right, binaryExpression.Left.Type, binaryExpression.Method, conversion);
			}
			return binaryExpression;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002DB64 File Offset: 0x0002BD64
		private static BinaryExpression GetUserDefinedBinaryOperatorOrThrow(ExpressionType binaryType, string name, Expression left, Expression right, bool liftToNull)
		{
			BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, name, left, right, liftToNull);
			if (userDefinedBinaryOperator != null)
			{
				ParameterInfo[] parametersCached = userDefinedBinaryOperator.Method.GetParametersCached();
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, binaryType, name);
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, binaryType, name);
				return userDefinedBinaryOperator;
			}
			throw Error.BinaryOperatorNotDefined(binaryType, left.Type, right.Type);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002DBD0 File Offset: 0x0002BDD0
		private static BinaryExpression GetUserDefinedAssignOperatorOrThrow(ExpressionType binaryType, string name, Expression left, Expression right, LambdaExpression conversion, bool liftToNull)
		{
			BinaryExpression binaryExpression = Expression.GetUserDefinedBinaryOperatorOrThrow(binaryType, name, left, right, liftToNull);
			if (conversion == null)
			{
				if (!TypeUtils.AreReferenceAssignable(left.Type, binaryExpression.Type))
				{
					throw Error.UserDefinedOpMustHaveValidReturnType(binaryType, binaryExpression.Method.Name);
				}
			}
			else
			{
				Expression.ValidateOpAssignConversionLambda(conversion, binaryExpression.Left, binaryExpression.Method, binaryExpression.NodeType);
				binaryExpression = new OpAssignMethodConversionBinaryExpression(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.Right, binaryExpression.Left.Type, binaryExpression.Method, conversion);
			}
			return binaryExpression;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		private static MethodInfo GetUserDefinedBinaryOperator(ExpressionType binaryType, Type leftType, Type rightType, string name)
		{
			Type[] array = new Type[] { leftType, rightType };
			Type nonNullableType = leftType.GetNonNullableType();
			Type nonNullableType2 = rightType.GetNonNullableType();
			MethodInfo methodInfo = nonNullableType.GetAnyStaticMethodValidated(name, array);
			if (methodInfo == null && !TypeUtils.AreEquivalent(leftType, rightType))
			{
				methodInfo = nonNullableType2.GetAnyStaticMethodValidated(name, array);
			}
			if (Expression.IsLiftingConditionalLogicalOperator(leftType, rightType, methodInfo, binaryType))
			{
				methodInfo = Expression.GetUserDefinedBinaryOperator(binaryType, nonNullableType, nonNullableType2, name);
			}
			return methodInfo;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		private static bool IsLiftingConditionalLogicalOperator(Type left, Type right, MethodInfo method, ExpressionType binaryType)
		{
			return right.IsNullableType() && left.IsNullableType() && method == null && (binaryType == ExpressionType.AndAlso || binaryType == ExpressionType.OrElse);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		internal static bool ParameterIsAssignable(ParameterInfo pi, Type argType)
		{
			Type type = pi.ParameterType;
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			return TypeUtils.AreReferenceAssignable(type, argType);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002DD12 File Offset: 0x0002BF12
		private static void ValidateParamswithOperandsOrThrow(Type paramType, Type operandType, ExpressionType exprType, string name)
		{
			if (paramType.IsNullableType() && !operandType.IsNullableType())
			{
				throw Error.OperandTypesDoNotMatchParameters(exprType, name);
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002DD34 File Offset: 0x0002BF34
		private static void ValidateOperator(MethodInfo method)
		{
			Expression.ValidateMethodInfo(method, "method");
			if (!method.IsStatic)
			{
				throw Error.UserDefinedOperatorMustBeStatic(method, "method");
			}
			if (method.ReturnType == typeof(void))
			{
				throw Error.UserDefinedOperatorMustNotBeVoid(method, "method");
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002DD83 File Offset: 0x0002BF83
		private static void ValidateMethodInfo(MethodInfo method, string paramName)
		{
			if (method.ContainsGenericParameters)
			{
				throw method.IsGenericMethodDefinition ? Error.MethodIsGeneric(method, paramName) : Error.MethodContainsGenericParameters(method, paramName);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002DDA6 File Offset: 0x0002BFA6
		private static bool IsNullComparison(Expression left, Expression right)
		{
			if (!Expression.IsNullConstant(left))
			{
				return Expression.IsNullConstant(right) && left.Type.IsNullableType();
			}
			return !Expression.IsNullConstant(right) && right.Type.IsNullableType();
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002DDDC File Offset: 0x0002BFDC
		private static bool IsNullConstant(Expression e)
		{
			ConstantExpression constantExpression = e as ConstantExpression;
			return constantExpression != null && constantExpression.Value == null;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002DE00 File Offset: 0x0002C000
		private static void ValidateUserDefinedConditionalLogicOperator(ExpressionType nodeType, Type left, Type right, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 2)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left) && (!left.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, method.Name);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[1], right) && (!right.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[1], right.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, method.Name);
			}
			if (parametersCached[0].ParameterType != parametersCached[1].ParameterType)
			{
				throw Error.UserDefinedOpMustHaveConsistentTypes(nodeType, method.Name);
			}
			if (method.ReturnType != parametersCached[0].ParameterType)
			{
				throw Error.UserDefinedOpMustHaveConsistentTypes(nodeType, method.Name);
			}
			if (Expression.IsValidLiftedConditionalLogicalOperator(left, right, parametersCached))
			{
				left = left.GetNonNullableType();
			}
			Type declaringType = method.DeclaringType;
			if (declaringType == null)
			{
				throw Error.LogicalOperatorMustHaveBooleanOperators(nodeType, method.Name);
			}
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(declaringType, "op_True");
			MethodInfo booleanOperator2 = TypeUtils.GetBooleanOperator(declaringType, "op_False");
			if (booleanOperator == null || booleanOperator.ReturnType != typeof(bool) || booleanOperator2 == null || booleanOperator2.ReturnType != typeof(bool))
			{
				throw Error.LogicalOperatorMustHaveBooleanOperators(nodeType, method.Name);
			}
			Expression.VerifyOpTrueFalse(nodeType, left, booleanOperator2, "method");
			Expression.VerifyOpTrueFalse(nodeType, left, booleanOperator, "method");
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002DF9C File Offset: 0x0002C19C
		private static void VerifyOpTrueFalse(ExpressionType nodeType, Type left, MethodInfo opTrue, string paramName)
		{
			ParameterInfo[] parametersCached = opTrue.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(opTrue, paramName);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left) && (!left.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, opTrue.Name);
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002DFF3 File Offset: 0x0002C1F3
		private static bool IsValidLiftedConditionalLogicalOperator(Type left, Type right, ParameterInfo[] pms)
		{
			return TypeUtils.AreEquivalent(left, right) && right.IsNullableType() && TypeUtils.AreEquivalent(pms[1].ParameterType, right.GetNonNullableType());
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" />, given the left and right operands, by calling an appropriate factory method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.BinaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <param name="binaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of binary operation.</param>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="binaryType" /> does not correspond to a binary expression node.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0002E01B File Offset: 0x0002C21B
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right)
		{
			return Expression.MakeBinary(binaryType, left, right, false, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" />, given the left operand, right operand and implementing method, by calling the appropriate factory method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.BinaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <param name="binaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of binary operation.</param>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that specifies the implementing method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="binaryType" /> does not correspond to a binary expression node.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002E028 File Offset: 0x0002C228
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			return Expression.MakeBinary(binaryType, left, right, liftToNull, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" />, given the left operand, right operand, implementing method and type conversion function, by calling the appropriate factory method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.BinaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <param name="binaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of binary operation.</param>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that specifies the implementing method.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that represents a type conversion function. This parameter is used only if <paramref name="binaryType" /> is <see cref="F:System.Linq.Expressions.ExpressionType.Coalesce" /> or compound assignment..</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="binaryType" /> does not correspond to a binary expression node.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002E038 File Offset: 0x0002C238
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method, LambdaExpression conversion)
		{
			switch (binaryType)
			{
			case ExpressionType.Add:
				return Expression.Add(left, right, method);
			case ExpressionType.AddChecked:
				return Expression.AddChecked(left, right, method);
			case ExpressionType.And:
				return Expression.And(left, right, method);
			case ExpressionType.AndAlso:
				return Expression.AndAlso(left, right, method);
			case ExpressionType.ArrayIndex:
				return Expression.ArrayIndex(left, right);
			case ExpressionType.Coalesce:
				return Expression.Coalesce(left, right, conversion);
			case ExpressionType.Divide:
				return Expression.Divide(left, right, method);
			case ExpressionType.Equal:
				return Expression.Equal(left, right, liftToNull, method);
			case ExpressionType.ExclusiveOr:
				return Expression.ExclusiveOr(left, right, method);
			case ExpressionType.GreaterThan:
				return Expression.GreaterThan(left, right, liftToNull, method);
			case ExpressionType.GreaterThanOrEqual:
				return Expression.GreaterThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.LeftShift:
				return Expression.LeftShift(left, right, method);
			case ExpressionType.LessThan:
				return Expression.LessThan(left, right, liftToNull, method);
			case ExpressionType.LessThanOrEqual:
				return Expression.LessThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.Modulo:
				return Expression.Modulo(left, right, method);
			case ExpressionType.Multiply:
				return Expression.Multiply(left, right, method);
			case ExpressionType.MultiplyChecked:
				return Expression.MultiplyChecked(left, right, method);
			case ExpressionType.NotEqual:
				return Expression.NotEqual(left, right, liftToNull, method);
			case ExpressionType.Or:
				return Expression.Or(left, right, method);
			case ExpressionType.OrElse:
				return Expression.OrElse(left, right, method);
			case ExpressionType.Power:
				return Expression.Power(left, right, method);
			case ExpressionType.RightShift:
				return Expression.RightShift(left, right, method);
			case ExpressionType.Subtract:
				return Expression.Subtract(left, right, method);
			case ExpressionType.SubtractChecked:
				return Expression.SubtractChecked(left, right, method);
			case ExpressionType.Assign:
				return Expression.Assign(left, right);
			case ExpressionType.AddAssign:
				return Expression.AddAssign(left, right, method, conversion);
			case ExpressionType.AndAssign:
				return Expression.AndAssign(left, right, method, conversion);
			case ExpressionType.DivideAssign:
				return Expression.DivideAssign(left, right, method, conversion);
			case ExpressionType.ExclusiveOrAssign:
				return Expression.ExclusiveOrAssign(left, right, method, conversion);
			case ExpressionType.LeftShiftAssign:
				return Expression.LeftShiftAssign(left, right, method, conversion);
			case ExpressionType.ModuloAssign:
				return Expression.ModuloAssign(left, right, method, conversion);
			case ExpressionType.MultiplyAssign:
				return Expression.MultiplyAssign(left, right, method, conversion);
			case ExpressionType.OrAssign:
				return Expression.OrAssign(left, right, method, conversion);
			case ExpressionType.PowerAssign:
				return Expression.PowerAssign(left, right, method, conversion);
			case ExpressionType.RightShiftAssign:
				return Expression.RightShiftAssign(left, right, method, conversion);
			case ExpressionType.SubtractAssign:
				return Expression.SubtractAssign(left, right, method, conversion);
			case ExpressionType.AddAssignChecked:
				return Expression.AddAssignChecked(left, right, method, conversion);
			case ExpressionType.MultiplyAssignChecked:
				return Expression.MultiplyAssignChecked(left, right, method, conversion);
			case ExpressionType.SubtractAssignChecked:
				return Expression.SubtractAssignChecked(left, right, method, conversion);
			}
			throw Error.UnhandledBinary(binaryType, "binaryType");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an equality comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Equal" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The equality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002E338 File Offset: 0x0002C538
		public static BinaryExpression Equal(Expression left, Expression right)
		{
			return Expression.Equal(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an equality comparison. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Equal" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the equality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002E343 File Offset: 0x0002C543
		public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetEqualityComparisonOperator(ExpressionType.Equal, "op_Equality", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.Equal, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a reference equality comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Equal" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002E380 File Offset: 0x0002C580
		public static BinaryExpression ReferenceEqual(Expression left, Expression right)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (TypeUtils.HasReferenceEquality(left.Type, right.Type))
			{
				return new LogicalBinaryExpression(ExpressionType.Equal, left, right);
			}
			throw Error.ReferenceEqualityNotDefined(left.Type, right.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an inequality comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NotEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The inequality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002E3D1 File Offset: 0x0002C5D1
		public static BinaryExpression NotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an inequality comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NotEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the inequality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002E3DC File Offset: 0x0002C5DC
		public static BinaryExpression NotEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetEqualityComparisonOperator(ExpressionType.NotEqual, "op_Inequality", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.NotEqual, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a reference inequality comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NotEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002E418 File Offset: 0x0002C618
		public static BinaryExpression ReferenceNotEqual(Expression left, Expression right)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (TypeUtils.HasReferenceEquality(left.Type, right.Type))
			{
				return new LogicalBinaryExpression(ExpressionType.NotEqual, left, right);
			}
			throw Error.ReferenceEqualityNotDefined(left.Type, right.Type);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002E46C File Offset: 0x0002C66C
		private static BinaryExpression GetEqualityComparisonOperator(ExpressionType binaryType, string opName, Expression left, Expression right, bool liftToNull)
		{
			if (left.Type == right.Type && (left.Type.IsNumeric() || left.Type == typeof(object) || left.Type.IsBool() || left.Type.GetNonNullableType().IsEnum))
			{
				if (left.Type.IsNullableType() && liftToNull)
				{
					return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
				}
				return new LogicalBinaryExpression(binaryType, left, right);
			}
			else
			{
				BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, opName, left, right, liftToNull);
				if (userDefinedBinaryOperator != null)
				{
					return userDefinedBinaryOperator;
				}
				if (!TypeUtils.HasBuiltInEqualityOperator(left.Type, right.Type) && !Expression.IsNullComparison(left, right))
				{
					throw Error.BinaryOperatorNotDefined(binaryType, left.Type, right.Type);
				}
				if (left.Type.IsNullableType() && liftToNull)
				{
					return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
				}
				return new LogicalBinaryExpression(binaryType, left, right);
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "greater than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002E56B File Offset: 0x0002C76B
		public static BinaryExpression GreaterThan(Expression left, Expression right)
		{
			return Expression.GreaterThan(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than" numeric comparison. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the "greater than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DCA RID: 3530 RVA: 0x0002E576 File Offset: 0x0002C776
		public static BinaryExpression GreaterThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.GreaterThan, "op_GreaterThan", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.GreaterThan, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "less than" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "less than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DCB RID: 3531 RVA: 0x0002E5B2 File Offset: 0x0002C7B2
		public static BinaryExpression LessThan(Expression left, Expression right)
		{
			return Expression.LessThan(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "less than" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the "less than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DCC RID: 3532 RVA: 0x0002E5BD File Offset: 0x0002C7BD
		public static BinaryExpression LessThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.LessThan, "op_LessThan", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.LessThan, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than or equal" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "greater than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DCD RID: 3533 RVA: 0x0002E5F9 File Offset: 0x0002C7F9
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right)
		{
			return Expression.GreaterThanOrEqual(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than or equal" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the "greater than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DCE RID: 3534 RVA: 0x0002E604 File Offset: 0x0002C804
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.GreaterThanOrEqual, "op_GreaterThanOrEqual", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.GreaterThanOrEqual, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a " less than or equal" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "less than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DCF RID: 3535 RVA: 0x0002E640 File Offset: 0x0002C840
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right)
		{
			return Expression.LessThanOrEqual(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "less than or equal" numeric comparison.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">true to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to true; false to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to false.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the "less than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002E64B File Offset: 0x0002C84B
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.LessThanOrEqual, "op_LessThanOrEqual", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.LessThanOrEqual, left, right, method, liftToNull);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002E688 File Offset: 0x0002C888
		private static BinaryExpression GetComparisonOperator(ExpressionType binaryType, string opName, Expression left, Expression right, bool liftToNull)
		{
			if (!(left.Type == right.Type) || !left.Type.IsNumeric())
			{
				return Expression.GetUserDefinedBinaryOperatorOrThrow(binaryType, opName, left, right, liftToNull);
			}
			if (left.Type.IsNullableType() && liftToNull)
			{
				return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
			}
			return new LogicalBinaryExpression(binaryType, left, right);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional AND operation that evaluates the second operand only if the first operand evaluates to true.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAlso" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise AND operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-<paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002E6EC File Offset: 0x0002C8EC
		public static BinaryExpression AndAlso(Expression left, Expression right)
		{
			return Expression.AndAlso(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional AND operation that evaluates the second operand only if the first operand is resolved to true. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAlso" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the bitwise AND operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-<paramref name="method" /> is null and <paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002E6F8 File Offset: 0x0002C8F8
		public static BinaryExpression AndAlso(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.AndAlso, left.Type, right.Type, method);
				Type type = ((left.Type.IsNullableType() && TypeUtils.AreEquivalent(method.ReturnType, left.Type.GetNonNullableType())) ? left.Type : method.ReturnType);
				return new MethodBinaryExpression(ExpressionType.AndAlso, left, right, type, method);
			}
			if (left.Type == right.Type)
			{
				if (left.Type == typeof(bool))
				{
					return new LogicalBinaryExpression(ExpressionType.AndAlso, left, right);
				}
				if (left.Type == typeof(bool?))
				{
					return new SimpleBinaryExpression(ExpressionType.AndAlso, left, right, left.Type);
				}
			}
			method = Expression.GetUserDefinedBinaryOperator(ExpressionType.AndAlso, left.Type, right.Type, "op_BitwiseAnd");
			if (method != null)
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.AndAlso, left.Type, right.Type, method);
				Type type = ((left.Type.IsNullableType() && TypeUtils.AreEquivalent(method.ReturnType, left.Type.GetNonNullableType())) ? left.Type : method.ReturnType);
				return new MethodBinaryExpression(ExpressionType.AndAlso, left, right, type, method);
			}
			throw Error.BinaryOperatorNotDefined(ExpressionType.AndAlso, left.Type, right.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional OR operation that evaluates the second operand only if the first operand evaluates to false.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrElse" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise OR operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-<paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002E85D File Offset: 0x0002CA5D
		public static BinaryExpression OrElse(Expression left, Expression right)
		{
			return Expression.OrElse(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional OR operation that evaluates the second operand only if the first operand evaluates to false.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrElse" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the bitwise OR operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-<paramref name="method" /> is null and <paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002E868 File Offset: 0x0002CA68
		public static BinaryExpression OrElse(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.OrElse, left.Type, right.Type, method);
				Type type = ((left.Type.IsNullableType() && method.ReturnType == left.Type.GetNonNullableType()) ? left.Type : method.ReturnType);
				return new MethodBinaryExpression(ExpressionType.OrElse, left, right, type, method);
			}
			if (left.Type == right.Type)
			{
				if (left.Type == typeof(bool))
				{
					return new LogicalBinaryExpression(ExpressionType.OrElse, left, right);
				}
				if (left.Type == typeof(bool?))
				{
					return new SimpleBinaryExpression(ExpressionType.OrElse, left, right, left.Type);
				}
			}
			method = Expression.GetUserDefinedBinaryOperator(ExpressionType.OrElse, left.Type, right.Type, "op_BitwiseOr");
			if (method != null)
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.OrElse, left.Type, right.Type, method);
				Type type = ((left.Type.IsNullableType() && method.ReturnType == left.Type.GetNonNullableType()) ? left.Type : method.ReturnType);
				return new MethodBinaryExpression(ExpressionType.OrElse, left, right, type, method);
			}
			throw Error.BinaryOperatorNotDefined(ExpressionType.OrElse, left.Type, right.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a coalescing operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Coalesce" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="left" /> does not represent a reference type or a nullable value type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="left" />.Type and <paramref name="right" />.Type are not convertible to each other.</exception>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002E9D5 File Offset: 0x0002CBD5
		public static BinaryExpression Coalesce(Expression left, Expression right)
		{
			return Expression.Coalesce(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a coalescing operation, given a conversion function.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Coalesce" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="left" />.Type and <paramref name="right" />.Type are not convertible to each other.-or-<paramref name="conversion" /> is not null and <paramref name="conversion" />.Type is a delegate type that does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="left" /> does not represent a reference type or a nullable value type.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="left" /> represents a type that is not assignable to the parameter type of the delegate type <paramref name="conversion" />.Type.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="right" /> is not equal to the return type of the delegate type <paramref name="conversion" />.Type.</exception>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002E9E0 File Offset: 0x0002CBE0
		public static BinaryExpression Coalesce(Expression left, Expression right, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (conversion == null)
			{
				Type type = Expression.ValidateCoalesceArgTypes(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.Coalesce, left, right, type);
			}
			if (left.Type.IsValueType && !left.Type.IsNullableType())
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			MethodInfo invokeMethod = conversion.Type.GetInvokeMethod();
			if (invokeMethod.ReturnType == typeof(void))
			{
				throw Error.UserDefinedOperatorMustNotBeVoid(conversion, "conversion");
			}
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(conversion, "conversion");
			}
			if (!TypeUtils.AreEquivalent(invokeMethod.ReturnType, right.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(ExpressionType.Coalesce, conversion.ToString());
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left.Type.GetNonNullableType()) && !Expression.ParameterIsAssignable(parametersCached[0], left.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(ExpressionType.Coalesce, conversion.ToString());
			}
			return new CoalesceConversionBinaryExpression(left, right, conversion);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002EAEC File Offset: 0x0002CCEC
		private static Type ValidateCoalesceArgTypes(Type left, Type right)
		{
			Type nonNullableType = left.GetNonNullableType();
			if (left.IsValueType && !left.IsNullableType())
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			if (left.IsNullableType() && right.IsImplicitlyConvertibleTo(nonNullableType))
			{
				return nonNullableType;
			}
			if (right.IsImplicitlyConvertibleTo(left))
			{
				return left;
			}
			if (nonNullableType.IsImplicitlyConvertibleTo(right))
			{
				return right;
			}
			throw Error.ArgumentTypesMustMatch();
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Add" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002EB44 File Offset: 0x0002CD44
		public static BinaryExpression Add(Expression left, Expression right)
		{
			return Expression.Add(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that does not have overflow checking. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Add" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DDA RID: 3546 RVA: 0x0002EB50 File Offset: 0x0002CD50
		public static BinaryExpression Add(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Add, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Add, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Add, "op_Addition", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DDB RID: 3547 RVA: 0x0002EBC4 File Offset: 0x0002CDC4
		public static BinaryExpression AddAssign(Expression left, Expression right)
		{
			return Expression.AddAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DDC RID: 3548 RVA: 0x0002EBCF File Offset: 0x0002CDCF
		public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AddAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DDD RID: 3549 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AddAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AddAssign, "op_Addition", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AddAssign, left, right, left.Type);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0002EC6C File Offset: 0x0002CE6C
		private static void ValidateOpAssignConversionLambda(LambdaExpression conversion, Expression left, MethodInfo method, ExpressionType nodeType)
		{
			MethodInfo invokeMethod = conversion.Type.GetInvokeMethod();
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(conversion, "conversion");
			}
			if (!TypeUtils.AreEquivalent(invokeMethod.ReturnType, left.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, conversion.ToString());
			}
			if (!TypeUtils.AreEquivalent(parametersCached[0].ParameterType, method.ReturnType))
			{
				throw Error.OverloadOperatorTypeDoesNotMatchConversionType(nodeType, conversion.ToString());
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DDF RID: 3551 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
		public static BinaryExpression AddAssignChecked(Expression left, Expression right)
		{
			return Expression.AddAssignChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002ECF2 File Offset: 0x0002CEF2
		public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AddAssignChecked(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002ED00 File Offset: 0x0002CF00
		public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AddAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AddAssignChecked, "op_Addition", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AddAssignChecked, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002ED8D File Offset: 0x0002CF8D
		public static BinaryExpression AddChecked(Expression left, Expression right)
		{
			return Expression.AddChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that has overflow checking. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002ED98 File Offset: 0x0002CF98
		public static BinaryExpression AddChecked(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.AddChecked, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.AddChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.AddChecked, "op_Addition", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Subtract" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002EE0C File Offset: 0x0002D00C
		public static BinaryExpression Subtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Subtract" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002EE18 File Offset: 0x0002D018
		public static BinaryExpression Subtract(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Subtract, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Subtract, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Subtract, "op_Subtraction", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002EE8F File Offset: 0x0002D08F
		public static BinaryExpression SubtractAssign(Expression left, Expression right)
		{
			return Expression.SubtractAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002EE9A File Offset: 0x0002D09A
		public static BinaryExpression SubtractAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.SubtractAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002EEA8 File Offset: 0x0002D0A8
		public static BinaryExpression SubtractAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.SubtractAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.SubtractAssign, "op_Subtraction", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.SubtractAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002EF35 File Offset: 0x0002D135
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right)
		{
			return Expression.SubtractAssignChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DEA RID: 3562 RVA: 0x0002EF3F File Offset: 0x0002D13F
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.SubtractAssignChecked(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DEB RID: 3563 RVA: 0x0002EF4C File Offset: 0x0002D14C
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.SubtractAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.SubtractAssignChecked, "op_Subtraction", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.SubtractAssignChecked, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DEC RID: 3564 RVA: 0x0002EFD9 File Offset: 0x0002D1D9
		public static BinaryExpression SubtractChecked(Expression left, Expression right)
		{
			return Expression.SubtractChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DED RID: 3565 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
		public static BinaryExpression SubtractChecked(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.SubtractChecked, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.SubtractChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.SubtractChecked, "op_Subtraction", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic division operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Divide" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The division operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DEE RID: 3566 RVA: 0x0002F05B File Offset: 0x0002D25B
		public static BinaryExpression Divide(Expression left, Expression right)
		{
			return Expression.Divide(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic division operation. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Divide" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the division operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DEF RID: 3567 RVA: 0x0002F068 File Offset: 0x0002D268
		public static BinaryExpression Divide(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Divide, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Divide, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Divide, "op_Division", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a division assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.DivideAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0002F0DF File Offset: 0x0002D2DF
		public static BinaryExpression DivideAssign(Expression left, Expression right)
		{
			return Expression.DivideAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a division assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.DivideAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002F0EA File Offset: 0x0002D2EA
		public static BinaryExpression DivideAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.DivideAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a division assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.DivideAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		public static BinaryExpression DivideAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.DivideAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.DivideAssign, "op_Division", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.DivideAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic remainder operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Modulo" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The modulus operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0002F185 File Offset: 0x0002D385
		public static BinaryExpression Modulo(Expression left, Expression right)
		{
			return Expression.Modulo(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic remainder operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Modulo" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the modulus operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002F190 File Offset: 0x0002D390
		public static BinaryExpression Modulo(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Modulo, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Modulo, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Modulo, "op_Modulus", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a remainder assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ModuloAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002F207 File Offset: 0x0002D407
		public static BinaryExpression ModuloAssign(Expression left, Expression right)
		{
			return Expression.ModuloAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a remainder assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ModuloAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002F212 File Offset: 0x0002D412
		public static BinaryExpression ModuloAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.ModuloAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a remainder assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ModuloAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002F220 File Offset: 0x0002D420
		public static BinaryExpression ModuloAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.ModuloAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.ModuloAssign, "op_Modulus", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.ModuloAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Multiply" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002F2AD File Offset: 0x0002D4AD
		public static BinaryExpression Multiply(Expression left, Expression right)
		{
			return Expression.Multiply(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Multiply" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		public static BinaryExpression Multiply(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Multiply, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Multiply, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Multiply, "op_Multiply", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0002F32F File Offset: 0x0002D52F
		public static BinaryExpression MultiplyAssign(Expression left, Expression right)
		{
			return Expression.MultiplyAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DFB RID: 3579 RVA: 0x0002F33A File Offset: 0x0002D53A
		public static BinaryExpression MultiplyAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.MultiplyAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that does not have overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DFC RID: 3580 RVA: 0x0002F348 File Offset: 0x0002D548
		public static BinaryExpression MultiplyAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.MultiplyAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.MultiplyAssign, "op_Multiply", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.MultiplyAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000DFD RID: 3581 RVA: 0x0002F3D5 File Offset: 0x0002D5D5
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right)
		{
			return Expression.MultiplyAssignChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000DFE RID: 3582 RVA: 0x0002F3DF File Offset: 0x0002D5DF
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.MultiplyAssignChecked(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000DFF RID: 3583 RVA: 0x0002F3EC File Offset: 0x0002D5EC
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.MultiplyAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.MultiplyAssignChecked, "op_Multiply", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.MultiplyAssignChecked, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E00 RID: 3584 RVA: 0x0002F479 File Offset: 0x0002D679
		public static BinaryExpression MultiplyChecked(Expression left, Expression right)
		{
			return Expression.MultiplyChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E01 RID: 3585 RVA: 0x0002F484 File Offset: 0x0002D684
		public static BinaryExpression MultiplyChecked(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.MultiplyChecked, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.MultiplyChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.MultiplyChecked, "op_Multiply", left, right, true);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002F4FB File Offset: 0x0002D6FB
		private static bool IsSimpleShift(Type left, Type right)
		{
			return left.IsInteger() && right.GetNonNullableType() == typeof(int);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002F51C File Offset: 0x0002D71C
		private static Type GetResultTypeOfShift(Type left, Type right)
		{
			if (!left.IsNullableType() && right.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[] { left });
			}
			return left;
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The left-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E04 RID: 3588 RVA: 0x0002F549 File Offset: 0x0002D749
		public static BinaryExpression LeftShift(Expression left, Expression right)
		{
			return Expression.LeftShift(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the left-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E05 RID: 3589 RVA: 0x0002F554 File Offset: 0x0002D754
		public static BinaryExpression LeftShift(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.LeftShift, left, right, method, true);
			}
			if (Expression.IsSimpleShift(left.Type, right.Type))
			{
				Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.LeftShift, left, right, resultTypeOfShift);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.LeftShift, "op_LeftShift", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000E06 RID: 3590 RVA: 0x0002F5CB File Offset: 0x0002D7CB
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right)
		{
			return Expression.LeftShiftAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000E07 RID: 3591 RVA: 0x0002F5D6 File Offset: 0x0002D7D6
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.LeftShiftAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000E08 RID: 3592 RVA: 0x0002F5E4 File Offset: 0x0002D7E4
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.LeftShiftAssign, left, right, method, conversion, true);
			}
			if (!Expression.IsSimpleShift(left.Type, right.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.LeftShiftAssign, "op_LeftShift", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
			return new SimpleBinaryExpression(ExpressionType.LeftShiftAssign, left, right, resultTypeOfShift);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The right-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E09 RID: 3593 RVA: 0x0002F671 File Offset: 0x0002D871
		public static BinaryExpression RightShift(Expression left, Expression right)
		{
			return Expression.RightShift(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the right-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E0A RID: 3594 RVA: 0x0002F67C File Offset: 0x0002D87C
		public static BinaryExpression RightShift(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.RightShift, left, right, method, true);
			}
			if (Expression.IsSimpleShift(left.Type, right.Type))
			{
				Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.RightShift, left, right, resultTypeOfShift);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.RightShift, "op_RightShift", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000E0B RID: 3595 RVA: 0x0002F6F3 File Offset: 0x0002D8F3
		public static BinaryExpression RightShiftAssign(Expression left, Expression right)
		{
			return Expression.RightShiftAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000E0C RID: 3596 RVA: 0x0002F6FE File Offset: 0x0002D8FE
		public static BinaryExpression RightShiftAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.RightShiftAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000E0D RID: 3597 RVA: 0x0002F70C File Offset: 0x0002D90C
		public static BinaryExpression RightShiftAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.RightShiftAssign, left, right, method, conversion, true);
			}
			if (!Expression.IsSimpleShift(left.Type, right.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.RightShiftAssign, "op_RightShift", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
			return new SimpleBinaryExpression(ExpressionType.RightShiftAssign, left, right, resultTypeOfShift);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.And" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise AND operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E0E RID: 3598 RVA: 0x0002F799 File Offset: 0x0002D999
		public static BinaryExpression And(Expression left, Expression right)
		{
			return Expression.And(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND operation. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.And" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the bitwise AND operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E0F RID: 3599 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		public static BinaryExpression And(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.And, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsIntegerOrBool())
			{
				return new SimpleBinaryExpression(ExpressionType.And, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.And, "op_BitwiseAnd", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000E10 RID: 3600 RVA: 0x0002F818 File Offset: 0x0002DA18
		public static BinaryExpression AndAssign(Expression left, Expression right)
		{
			return Expression.AndAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000E11 RID: 3601 RVA: 0x0002F823 File Offset: 0x0002DA23
		public static BinaryExpression AndAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AndAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000E12 RID: 3602 RVA: 0x0002F830 File Offset: 0x0002DA30
		public static BinaryExpression AndAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AndAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsIntegerOrBool())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AndAssign, "op_BitwiseAnd", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AndAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Or" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise OR operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0002F8BD File Offset: 0x0002DABD
		public static BinaryExpression Or(Expression left, Expression right)
		{
			return Expression.Or(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Or" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the bitwise OR operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E14 RID: 3604 RVA: 0x0002F8C8 File Offset: 0x0002DAC8
		public static BinaryExpression Or(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Or, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsIntegerOrBool())
			{
				return new SimpleBinaryExpression(ExpressionType.Or, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Or, "op_BitwiseOr", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000E15 RID: 3605 RVA: 0x0002F93F File Offset: 0x0002DB3F
		public static BinaryExpression OrAssign(Expression left, Expression right)
		{
			return Expression.OrAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000E16 RID: 3606 RVA: 0x0002F94A File Offset: 0x0002DB4A
		public static BinaryExpression OrAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.OrAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000E17 RID: 3607 RVA: 0x0002F958 File Offset: 0x0002DB58
		public static BinaryExpression OrAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.OrAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsIntegerOrBool())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.OrAssign, "op_BitwiseOr", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.OrAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOr" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The XOR operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E18 RID: 3608 RVA: 0x0002F9E5 File Offset: 0x0002DBE5
		public static BinaryExpression ExclusiveOr(Expression left, Expression right)
		{
			return Expression.ExclusiveOr(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR operation, using op_ExclusiveOr for user-defined types. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOr" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the XOR operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000E19 RID: 3609 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		public static BinaryExpression ExclusiveOr(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.ExclusiveOr, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsIntegerOrBool())
			{
				return new SimpleBinaryExpression(ExpressionType.ExclusiveOr, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.ExclusiveOr, "op_ExclusiveOr", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000E1A RID: 3610 RVA: 0x0002FA67 File Offset: 0x0002DC67
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right)
		{
			return Expression.ExclusiveOrAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000E1B RID: 3611 RVA: 0x0002FA72 File Offset: 0x0002DC72
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.ExclusiveOrAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000E1C RID: 3612 RVA: 0x0002FA80 File Offset: 0x0002DC80
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.ExclusiveOrAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsIntegerOrBool())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.ExclusiveOrAssign, "op_ExclusiveOr", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.ExclusiveOrAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising a number to a power.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Power" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The exponentiation operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-<paramref name="left" />.Type and/or <paramref name="right" />.Type are not <see cref="T:System.Double" />.</exception>
		// Token: 0x06000E1D RID: 3613 RVA: 0x0002FB0D File Offset: 0x0002DD0D
		public static BinaryExpression Power(Expression left, Expression right)
		{
			return Expression.Power(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising a number to a power.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Power" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="left" /> or <paramref name="right" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the exponentiation operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-<paramref name="method" /> is null and <paramref name="left" />.Type and/or <paramref name="right" />.Type are not <see cref="T:System.Double" />.</exception>
		// Token: 0x06000E1E RID: 3614 RVA: 0x0002FB18 File Offset: 0x0002DD18
		public static BinaryExpression Power(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				method = CachedReflectionInfo.Math_Pow_Double_Double;
				if (method == null)
				{
					throw Error.BinaryOperatorNotDefined(ExpressionType.Power, left.Type, right.Type);
				}
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.Power, left, right, method, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising an expression to a power and assigning the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.PowerAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		// Token: 0x06000E1F RID: 3615 RVA: 0x0002FB78 File Offset: 0x0002DD78
		public static BinaryExpression PowerAssign(Expression left, Expression right)
		{
			return Expression.PowerAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising an expression to a power and assigning the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.PowerAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		// Token: 0x06000E20 RID: 3616 RVA: 0x0002FB83 File Offset: 0x0002DD83
		public static BinaryExpression PowerAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.PowerAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising an expression to a power and assigning the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.PowerAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		// Token: 0x06000E21 RID: 3617 RVA: 0x0002FB90 File Offset: 0x0002DD90
		public static BinaryExpression PowerAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				method = CachedReflectionInfo.Math_Pow_Double_Double;
				if (method == null)
				{
					throw Error.BinaryOperatorNotDefined(ExpressionType.PowerAssign, left.Type, right.Type);
				}
			}
			return Expression.GetMethodBasedAssignOperator(ExpressionType.PowerAssign, left, right, method, conversion, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents applying an array index operator to an array of rank one.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ArrayIndex" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <param name="array">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="index">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> or <paramref name="index" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" />.Type does not represent an array type.-or-<paramref name="array" />.Type represents an array type whose rank is not 1.-or-<paramref name="index" />.Type does not represent the <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06000E22 RID: 3618 RVA: 0x0002FBFC File Offset: 0x0002DDFC
		public static BinaryExpression ArrayIndex(Expression array, Expression index)
		{
			ExpressionUtils.RequiresCanRead(array, "array");
			ExpressionUtils.RequiresCanRead(index, "index");
			if (index.Type != typeof(int))
			{
				throw Error.ArgumentMustBeArrayIndexType("index");
			}
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray("array");
			}
			if (type.GetArrayRank() != 1)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			return new SimpleBinaryExpression(ExpressionType.ArrayIndex, array, index, type.GetElementType());
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains two expressions and has no variables.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		// Token: 0x06000E23 RID: 3619 RVA: 0x0002FC78 File Offset: 0x0002DE78
		public static BlockExpression Block(Expression arg0, Expression arg1)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			return new Block2(arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains three expressions and has no variables.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <param name="arg2">The third expression in the block.</param>
		// Token: 0x06000E24 RID: 3620 RVA: 0x0002FC97 File Offset: 0x0002DE97
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			ExpressionUtils.RequiresCanRead(arg2, "arg2");
			return new Block3(arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains four expressions and has no variables.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <param name="arg2">The third expression in the block.</param>
		/// <param name="arg3">The fourth expression in the block.</param>
		// Token: 0x06000E25 RID: 3621 RVA: 0x0002FCC2 File Offset: 0x0002DEC2
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			ExpressionUtils.RequiresCanRead(arg2, "arg2");
			ExpressionUtils.RequiresCanRead(arg3, "arg3");
			return new Block4(arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains five expressions and has no variables.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <param name="arg2">The third expression in the block.</param>
		/// <param name="arg3">The fourth expression in the block.</param>
		/// <param name="arg4">The fifth expression in the block.</param>
		// Token: 0x06000E26 RID: 3622 RVA: 0x0002FCFC File Offset: 0x0002DEFC
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			ExpressionUtils.RequiresCanRead(arg2, "arg2");
			ExpressionUtils.RequiresCanRead(arg3, "arg3");
			ExpressionUtils.RequiresCanRead(arg4, "arg4");
			return new Block5(arg0, arg1, arg2, arg3, arg4);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions and has no variables.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E27 RID: 3623 RVA: 0x0002FD4C File Offset: 0x0002DF4C
		public static BlockExpression Block(params Expression[] expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			Expression.RequiresCanRead(expressions, "expressions");
			return Expression.GetOptimizedBlockExpression(expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions and has no variables.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E28 RID: 3624 RVA: 0x0002FD6A File Offset: 0x0002DF6A
		public static BlockExpression Block(IEnumerable<Expression> expressions)
		{
			return Expression.Block(EmptyReadOnlyCollection<ParameterExpression>.Instance, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions, has no variables and has specific result type.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="type">The result type of the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E29 RID: 3625 RVA: 0x0002FD77 File Offset: 0x0002DF77
		public static BlockExpression Block(Type type, params Expression[] expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			return Expression.Block(type, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions, has no variables and has specific result type.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="type">The result type of the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E2A RID: 3626 RVA: 0x0002FD8B File Offset: 0x0002DF8B
		public static BlockExpression Block(Type type, IEnumerable<Expression> expressions)
		{
			return Expression.Block(type, EmptyReadOnlyCollection<ParameterExpression>.Instance, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E2B RID: 3627 RVA: 0x0002FD99 File Offset: 0x0002DF99
		public static BlockExpression Block(IEnumerable<ParameterExpression> variables, params Expression[] expressions)
		{
			return Expression.Block(variables, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="type">The result type of the block.</param>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E2C RID: 3628 RVA: 0x0002FDA2 File Offset: 0x0002DFA2
		public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, params Expression[] expressions)
		{
			return Expression.Block(type, variables, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E2D RID: 3629 RVA: 0x0002FDAC File Offset: 0x0002DFAC
		public static BlockExpression Block(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = variables.ToReadOnly<ParameterExpression>();
			if (readOnlyCollection.Count == 0)
			{
				IReadOnlyList<Expression> readOnlyList = (expressions as IReadOnlyList<Expression>) ?? expressions.ToReadOnly<Expression>();
				Expression.RequiresCanRead(readOnlyList, "expressions");
				return Expression.GetOptimizedBlockExpression(readOnlyList);
			}
			ReadOnlyCollection<Expression> readOnlyCollection2 = expressions.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection2, "expressions");
			return Expression.BlockCore(null, readOnlyCollection, readOnlyCollection2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		/// <param name="type">The result type of the block.</param>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		// Token: 0x06000E2E RID: 3630 RVA: 0x0002FE10 File Offset: 0x0002E010
		public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(expressions, "expressions");
			ReadOnlyCollection<Expression> readOnlyCollection = expressions.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection, "expressions");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection2 = variables.ToReadOnly<ParameterExpression>();
			if (readOnlyCollection2.Count == 0 && readOnlyCollection.Count != 0)
			{
				int count = readOnlyCollection.Count;
				if (count != 0 && readOnlyCollection[count - 1].Type == type)
				{
					return Expression.GetOptimizedBlockExpression(readOnlyCollection);
				}
			}
			return Expression.BlockCore(type, readOnlyCollection2, readOnlyCollection);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002FE8C File Offset: 0x0002E08C
		private static BlockExpression BlockCore(Type type, ReadOnlyCollection<ParameterExpression> variables, ReadOnlyCollection<Expression> expressions)
		{
			Expression.ValidateVariables(variables, "variables");
			if (type != null)
			{
				if (expressions.Count == 0)
				{
					if (type != typeof(void))
					{
						throw Error.ArgumentTypesMustMatch();
					}
					return new ScopeWithType(variables, expressions, type);
				}
				else
				{
					Expression expression = expressions.Last<Expression>();
					if (type != typeof(void) && !TypeUtils.AreReferenceAssignable(type, expression.Type))
					{
						throw Error.ArgumentTypesMustMatch();
					}
					if (!TypeUtils.AreEquivalent(type, expression.Type))
					{
						return new ScopeWithType(variables, expressions, type);
					}
				}
			}
			int count = expressions.Count;
			if (count == 0)
			{
				return new ScopeWithType(variables, expressions, typeof(void));
			}
			if (count != 1)
			{
				return new ScopeN(variables, expressions);
			}
			return new Scope1(variables, expressions[0]);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002FF54 File Offset: 0x0002E154
		internal static void ValidateVariables(ReadOnlyCollection<ParameterExpression> varList, string collectionName)
		{
			int count = varList.Count;
			if (count != 0)
			{
				HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
				for (int i = 0; i < count; i++)
				{
					ParameterExpression parameterExpression = varList[i];
					ContractUtils.RequiresNotNull(parameterExpression, collectionName, i);
					if (parameterExpression.IsByRef)
					{
						throw Error.VariableMustNotBeByRef(parameterExpression, parameterExpression.Type, collectionName, i);
					}
					if (!hashSet.Add(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression, collectionName, i);
					}
				}
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		private static BlockExpression GetOptimizedBlockExpression(IReadOnlyList<Expression> expressions)
		{
			switch (expressions.Count)
			{
			case 0:
				return Expression.BlockCore(typeof(void), EmptyReadOnlyCollection<ParameterExpression>.Instance, EmptyReadOnlyCollection<Expression>.Instance);
			case 2:
				return new Block2(expressions[0], expressions[1]);
			case 3:
				return new Block3(expressions[0], expressions[1], expressions[2]);
			case 4:
				return new Block4(expressions[0], expressions[1], expressions[2], expressions[3]);
			case 5:
				return new Block5(expressions[0], expressions[1], expressions[2], expressions[3], expressions[4]);
			}
			IReadOnlyList<Expression> readOnlyList = expressions as ReadOnlyCollection<Expression>;
			return new BlockN(readOnlyList ?? expressions.ToArray<Expression>());
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of <see cref="T:System.Exception" /> this <see cref="T:System.Linq.Expressions.CatchBlock" /> will handle.</param>
		/// <param name="body">The body of the catch statement.</param>
		// Token: 0x06000E32 RID: 3634 RVA: 0x0003009A File Offset: 0x0002E29A
		public static CatchBlock Catch(Type type, Expression body)
		{
			return Expression.MakeCatchBlock(type, null, body, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with a reference to the caught <see cref="T:System.Exception" /> object for use in the handler body.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		/// <param name="variable">A <see cref="T:System.Linq.Expressions.ParameterExpression" /> representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</param>
		/// <param name="body">The body of the catch statement.</param>
		// Token: 0x06000E33 RID: 3635 RVA: 0x000300A5 File Offset: 0x0002E2A5
		public static CatchBlock Catch(ParameterExpression variable, Expression body)
		{
			ContractUtils.RequiresNotNull(variable, "variable");
			return Expression.MakeCatchBlock(variable.Type, variable, body, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with an <see cref="T:System.Exception" /> filter but no reference to the caught <see cref="T:System.Exception" /> object.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of <see cref="T:System.Exception" /> this <see cref="T:System.Linq.Expressions.CatchBlock" /> will handle.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <param name="filter">The body of the <see cref="T:System.Exception" /> filter.</param>
		// Token: 0x06000E34 RID: 3636 RVA: 0x000300C0 File Offset: 0x0002E2C0
		public static CatchBlock Catch(Type type, Expression body, Expression filter)
		{
			return Expression.MakeCatchBlock(type, null, body, filter);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with an <see cref="T:System.Exception" /> filter and a reference to the caught <see cref="T:System.Exception" /> object.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		/// <param name="variable">A <see cref="T:System.Linq.Expressions.ParameterExpression" /> representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <param name="filter">The body of the <see cref="T:System.Exception" /> filter.</param>
		// Token: 0x06000E35 RID: 3637 RVA: 0x000300CB File Offset: 0x0002E2CB
		public static CatchBlock Catch(ParameterExpression variable, Expression body, Expression filter)
		{
			ContractUtils.RequiresNotNull(variable, "variable");
			return Expression.MakeCatchBlock(variable.Type, variable, body, filter);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with the specified elements.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of <see cref="T:System.Exception" /> this <see cref="T:System.Linq.Expressions.CatchBlock" /> will handle.</param>
		/// <param name="variable">A <see cref="T:System.Linq.Expressions.ParameterExpression" /> representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <param name="filter">The body of the <see cref="T:System.Exception" /> filter.</param>
		// Token: 0x06000E36 RID: 3638 RVA: 0x000300E8 File Offset: 0x0002E2E8
		public static CatchBlock MakeCatchBlock(Type type, ParameterExpression variable, Expression body, Expression filter)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.Requires(variable == null || TypeUtils.AreEquivalent(variable.Type, type), "variable");
			if (variable == null)
			{
				TypeUtils.ValidateType(type, "type");
			}
			else if (variable.IsByRef)
			{
				throw Error.VariableMustNotBeByRef(variable, variable.Type, "variable");
			}
			ExpressionUtils.RequiresCanRead(body, "body");
			if (filter != null)
			{
				ExpressionUtils.RequiresCanRead(filter, "filter");
				if (filter.Type != typeof(bool))
				{
					throw Error.ArgumentMustBeBoolean("filter");
				}
			}
			return new CatchBlock(type, variable, body, filter);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, and <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> properties set to the specified values.</returns>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <param name="ifFalse">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="test" /> or <paramref name="ifTrue" /> or <paramref name="ifFalse" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="test" />.Type is not <see cref="T:System.Boolean" />.-or-<paramref name="ifTrue" />.Type is not equal to <paramref name="ifFalse" />.Type.</exception>
		// Token: 0x06000E37 RID: 3639 RVA: 0x0003018C File Offset: 0x0002E38C
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
		{
			ExpressionUtils.RequiresCanRead(test, "test");
			ExpressionUtils.RequiresCanRead(ifTrue, "ifTrue");
			ExpressionUtils.RequiresCanRead(ifFalse, "ifFalse");
			if (test.Type != typeof(bool))
			{
				throw Error.ArgumentMustBeBoolean("test");
			}
			if (!TypeUtils.AreEquivalent(ifTrue.Type, ifFalse.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConditionalExpression.Make(test, ifTrue, ifFalse, ifTrue.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, and <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> properties set to the specified values.</returns>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <param name="ifFalse">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property equal to.</param>
		/// <param name="type">A <see cref="P:System.Linq.Expressions.Expression.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E38 RID: 3640 RVA: 0x00030204 File Offset: 0x0002E404
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			ExpressionUtils.RequiresCanRead(test, "test");
			ExpressionUtils.RequiresCanRead(ifTrue, "ifTrue");
			ExpressionUtils.RequiresCanRead(ifFalse, "ifFalse");
			ContractUtils.RequiresNotNull(type, "type");
			if (test.Type != typeof(bool))
			{
				throw Error.ArgumentMustBeBoolean("test");
			}
			if (type != typeof(void) && (!TypeUtils.AreReferenceAssignable(type, ifTrue.Type) || !TypeUtils.AreReferenceAssignable(type, ifFalse.Type)))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConditionalExpression.Make(test, ifTrue, ifFalse, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional block with an if statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, properties set to the specified values. The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property is set to default expression and the type of the resulting <see cref="T:System.Linq.Expressions.ConditionalExpression" /> returned by this method is <see cref="T:System.Void" />.</returns>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		// Token: 0x06000E39 RID: 3641 RVA: 0x0003029C File Offset: 0x0002E49C
		public static ConditionalExpression IfThen(Expression test, Expression ifTrue)
		{
			return Expression.Condition(test, ifTrue, Expression.Empty(), typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional block with if and else statements.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, and <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> properties set to the specified values. The type of the resulting <see cref="T:System.Linq.Expressions.ConditionalExpression" /> returned by this method is <see cref="T:System.Void" />.</returns>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <param name="ifFalse">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property equal to.</param>
		// Token: 0x06000E3A RID: 3642 RVA: 0x000302B4 File Offset: 0x0002E4B4
		public static ConditionalExpression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse)
		{
			return Expression.Condition(test, ifTrue, ifFalse, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property set to the specified value.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Constant" /> and the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property set to the specified value.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> to set the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property equal to.</param>
		// Token: 0x06000E3B RID: 3643 RVA: 0x000302C8 File Offset: 0x0002E4C8
		public static ConstantExpression Constant(object value)
		{
			return new ConstantExpression(value);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Constant" /> and the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> to set the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not null and <paramref name="type" /> is not assignable from the dynamic type of <paramref name="value" />.</exception>
		// Token: 0x06000E3C RID: 3644 RVA: 0x000302D0 File Offset: 0x0002E4D0
		public static ConstantExpression Constant(object value, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (value == null)
			{
				if (type == typeof(object))
				{
					return new ConstantExpression(null);
				}
				if (!type.IsValueType || type.IsNullableType())
				{
					return new TypedConstantExpression(null, type);
				}
			}
			else
			{
				Type type2 = value.GetType();
				if (type == type2)
				{
					return new ConstantExpression(value);
				}
				if (type.IsAssignableFrom(type2))
				{
					return new TypedConstantExpression(value, type);
				}
			}
			throw Error.ArgumentTypesMustMatch();
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> with the specified span.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</returns>
		/// <param name="document">The <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</param>
		/// <param name="startLine">The start line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. Must be greater than 0.</param>
		/// <param name="startColumn">The start column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. Must be greater than 0.</param>
		/// <param name="endLine">The end line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. Must be greater or equal than the start line.</param>
		/// <param name="endColumn">The end column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. If the end line is the same as the start line, it must be greater or equal than the start column. In any case, must be greater than 0.</param>
		// Token: 0x06000E3D RID: 3645 RVA: 0x00030354 File Offset: 0x0002E554
		public static DebugInfoExpression DebugInfo(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
		{
			ContractUtils.RequiresNotNull(document, "document");
			if (startLine == 16707566 && startColumn == 0 && endLine == 16707566 && endColumn == 0)
			{
				return new ClearDebugInfoExpression(document);
			}
			Expression.ValidateSpan(startLine, startColumn, endLine, endColumn);
			return new SpanDebugInfoExpression(document, startLine, startColumn, endLine, endColumn);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> for clearing a sequence point.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> for clearning a sequence point.</returns>
		/// <param name="document">The <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</param>
		// Token: 0x06000E3E RID: 3646 RVA: 0x00030394 File Offset: 0x0002E594
		public static DebugInfoExpression ClearDebugInfo(SymbolDocumentInfo document)
		{
			ContractUtils.RequiresNotNull(document, "document");
			return new ClearDebugInfoExpression(document);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000303A8 File Offset: 0x0002E5A8
		private static void ValidateSpan(int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine < 1)
			{
				throw Error.OutOfRange("startLine", 1);
			}
			if (startColumn < 1)
			{
				throw Error.OutOfRange("startColumn", 1);
			}
			if (endLine < 1)
			{
				throw Error.OutOfRange("endLine", 1);
			}
			if (endColumn < 1)
			{
				throw Error.OutOfRange("endColumn", 1);
			}
			if (startLine > endLine)
			{
				throw Error.StartEndMustBeOrdered();
			}
			if (startLine == endLine && startColumn > endColumn)
			{
				throw Error.StartEndMustBeOrdered();
			}
		}

		/// <summary>Creates an empty expression that has <see cref="T:System.Void" /> type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DefaultExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Default" /> and the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <see cref="T:System.Void" />.</returns>
		// Token: 0x06000E40 RID: 3648 RVA: 0x00030421 File Offset: 0x0002E621
		public static DefaultExpression Empty()
		{
			return new DefaultExpression(typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DefaultExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to the specified type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DefaultExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Default" /> and the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E41 RID: 3649 RVA: 0x00030432 File Offset: 0x0002E632
		public static DefaultExpression Default(Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			return new DefaultExpression(type);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.ElementInit" />, given an array of values as the second argument.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.ElementInit" /> that has the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> and <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> properties set to the specified values.</returns>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to set the <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addMethod" /> or <paramref name="arguments" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The method that addMethod represents is not named "Add" (case insensitive).-or-The method that addMethod represents is not an instance method.-or-arguments does not contain the same number of elements as the number of parameters for the method that addMethod represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the method that <paramref name="addMethod" /> represents.</exception>
		// Token: 0x06000E42 RID: 3650 RVA: 0x00030450 File Offset: 0x0002E650
		public static ElementInit ElementInit(MethodInfo addMethod, params Expression[] arguments)
		{
			return Expression.ElementInit(addMethod, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.ElementInit" />, given an <see cref="T:System.Collections.Generic.IEnumerable`1" /> as the second argument.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.ElementInit" /> that has the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> and <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> properties set to the specified values.</returns>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to set the <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addMethod" /> or <paramref name="arguments" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The method that <paramref name="addMethod" /> represents is not named "Add" (case insensitive).-or-The method that <paramref name="addMethod" /> represents is not an instance method.-or-<paramref name="arguments" /> does not contain the same number of elements as the number of parameters for the method that <paramref name="addMethod" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the method that <paramref name="addMethod" /> represents.</exception>
		// Token: 0x06000E43 RID: 3651 RVA: 0x0003045C File Offset: 0x0002E65C
		public static ElementInit ElementInit(MethodInfo addMethod, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(addMethod, "addMethod");
			ContractUtils.RequiresNotNull(arguments, "arguments");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection, "arguments");
			Expression.ValidateElementInitAddMethodInfo(addMethod, "addMethod");
			Expression.ValidateArgumentTypes(addMethod, ExpressionType.Call, ref readOnlyCollection, "addMethod");
			return new ElementInit(addMethod, readOnlyCollection);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000304B4 File Offset: 0x0002E6B4
		private static void ValidateElementInitAddMethodInfo(MethodInfo addMethod, string paramName)
		{
			Expression.ValidateMethodInfo(addMethod, paramName);
			ParameterInfo[] parametersCached = addMethod.GetParametersCached();
			if (parametersCached.Length == 0)
			{
				throw Error.ElementInitializerMethodWithZeroArgs(paramName);
			}
			if (!addMethod.Name.Equals("Add", StringComparison.OrdinalIgnoreCase))
			{
				throw Error.ElementInitializerMethodNotAdd(paramName);
			}
			if (addMethod.IsStatic)
			{
				throw Error.ElementInitializerMethodStatic(paramName);
			}
			foreach (ParameterInfo parameterInfo in parametersCached)
			{
				if (parameterInfo.ParameterType.IsByRef)
				{
					throw Error.ElementInitializerMethodNoRefOutParam(parameterInfo.Name, addMethod.Name, paramName);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.Expressions.Expression" /> class.</summary>
		/// <param name="nodeType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> to set as the node type.</param>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of this <see cref="T:System.Linq.Expressions.Expression" />.</param>
		// Token: 0x06000E45 RID: 3653 RVA: 0x00030535 File Offset: 0x0002E735
		[Obsolete("use a different constructor that does not take ExpressionType. Then override NodeType and Type properties to provide the values that would be specified to this constructor.")]
		protected Expression(ExpressionType nodeType, Type type)
		{
			if (Expression.s_legacyCtorSupportTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<Expression, Expression.ExtensionInfo>>(ref Expression.s_legacyCtorSupportTable, new ConditionalWeakTable<Expression, Expression.ExtensionInfo>(), null);
			}
			Expression.s_legacyCtorSupportTable.Add(this, new Expression.ExtensionInfo(nodeType, type));
		}

		/// <summary>Constructs a new instance of <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		// Token: 0x06000E46 RID: 3654 RVA: 0x00002320 File Offset: 0x00000520
		protected Expression()
		{
		}

		/// <summary>Gets the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>One of the <see cref="T:System.Linq.Expressions.ExpressionType" /> values.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00030568 File Offset: 0x0002E768
		public virtual ExpressionType NodeType
		{
			get
			{
				Expression.ExtensionInfo extensionInfo;
				if (Expression.s_legacyCtorSupportTable != null && Expression.s_legacyCtorSupportTable.TryGetValue(this, out extensionInfo))
				{
					return extensionInfo.NodeType;
				}
				throw Error.ExtensionNodeMustOverrideProperty("Expression.NodeType");
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="T:System.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0003059C File Offset: 0x0002E79C
		public virtual Type Type
		{
			get
			{
				Expression.ExtensionInfo extensionInfo;
				if (Expression.s_legacyCtorSupportTable != null && Expression.s_legacyCtorSupportTable.TryGetValue(this, out extensionInfo))
				{
					return extensionInfo.Type;
				}
				throw Error.ExtensionNodeMustOverrideProperty("Expression.Type");
			}
		}

		/// <summary>Indicates that the node can be reduced to a simpler node. If this returns true, Reduce() can be called to produce the reduced form.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00002285 File Offset: 0x00000485
		public virtual bool CanReduce
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reduces this node to a simpler expression. If CanReduce returns true, this should return a valid expression. This method can return another node which itself must be reduced.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000E4A RID: 3658 RVA: 0x000305D0 File Offset: 0x0002E7D0
		public virtual Expression Reduce()
		{
			if (this.CanReduce)
			{
				throw Error.ReducibleMustOverrideReduce();
			}
			return this;
		}

		/// <summary>Reduces the node and then calls the visitor delegate on the reduced expression. The method throws an exception if the node is not reducible.</summary>
		/// <returns>The expression being visited, or an expression which should replace it in the tree.</returns>
		/// <param name="visitor">An instance of <see cref="T:System.Func`2" />.</param>
		// Token: 0x06000E4B RID: 3659 RVA: 0x000305E1 File Offset: 0x0002E7E1
		protected internal virtual Expression VisitChildren(ExpressionVisitor visitor)
		{
			if (!this.CanReduce)
			{
				throw Error.MustBeReducible();
			}
			return visitor.Visit(this.ReduceAndCheck());
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06000E4C RID: 3660 RVA: 0x000305FD File Offset: 0x0002E7FD
		protected internal virtual Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitExtension(this);
		}

		/// <summary>Reduces this node to a simpler expression. If CanReduce returns true, this should return a valid expression. This method can return another node which itself must be reduced.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000E4D RID: 3661 RVA: 0x00030608 File Offset: 0x0002E808
		public Expression ReduceAndCheck()
		{
			if (!this.CanReduce)
			{
				throw Error.MustBeReducible();
			}
			Expression expression = this.Reduce();
			if (expression == null || expression == this)
			{
				throw Error.MustReduceToDifferent();
			}
			if (!TypeUtils.AreReferenceAssignable(this.Type, expression.Type))
			{
				throw Error.ReducedNotCompatible();
			}
			return expression;
		}

		/// <summary>Reduces the expression to a known node type (that is not an Extension node) or just returns the expression if it is already a known type.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000E4E RID: 3662 RVA: 0x00030654 File Offset: 0x0002E854
		public Expression ReduceExtensions()
		{
			Expression expression = this;
			while (expression.NodeType == ExpressionType.Extension)
			{
				expression = expression.ReduceAndCheck();
			}
			return expression;
		}

		/// <summary>Returns a textual representation of the <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.Expression" />.</returns>
		// Token: 0x06000E4F RID: 3663 RVA: 0x00030677 File Offset: 0x0002E877
		public override string ToString()
		{
			return ExpressionStringBuilder.ExpressionToString(this);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00030680 File Offset: 0x0002E880
		private string DebugView
		{
			get
			{
				string text;
				using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture))
				{
					DebugViewWriter.WriteTo(this, stringWriter);
					text = stringWriter.ToString();
				}
				return text;
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000306C4 File Offset: 0x0002E8C4
		private static void RequiresCanRead(IReadOnlyList<Expression> items, string paramName)
		{
			int i = 0;
			int count = items.Count;
			while (i < count)
			{
				ExpressionUtils.RequiresCanRead(items[i], paramName, i);
				i++;
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000306F4 File Offset: 0x0002E8F4
		private static void RequiresCanWrite(Expression expression, string paramName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException(paramName);
			}
			ExpressionType nodeType = expression.NodeType;
			if (nodeType != ExpressionType.MemberAccess)
			{
				if (nodeType == ExpressionType.Parameter)
				{
					return;
				}
				if (nodeType == ExpressionType.Index)
				{
					PropertyInfo indexer = ((IndexExpression)expression).Indexer;
					if (indexer == null || indexer.CanWrite)
					{
						return;
					}
				}
			}
			else
			{
				MemberInfo member = ((MemberExpression)expression).Member;
				PropertyInfo propertyInfo = member as PropertyInfo;
				if (propertyInfo != null)
				{
					if (propertyInfo.CanWrite)
					{
						return;
					}
				}
				else
				{
					FieldInfo fieldInfo = (FieldInfo)member;
					if (!fieldInfo.IsInitOnly && !fieldInfo.IsLiteral)
					{
						return;
					}
				}
			}
			throw Error.ExpressionMustBeWriteable(paramName);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06000E53 RID: 3667 RVA: 0x00030786 File Offset: 0x0002E986
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			return DynamicExpression.Dynamic(binder, returnType, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		// Token: 0x06000E54 RID: 3668 RVA: 0x00030790 File Offset: 0x0002E990
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		// Token: 0x06000E55 RID: 3669 RVA: 0x0003079A File Offset: 0x0002E99A
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		// Token: 0x06000E56 RID: 3670 RVA: 0x000307A5 File Offset: 0x0002E9A5
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <param name="arg3">The fourth argument to the dynamic operation.</param>
		// Token: 0x06000E57 RID: 3671 RVA: 0x000307B2 File Offset: 0x0002E9B2
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06000E58 RID: 3672 RVA: 0x000307C1 File Offset: 0x0002E9C1
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return DynamicExpression.Dynamic(binder, returnType, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06000E59 RID: 3673 RVA: 0x000307CB File Offset: 0x0002E9CB
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and one argument.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The argument to the dynamic operation.</param>
		// Token: 0x06000E5A RID: 3674 RVA: 0x000307D5 File Offset: 0x0002E9D5
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and two arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		// Token: 0x06000E5B RID: 3675 RVA: 0x000307DF File Offset: 0x0002E9DF
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and three arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		// Token: 0x06000E5C RID: 3676 RVA: 0x000307EA File Offset: 0x0002E9EA
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and four arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <param name="arg3">The fourth argument to the dynamic operation.</param>
		// Token: 0x06000E5D RID: 3677 RVA: 0x000307F7 File Offset: 0x0002E9F7
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		// Token: 0x06000E5E RID: 3678 RVA: 0x00030806 File Offset: 0x0002EA06
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return Expression.MakeDynamic(delegateType, binder, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		// Token: 0x06000E5F RID: 3679 RVA: 0x00030810 File Offset: 0x0002EA10
		public static GotoExpression Break(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement. The value passed to the label upon jumping can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		// Token: 0x06000E60 RID: 3680 RVA: 0x00030824 File Offset: 0x0002EA24
		public static GotoExpression Break(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement with the specified type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E61 RID: 3681 RVA: 0x00030838 File Offset: 0x0002EA38
		public static GotoExpression Break(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement with the specified type. The value passed to the label upon jumping can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E62 RID: 3682 RVA: 0x00030843 File Offset: 0x0002EA43
		public static GotoExpression Break(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, value, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a continue statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		// Token: 0x06000E63 RID: 3683 RVA: 0x0003084E File Offset: 0x0002EA4E
		public static GotoExpression Continue(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Continue, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a continue statement with the specified type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E64 RID: 3684 RVA: 0x00030862 File Offset: 0x0002EA62
		public static GotoExpression Continue(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Continue, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Return, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		// Token: 0x06000E65 RID: 3685 RVA: 0x0003086D File Offset: 0x0002EA6D
		public static GotoExpression Return(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement with the specified type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Return, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E66 RID: 3686 RVA: 0x00030881 File Offset: 0x0002EA81
		public static GotoExpression Return(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement. The value passed to the label upon jumping can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		// Token: 0x06000E67 RID: 3687 RVA: 0x0003088C File Offset: 0x0002EA8C
		public static GotoExpression Return(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement with the specified type. The value passed to the label upon jumping can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E68 RID: 3688 RVA: 0x000308A0 File Offset: 0x0002EAA0
		public static GotoExpression Return(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, value, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to the specified value, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		// Token: 0x06000E69 RID: 3689 RVA: 0x000308AB File Offset: 0x0002EAAB
		public static GotoExpression Goto(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement with the specified type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to the specified value, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and a null value to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E6A RID: 3690 RVA: 0x000308BF File Offset: 0x0002EABF
		public static GotoExpression Goto(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement. The value passed to the label upon jumping can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		// Token: 0x06000E6B RID: 3691 RVA: 0x000308CA File Offset: 0x0002EACA
		public static GotoExpression Goto(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement with the specified type. The value passed to the label upon jumping can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E6C RID: 3692 RVA: 0x000308DE File Offset: 0x0002EADE
		public static GotoExpression Goto(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, value, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a jump of the specified <see cref="T:System.Linq.Expressions.GotoExpressionKind" />. The value passed to the label upon jumping can also be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to <paramref name="kind" />, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		/// <param name="kind">The <see cref="T:System.Linq.Expressions.GotoExpressionKind" /> of the <see cref="T:System.Linq.Expressions.GotoExpression" />.</param>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		// Token: 0x06000E6D RID: 3693 RVA: 0x000308E9 File Offset: 0x0002EAE9
		public static GotoExpression MakeGoto(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			Expression.ValidateGoto(target, ref value, "target", "value", type);
			return new GotoExpression(kind, target, value, type);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00030908 File Offset: 0x0002EB08
		private static void ValidateGoto(LabelTarget target, ref Expression value, string targetParameter, string valueParameter, Type type)
		{
			ContractUtils.RequiresNotNull(target, targetParameter);
			if (value == null)
			{
				if (target.Type != typeof(void))
				{
					throw Error.LabelMustBeVoidOrHaveExpression("target");
				}
				if (type != null)
				{
					TypeUtils.ValidateType(type, "type");
					return;
				}
			}
			else
			{
				Expression.ValidateGotoType(target.Type, ref value, valueParameter);
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00030968 File Offset: 0x0002EB68
		private static void ValidateGotoType(Type expectedType, ref Expression value, string paramName)
		{
			ExpressionUtils.RequiresCanRead(value, paramName);
			if (expectedType != typeof(void) && !TypeUtils.AreReferenceAssignable(expectedType, value.Type) && !Expression.TryQuote(expectedType, ref value))
			{
				throw Error.ExpressionTypeDoesNotMatchLabel(value.Type, expectedType);
			}
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> that represents accessing an indexed property in an object.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		/// <param name="instance">The object to which the property belongs. It should be null if the property is static (shared in Visual Basic).</param>
		/// <param name="indexer">An <see cref="T:System.Linq.Expressions.Expression" /> representing the property to index.</param>
		/// <param name="arguments">An IEnumerable&lt;Expression&gt; (IEnumerable (Of Expression) in Visual Basic) that contains the arguments that will be used to index the property.</param>
		// Token: 0x06000E70 RID: 3696 RVA: 0x000309B5 File Offset: 0x0002EBB5
		public static IndexExpression MakeIndex(Expression instance, PropertyInfo indexer, IEnumerable<Expression> arguments)
		{
			if (indexer != null)
			{
				return Expression.Property(instance, indexer, arguments);
			}
			return Expression.ArrayAccess(instance, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> to access an array.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		/// <param name="array">An expression representing the array to index.</param>
		/// <param name="indexes">An array that contains expressions used to index the array.</param>
		// Token: 0x06000E71 RID: 3697 RVA: 0x000309D0 File Offset: 0x0002EBD0
		public static IndexExpression ArrayAccess(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayAccess(array, indexes);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> to access a multidimensional array.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		/// <param name="array">An expression that represents the multidimensional array.</param>
		/// <param name="indexes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> containing expressions used to index the array.</param>
		// Token: 0x06000E72 RID: 3698 RVA: 0x000309DC File Offset: 0x0002EBDC
		public static IndexExpression ArrayAccess(Expression array, IEnumerable<Expression> indexes)
		{
			ExpressionUtils.RequiresCanRead(array, "array");
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray("array");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnly<Expression>();
			if (type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			foreach (Expression expression in readOnlyCollection)
			{
				ExpressionUtils.RequiresCanRead(expression, "indexes");
				if (expression.Type != typeof(int))
				{
					throw Error.ArgumentMustBeArrayIndexType("indexes");
				}
			}
			return new IndexExpression(array, null, readOnlyCollection);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> representing the access to an indexed property.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		/// <param name="instance">The object to which the property belongs. If the property is static/shared, it must be null.</param>
		/// <param name="propertyName">The name of the indexer.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that are used to index the property.</param>
		// Token: 0x06000E73 RID: 3699 RVA: 0x00030A90 File Offset: 0x0002EC90
		public static IndexExpression Property(Expression instance, string propertyName, params Expression[] arguments)
		{
			ExpressionUtils.RequiresCanRead(instance, "instance");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo propertyInfo = Expression.FindInstanceProperty(instance.Type, propertyName, arguments);
			return Expression.MakeIndexProperty(instance, propertyInfo, "propertyName", arguments.ToReadOnly<Expression>());
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00030AD4 File Offset: 0x0002ECD4
		private static PropertyInfo FindInstanceProperty(Type type, string propertyName, Expression[] arguments)
		{
			BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
			PropertyInfo propertyInfo = Expression.FindProperty(type, propertyName, arguments, bindingFlags);
			if (propertyInfo == null)
			{
				bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
				propertyInfo = Expression.FindProperty(type, propertyName, arguments, bindingFlags);
			}
			if (!(propertyInfo == null))
			{
				return propertyInfo;
			}
			if (arguments == null || arguments.Length == 0)
			{
				throw Error.InstancePropertyWithoutParameterNotDefinedForType(propertyName, type);
			}
			throw Error.InstancePropertyWithSpecifiedParametersNotDefinedForType(propertyName, Expression.GetArgTypesString(arguments), type, "propertyName");
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00030B30 File Offset: 0x0002ED30
		private static string GetArgTypesString(Expression[] arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			for (int i = 0; i < arguments.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				StringBuilder stringBuilder2 = stringBuilder;
				Expression expression = arguments[i];
				stringBuilder2.Append((expression != null) ? expression.Type.Name : null);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00030B94 File Offset: 0x0002ED94
		private static PropertyInfo FindProperty(Type type, string propertyName, Expression[] arguments, BindingFlags flags)
		{
			PropertyInfo propertyInfo = null;
			foreach (PropertyInfo propertyInfo2 in type.GetProperties(flags))
			{
				if (propertyInfo2.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase) && Expression.IsCompatible(propertyInfo2, arguments))
				{
					if (!(propertyInfo == null))
					{
						throw Error.PropertyWithMoreThanOneMatch(propertyName, type);
					}
					propertyInfo = propertyInfo2;
				}
			}
			return propertyInfo;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00030BEC File Offset: 0x0002EDEC
		private static bool IsCompatible(PropertyInfo pi, Expression[] args)
		{
			MethodInfo methodInfo = pi.GetGetMethod(true);
			ParameterInfo[] array;
			if (methodInfo != null)
			{
				array = methodInfo.GetParametersCached();
			}
			else
			{
				methodInfo = pi.GetSetMethod(true);
				if (methodInfo == null)
				{
					return false;
				}
				array = methodInfo.GetParametersCached();
				if (array.Length == 0)
				{
					return false;
				}
				array = array.RemoveLast<ParameterInfo>();
			}
			if (args == null)
			{
				return array.Length == 0;
			}
			if (array.Length != args.Length)
			{
				return false;
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == null)
				{
					return false;
				}
				if (!TypeUtils.AreReferenceAssignable(array[i].ParameterType, args[i].Type))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> representing the access to an indexed property.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		/// <param name="instance">The object to which the property belongs. If the property is static/shared, it must be null.</param>
		/// <param name="indexer">The <see cref="T:System.Reflection.PropertyInfo" /> that represents the property to index.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that are used to index the property.</param>
		// Token: 0x06000E78 RID: 3704 RVA: 0x00030C7C File Offset: 0x0002EE7C
		public static IndexExpression Property(Expression instance, PropertyInfo indexer, params Expression[] arguments)
		{
			return Expression.Property(instance, indexer, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> representing the access to an indexed property.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		/// <param name="instance">The object to which the property belongs. If the property is static/shared, it must be null.</param>
		/// <param name="indexer">The <see cref="T:System.Reflection.PropertyInfo" /> that represents the property to index.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects that are used to index the property.</param>
		// Token: 0x06000E79 RID: 3705 RVA: 0x00030C86 File Offset: 0x0002EE86
		public static IndexExpression Property(Expression instance, PropertyInfo indexer, IEnumerable<Expression> arguments)
		{
			return Expression.MakeIndexProperty(instance, indexer, "indexer", arguments.ToReadOnly<Expression>());
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00030C9A File Offset: 0x0002EE9A
		private static IndexExpression MakeIndexProperty(Expression instance, PropertyInfo indexer, string paramName, ReadOnlyCollection<Expression> argList)
		{
			Expression.ValidateIndexedProperty(instance, indexer, paramName, ref argList);
			return new IndexExpression(instance, indexer, argList);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00030CB0 File Offset: 0x0002EEB0
		private static void ValidateIndexedProperty(Expression instance, PropertyInfo indexer, string paramName, ref ReadOnlyCollection<Expression> argList)
		{
			ContractUtils.RequiresNotNull(indexer, paramName);
			if (indexer.PropertyType.IsByRef)
			{
				throw Error.PropertyCannotHaveRefType(paramName);
			}
			if (indexer.PropertyType == typeof(void))
			{
				throw Error.PropertyTypeCannotBeVoid(paramName);
			}
			ParameterInfo[] array = null;
			MethodInfo getMethod = indexer.GetGetMethod(true);
			if (getMethod != null)
			{
				if (getMethod.ReturnType != indexer.PropertyType)
				{
					throw Error.PropertyTypeMustMatchGetter(paramName);
				}
				array = getMethod.GetParametersCached();
				Expression.ValidateAccessor(instance, getMethod, array, ref argList, paramName);
			}
			MethodInfo setMethod = indexer.GetSetMethod(true);
			if (setMethod != null)
			{
				ParameterInfo[] parametersCached = setMethod.GetParametersCached();
				if (parametersCached.Length == 0)
				{
					throw Error.SetterHasNoParams(paramName);
				}
				Type parameterType = parametersCached[parametersCached.Length - 1].ParameterType;
				if (parameterType.IsByRef)
				{
					throw Error.PropertyCannotHaveRefType(paramName);
				}
				if (setMethod.ReturnType != typeof(void))
				{
					throw Error.SetterMustBeVoid(paramName);
				}
				if (indexer.PropertyType != parameterType)
				{
					throw Error.PropertyTypeMustMatchSetter(paramName);
				}
				if (!(getMethod != null))
				{
					Expression.ValidateAccessor(instance, setMethod, parametersCached.RemoveLast<ParameterInfo>(), ref argList, paramName);
					return;
				}
				if (getMethod.IsStatic ^ setMethod.IsStatic)
				{
					throw Error.BothAccessorsMustBeStatic(paramName);
				}
				if (array.Length != parametersCached.Length - 1)
				{
					throw Error.IndexesOfSetGetMustMatch(paramName);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ParameterType != parametersCached[i].ParameterType)
					{
						throw Error.IndexesOfSetGetMustMatch(paramName);
					}
				}
				return;
			}
			else
			{
				if (getMethod == null)
				{
					throw Error.PropertyDoesNotHaveAccessor(indexer, paramName);
				}
				return;
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00030E30 File Offset: 0x0002F030
		private static void ValidateAccessor(Expression instance, MethodInfo method, ParameterInfo[] indexes, ref ReadOnlyCollection<Expression> arguments, string paramName)
		{
			ContractUtils.RequiresNotNull(arguments, "arguments");
			Expression.ValidateMethodInfo(method, "method");
			if ((method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				throw Error.AccessorsCannotHaveVarArgs(paramName);
			}
			if (method.IsStatic)
			{
				if (instance != null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("instance");
				}
			}
			else
			{
				if (instance == null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("instance");
				}
				ExpressionUtils.RequiresCanRead(instance, "instance");
				Expression.ValidateCallInstanceType(instance.Type, method);
			}
			Expression.ValidateAccessorArgumentTypes(method, indexes, ref arguments, paramName);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00030EAC File Offset: 0x0002F0AC
		private static void ValidateAccessorArgumentTypes(MethodInfo method, ParameterInfo[] indexes, ref ReadOnlyCollection<Expression> arguments, string paramName)
		{
			if (indexes.Length != 0)
			{
				if (indexes.Length != arguments.Count)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(method, paramName);
				}
				Expression[] array = null;
				int i = 0;
				int num = indexes.Length;
				while (i < num)
				{
					Expression expression = arguments[i];
					ParameterInfo parameterInfo = indexes[i];
					ExpressionUtils.RequiresCanRead(expression, "arguments", i);
					Type parameterType = parameterInfo.ParameterType;
					if (parameterType.IsByRef)
					{
						throw Error.AccessorsCannotHaveByRefArgs("indexes", i);
					}
					TypeUtils.ValidateType(parameterType, "indexes", i);
					if (!TypeUtils.AreReferenceAssignable(parameterType, expression.Type) && !Expression.TryQuote(parameterType, ref expression))
					{
						throw Error.ExpressionTypeDoesNotMatchMethodParameter(expression.Type, parameterType, method, "arguments", i);
					}
					if (array == null && expression != arguments[i])
					{
						array = new Expression[arguments.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = arguments[j];
						}
					}
					if (array != null)
					{
						array[i] = expression;
					}
					i++;
				}
				if (array != null)
				{
					arguments = new TrueReadOnlyCollection<Expression>(array);
					return;
				}
			}
			else if (arguments.Count > 0)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, paramName);
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00030FBC File Offset: 0x0002F1BC
		internal static InvocationExpression Invoke(Expression expression)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 0, parametersForValidation);
			return new InvocationExpression0(expression, invokeMethod.ReturnType);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00030FFC File Offset: 0x0002F1FC
		internal static InvocationExpression Invoke(Expression expression, Expression arg0)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 1, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			return new InvocationExpression1(expression, invokeMethod.ReturnType, arg0);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00031054 File Offset: 0x0002F254
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 2, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			return new InvocationExpression2(expression, invokeMethod.ReturnType, arg0, arg1);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000310C4 File Offset: 0x0002F2C4
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1, Expression arg2)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 3, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			arg2 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg2, parametersForValidation[2], "expression", "arg2");
			return new InvocationExpression3(expression, invokeMethod.ReturnType, arg0, arg1, arg2);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00031150 File Offset: 0x0002F350
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 4, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			arg2 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg2, parametersForValidation[2], "expression", "arg2");
			arg3 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg3, parametersForValidation[3], "expression", "arg3");
			return new InvocationExpression4(expression, invokeMethod.ReturnType, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000311F4 File Offset: 0x0002F3F4
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 5, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			arg2 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg2, parametersForValidation[2], "expression", "arg2");
			arg3 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg3, parametersForValidation[3], "expression", "arg3");
			arg4 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg4, parametersForValidation[4], "expression", "arg4");
			return new InvocationExpression5(expression, invokeMethod.ReturnType, arg0, arg1, arg2, arg3, arg4);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies a delegate or lambda expression to a list of argument expressions.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies the specified delegate or lambda expression to the provided arguments.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate or lambda expression to be applied.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments that the delegate or lambda expression is applied to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="expression" />.Type does not represent a delegate type or an <see cref="T:System.Linq.Expressions.Expression`1" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the delegate represented by <paramref name="expression" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="arguments" /> does not contain the same number of elements as the list of parameters for the delegate represented by <paramref name="expression" />.</exception>
		// Token: 0x06000E84 RID: 3716 RVA: 0x000312B3 File Offset: 0x0002F4B3
		public static InvocationExpression Invoke(Expression expression, params Expression[] arguments)
		{
			return Expression.Invoke(expression, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies a delegate or lambda expression to a list of argument expressions.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies the specified delegate or lambda expression to the provided arguments.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate or lambda expression to be applied to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments that the delegate or lambda expression is applied to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="expression" />.Type does not represent a delegate type or an <see cref="T:System.Linq.Expressions.Expression`1" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the delegate represented by <paramref name="expression" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="arguments" /> does not contain the same number of elements as the list of parameters for the delegate represented by <paramref name="expression" />.</exception>
		// Token: 0x06000E85 RID: 3717 RVA: 0x000312BC File Offset: 0x0002F4BC
		public static InvocationExpression Invoke(Expression expression, IEnumerable<Expression> arguments)
		{
			IReadOnlyList<Expression> readOnlyList = (arguments as IReadOnlyList<Expression>) ?? arguments.ToReadOnly<Expression>();
			switch (readOnlyList.Count)
			{
			case 0:
				return Expression.Invoke(expression);
			case 1:
				return Expression.Invoke(expression, readOnlyList[0]);
			case 2:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1]);
			case 3:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1], readOnlyList[2]);
			case 4:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3]);
			case 5:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3], readOnlyList[4]);
			default:
			{
				ExpressionUtils.RequiresCanRead(expression, "expression");
				ReadOnlyCollection<Expression> readOnlyCollection = readOnlyList.ToReadOnly<Expression>();
				MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
				Expression.ValidateArgumentTypes(invokeMethod, ExpressionType.Invoke, ref readOnlyCollection, "expression");
				return new InvocationExpressionN(expression, readOnlyCollection, invokeMethod.ReturnType);
			}
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000313CC File Offset: 0x0002F5CC
		internal static MethodInfo GetInvokeMethod(Expression expression)
		{
			Type type = expression.Type;
			if (!expression.Type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				Type type2 = TypeUtils.FindGenericType(typeof(Expression<>), expression.Type);
				if (type2 == null)
				{
					throw Error.ExpressionTypeNotInvocable(expression.Type, "expression");
				}
				type = type2.GetGenericArguments()[0];
			}
			return type.GetInvokeMethod();
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelExpression" /> representing a label without a default value.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LabelExpression" /> without a default value.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this <see cref="T:System.Linq.Expressions.LabelExpression" /> will be associated with.</param>
		// Token: 0x06000E87 RID: 3719 RVA: 0x00031434 File Offset: 0x0002F634
		public static LabelExpression Label(LabelTarget target)
		{
			return Expression.Label(target, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelExpression" /> representing a label with the given default value.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LabelExpression" /> with the given default value.</returns>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this <see cref="T:System.Linq.Expressions.LabelExpression" /> will be associated with.</param>
		/// <param name="defaultValue">The value of this <see cref="T:System.Linq.Expressions.LabelExpression" /> when the label is reached through regular control flow.</param>
		// Token: 0x06000E88 RID: 3720 RVA: 0x0003143D File Offset: 0x0002F63D
		public static LabelExpression Label(LabelTarget target, Expression defaultValue)
		{
			Expression.ValidateGoto(target, ref defaultValue, "target", "defaultValue", null);
			return new LabelExpression(target, defaultValue);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with void type and no name.</summary>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		// Token: 0x06000E89 RID: 3721 RVA: 0x00031459 File Offset: 0x0002F659
		public static LabelTarget Label()
		{
			return Expression.Label(typeof(void), null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with void type and the given name.</summary>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		/// <param name="name">The name of the label.</param>
		// Token: 0x06000E8A RID: 3722 RVA: 0x0003146B File Offset: 0x0002F66B
		public static LabelTarget Label(string name)
		{
			return Expression.Label(typeof(void), name);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with the given type.</summary>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		/// <param name="type">The type of value that is passed when jumping to the label.</param>
		// Token: 0x06000E8B RID: 3723 RVA: 0x0003147D File Offset: 0x0002F67D
		public static LabelTarget Label(Type type)
		{
			return Expression.Label(type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with the given type and name.</summary>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		/// <param name="type">The type of value that is passed when jumping to the label.</param>
		/// <param name="name">The name of the label.</param>
		// Token: 0x06000E8C RID: 3724 RVA: 0x00031486 File Offset: 0x0002F686
		public static LabelTarget Label(Type type, string name)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			return new LabelTarget(type, name);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000314A8 File Offset: 0x0002F6A8
		internal static LambdaExpression CreateLambda(Type delegateType, Expression body, string name, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters)
		{
			CacheDict<Type, Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>> cacheDict = Expression.s_lambdaFactories;
			if (cacheDict == null)
			{
				cacheDict = (Expression.s_lambdaFactories = new CacheDict<Type, Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>>(50));
			}
			MethodInfo methodInfo = null;
			Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression> func;
			if (!cacheDict.TryGetValue(delegateType, out func))
			{
				methodInfo = typeof(Expression<>).MakeGenericType(new Type[] { delegateType }).GetMethod("Create", BindingFlags.Static | BindingFlags.NonPublic);
				if (delegateType.CanCache())
				{
					func = (cacheDict[delegateType] = (Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>)methodInfo.CreateDelegate(typeof(Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>)));
				}
			}
			if (func != null)
			{
				return func(body, name, tailCall, parameters);
			}
			return (LambdaExpression)methodInfo.Invoke(null, new object[] { body, name, tailCall, parameters });
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">A delegate type.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="body" /> is null.-or-One or more elements in <paramref name="parameters" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TDelegate" /> is not a delegate type.-or-<paramref name="body" />.Type represents a type that is not assignable to the return type of <paramref name="TDelegate" />.-or-<paramref name="parameters" /> does not contain the same number of elements as the list of parameters for <paramref name="TDelegate" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of <paramref name="TDelegate" />.</exception>
		// Token: 0x06000E8E RID: 3726 RVA: 0x00031560 File Offset: 0x0002F760
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, false, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An array that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		// Token: 0x06000E8F RID: 3727 RVA: 0x0003156A File Offset: 0x0002F76A
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, tailCall, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">A delegate type.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="body" /> is null.-or-One or more elements in <paramref name="parameters" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TDelegate" /> is not a delegate type.-or-<paramref name="body" />.Type represents a type that is not assignable to the return type of <paramref name="TDelegate" />.-or-<paramref name="parameters" /> does not contain the same number of elements as the list of parameters for <paramref name="TDelegate" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of <paramref name="TDelegate" />.</exception>
		// Token: 0x06000E90 RID: 3728 RVA: 0x00031574 File Offset: 0x0002F774
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, null, false, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" />and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		// Token: 0x06000E91 RID: 3729 RVA: 0x0003157F File Offset: 0x0002F77F
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, null, tailCall, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression`1.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.Expression`1.Body" /> and <see cref="P:System.Linq.Expressions.Expression`1.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.Expression`1.Body" /> property equal to.</param>
		/// <param name="name">The name of the lambda. Used for generating debugging information.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.Expression`1.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		// Token: 0x06000E92 RID: 3730 RVA: 0x0003158A File Offset: 0x0002F78A
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, name, false, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name of the lambda. Used for generating debugging info.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		// Token: 0x06000E93 RID: 3731 RVA: 0x00031598 File Offset: 0x0002F798
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(typeof(TDelegate), ref body, readOnlyCollection, "TDelegate");
			return (Expression<TDelegate>)Expression.CreateLambda(typeof(TDelegate), body, name, tailCall, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LambdaExpression" /> by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="body" /> is null.-or-One or more elements of <paramref name="parameters" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parameters" /> contains more than sixteen elements.</exception>
		// Token: 0x06000E94 RID: 3732 RVA: 0x000315DB File Offset: 0x0002F7DB
		public static LambdaExpression Lambda(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(body, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An array that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E95 RID: 3733 RVA: 0x000315E5 File Offset: 0x0002F7E5
		public static LambdaExpression Lambda(Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(body, tailCall, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x000315EF File Offset: 0x0002F7EF
		public static LambdaExpression Lambda(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, null, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E97 RID: 3735 RVA: 0x000315FA File Offset: 0x0002F7FA
		public static LambdaExpression Lambda(Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, null, tailCall, parameters);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LambdaExpression" /> by first constructing a delegate type. It can be used when the delegate type is not known at compile time.</summary>
		/// <returns>An object that represents a lambda expression which has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="delegateType">A <see cref="T:System.Type" /> that represents a delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="delegateType" /> or <paramref name="body" /> is null.-or-One or more elements in <paramref name="parameters" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="delegateType" /> does not represent a delegate type.-or-<paramref name="body" />.Type represents a type that is not assignable to the return type of the delegate type represented by <paramref name="delegateType" />.-or-<paramref name="parameters" /> does not contain the same number of elements as the list of parameters for the delegate type represented by <paramref name="delegateType" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of the delegate type represented by <paramref name="delegateType" />.</exception>
		// Token: 0x06000E98 RID: 3736 RVA: 0x00031605 File Offset: 0x0002F805
		public static LambdaExpression Lambda(Type delegateType, Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, null, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An array that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E99 RID: 3737 RVA: 0x00031611 File Offset: 0x0002F811
		public static LambdaExpression Lambda(Type delegateType, Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, null, tailCall, parameters);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LambdaExpression" /> by first constructing a delegate type. It can be used when the delegate type is not known at compile time.</summary>
		/// <returns>An object that represents a lambda expression which has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="delegateType">A <see cref="T:System.Type" /> that represents a delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="delegateType" /> or <paramref name="body" /> is null.-or-One or more elements in <paramref name="parameters" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="delegateType" /> does not represent a delegate type.-or-<paramref name="body" />.Type represents a type that is not assignable to the return type of the delegate type represented by <paramref name="delegateType" />.-or-<paramref name="parameters" /> does not contain the same number of elements as the list of parameters for the delegate type represented by <paramref name="delegateType" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of the delegate type represented by <paramref name="delegateType" />.</exception>
		// Token: 0x06000E9A RID: 3738 RVA: 0x00031605 File Offset: 0x0002F805
		public static LambdaExpression Lambda(Type delegateType, Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(delegateType, body, null, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E9B RID: 3739 RVA: 0x00031611 File Offset: 0x0002F811
		public static LambdaExpression Lambda(Type delegateType, Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(delegateType, body, null, tailCall, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E9C RID: 3740 RVA: 0x0003161D File Offset: 0x0002F81D
		public static LambdaExpression Lambda(Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, name, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E9D RID: 3741 RVA: 0x00031628 File Offset: 0x0002F828
		public static LambdaExpression Lambda(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ContractUtils.RequiresNotNull(body, "body");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnly<ParameterExpression>();
			int count = readOnlyCollection.Count;
			Type[] array = new Type[count + 1];
			if (count > 0)
			{
				HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
				for (int i = 0; i < count; i++)
				{
					ParameterExpression parameterExpression = readOnlyCollection[i];
					ContractUtils.RequiresNotNull(parameterExpression, "parameter");
					array[i] = (parameterExpression.IsByRef ? parameterExpression.Type.MakeByRefType() : parameterExpression.Type);
					if (!hashSet.Add(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression, "parameters", i);
					}
				}
			}
			array[count] = body.Type;
			return Expression.CreateLambda(DelegateHelpers.MakeDelegateType(array), body, name, tailCall, readOnlyCollection);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x000316DC File Offset: 0x0002F8DC
		public static LambdaExpression Lambda(Type delegateType, Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(delegateType, ref body, readOnlyCollection, "delegateType");
			return Expression.CreateLambda(delegateType, body, name, false, readOnlyCollection);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to. </param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression. </param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection. </param>
		// Token: 0x06000E9F RID: 3743 RVA: 0x00031708 File Offset: 0x0002F908
		public static LambdaExpression Lambda(Type delegateType, Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(delegateType, ref body, readOnlyCollection, "delegateType");
			return Expression.CreateLambda(delegateType, body, name, tailCall, readOnlyCollection);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00031738 File Offset: 0x0002F938
		private static void ValidateLambdaArgs(Type delegateType, ref Expression body, ReadOnlyCollection<ParameterExpression> parameters, string paramName)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ExpressionUtils.RequiresCanRead(body, "body");
			if (!typeof(MulticastDelegate).IsAssignableFrom(delegateType) || delegateType == typeof(MulticastDelegate))
			{
				throw Error.LambdaTypeMustBeDerivedFromSystemDelegate(paramName);
			}
			TypeUtils.ValidateType(delegateType, "delegateType", true, true);
			CacheDict<Type, MethodInfo> cacheDict = Expression.s_lambdaDelegateCache;
			MethodInfo invokeMethod;
			if (!cacheDict.TryGetValue(delegateType, out invokeMethod))
			{
				invokeMethod = delegateType.GetInvokeMethod();
				if (delegateType.CanCache())
				{
					cacheDict[delegateType] = invokeMethod;
				}
			}
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length != 0)
			{
				if (parametersCached.Length != parameters.Count)
				{
					throw Error.IncorrectNumberOfLambdaDeclarationParameters();
				}
				HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
				int i = 0;
				int num = parametersCached.Length;
				while (i < num)
				{
					ParameterExpression parameterExpression = parameters[i];
					ParameterInfo parameterInfo = parametersCached[i];
					ExpressionUtils.RequiresCanRead(parameterExpression, "parameters", i);
					Type type = parameterInfo.ParameterType;
					if (parameterExpression.IsByRef)
					{
						if (!type.IsByRef)
						{
							throw Error.ParameterExpressionNotValidAsDelegate(parameterExpression.Type.MakeByRefType(), type);
						}
						type = type.GetElementType();
					}
					if (!TypeUtils.AreReferenceAssignable(parameterExpression.Type, type))
					{
						throw Error.ParameterExpressionNotValidAsDelegate(parameterExpression.Type, type);
					}
					if (!hashSet.Add(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression, "parameters", i);
					}
					i++;
				}
			}
			else if (parameters.Count > 0)
			{
				throw Error.IncorrectNumberOfLambdaDeclarationParameters();
			}
			if (invokeMethod.ReturnType != typeof(void) && !TypeUtils.AreReferenceAssignable(invokeMethod.ReturnType, body.Type) && !Expression.TryQuote(invokeMethod.ReturnType, ref body))
			{
				throw Error.ExpressionTypeDoesNotMatchReturn(body.Type, invokeMethod.ReturnType);
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000318E4 File Offset: 0x0002FAE4
		private static Expression.TryGetFuncActionArgsResult ValidateTryGetFuncActionArgs(Type[] typeArgs)
		{
			if (typeArgs == null)
			{
				return Expression.TryGetFuncActionArgsResult.ArgumentNull;
			}
			foreach (Type type in typeArgs)
			{
				if (type == null)
				{
					return Expression.TryGetFuncActionArgsResult.ArgumentNull;
				}
				if (type.IsByRef)
				{
					return Expression.TryGetFuncActionArgsResult.ByRef;
				}
				if (type == typeof(void) || type.IsPointer)
				{
					return Expression.TryGetFuncActionArgsResult.PointerOrVoid;
				}
			}
			return Expression.TryGetFuncActionArgsResult.Valid;
		}

		/// <summary>Creates a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Func delegate type that has specific type arguments. The last type argument specifies the return type of the created delegate.</summary>
		/// <returns>The type of a System.Func delegate that has the specified type arguments.</returns>
		/// <param name="typeArgs">An array of one to seventeen <see cref="T:System.Type" /> objects that specify the type arguments for the System.Func delegate type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeArgs" /> contains fewer than one or more than seventeen elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArgs" /> is null.</exception>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003193C File Offset: 0x0002FB3C
		public static Type GetFuncType(params Type[] typeArgs)
		{
			Expression.TryGetFuncActionArgsResult tryGetFuncActionArgsResult = Expression.ValidateTryGetFuncActionArgs(typeArgs);
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ArgumentNull)
			{
				throw new ArgumentNullException("typeArgs");
			}
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ByRef)
			{
				throw Error.TypeMustNotBeByRef("typeArgs");
			}
			Type funcType = DelegateHelpers.GetFuncType(typeArgs);
			if (funcType == null)
			{
				throw Error.IncorrectNumberOfTypeArgsForFunc("typeArgs");
			}
			return funcType;
		}

		/// <summary>Creates a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Func delegate type that has specific type arguments. The last type argument specifies the return type of the created delegate.</summary>
		/// <returns>true if generic System.Func delegate type was created for specific <paramref name="typeArgs" />; false otherwise.</returns>
		/// <param name="typeArgs">An array of Type objects that specify the type arguments for the System.Func delegate type.</param>
		/// <param name="funcType">When this method returns, contains the generic System.Func delegate type that has specific type arguments. Contains null if there is no generic System.Func delegate that matches the <paramref name="typeArgs" />.This parameter is passed uninitialized.</param>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003198C File Offset: 0x0002FB8C
		public static bool TryGetFuncType(Type[] typeArgs, out Type funcType)
		{
			if (Expression.ValidateTryGetFuncActionArgs(typeArgs) == Expression.TryGetFuncActionArgsResult.Valid)
			{
				Type funcType2;
				funcType = (funcType2 = DelegateHelpers.GetFuncType(typeArgs));
				return funcType2 != null;
			}
			funcType = null;
			return false;
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object that represents a generic System.Action delegate type that has specific type arguments.</summary>
		/// <returns>The type of a System.Action delegate that has the specified type arguments.</returns>
		/// <param name="typeArgs">An array of up to sixteen <see cref="T:System.Type" /> objects that specify the type arguments for the System.Action delegate type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeArgs" /> contains more than sixteen elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArgs" /> is null.</exception>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000319B8 File Offset: 0x0002FBB8
		public static Type GetActionType(params Type[] typeArgs)
		{
			Expression.TryGetFuncActionArgsResult tryGetFuncActionArgsResult = Expression.ValidateTryGetFuncActionArgs(typeArgs);
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ArgumentNull)
			{
				throw new ArgumentNullException("typeArgs");
			}
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ByRef)
			{
				throw Error.TypeMustNotBeByRef("typeArgs");
			}
			Type actionType = DelegateHelpers.GetActionType(typeArgs);
			if (actionType == null)
			{
				throw Error.IncorrectNumberOfTypeArgsForAction("typeArgs");
			}
			return actionType;
		}

		/// <summary>Creates a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Action delegate type that has specific type arguments.</summary>
		/// <returns>true if generic System.Action delegate type was created for specific <paramref name="typeArgs" />; false otherwise.</returns>
		/// <param name="typeArgs">An array of Type objects that specify the type arguments for the System.Action delegate type.</param>
		/// <param name="actionType">When this method returns, contains the generic System.Action delegate type that has specific type arguments. Contains null if there is no generic System.Action delegate that matches the <paramref name="typeArgs" />.This parameter is passed uninitialized.</param>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x00031A08 File Offset: 0x0002FC08
		public static bool TryGetActionType(Type[] typeArgs, out Type actionType)
		{
			if (Expression.ValidateTryGetFuncActionArgs(typeArgs) == Expression.TryGetFuncActionArgsResult.Valid)
			{
				Type actionType2;
				actionType = (actionType2 = DelegateHelpers.GetActionType(typeArgs));
				return actionType2 != null;
			}
			actionType = null;
			return false;
		}

		/// <summary>Gets a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Func or System.Action delegate type that has specific type arguments.</summary>
		/// <returns>The delegate type.</returns>
		/// <param name="typeArgs">The type arguments of the delegate.</param>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00031A33 File Offset: 0x0002FC33
		public static Type GetDelegateType(params Type[] typeArgs)
		{
			ContractUtils.RequiresNotEmpty<Type>(typeArgs, "typeArgs");
			ContractUtils.RequiresNotNullItems<Type>(typeArgs, "typeArgs");
			return DelegateHelpers.MakeDelegateType(typeArgs);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a method named "Add" to add elements to a collection.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="initializers" /> is null.-or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no instance method named "Add" (case insensitive) declared in <paramref name="newExpression" />.Type or its base type.-or-The add method on <paramref name="newExpression" />.Type or its base type does not take exactly one argument.-or-The type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of the first element of <paramref name="initializers" /> is not assignable to the argument type of the add method on <paramref name="newExpression" />.Type or its base type.-or-More than one argument-compatible method named "Add" (case-insensitive) exists on <paramref name="newExpression" />.Type and/or its base type.</exception>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x00031A51 File Offset: 0x0002FC51
		public static ListInitExpression ListInit(NewExpression newExpression, params Expression[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a method named "Add" to add elements to a collection.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="initializers" /> is null.-or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no instance method named "Add" (case insensitive) declared in <paramref name="newExpression" />.Type or its base type.-or-The add method on <paramref name="newExpression" />.Type or its base type does not take exactly one argument.-or-The type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of the first element of <paramref name="initializers" /> is not assignable to the argument type of the add method on <paramref name="newExpression" />.Type or its base type.-or-More than one argument-compatible method named "Add" (case-insensitive) exists on <paramref name="newExpression" />.Type and/or its base type.</exception>
		// Token: 0x06000EA8 RID: 3752 RVA: 0x00031A5C File Offset: 0x0002FC5C
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<Expression> initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			if (readOnlyCollection.Count == 0)
			{
				return new ListInitExpression(newExpression, EmptyReadOnlyCollection<global::System.Linq.Expressions.ElementInit>.Instance);
			}
			MethodInfo methodInfo = Expression.FindMethod(newExpression.Type, "Add", null, new Expression[] { readOnlyCollection[0] }, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return Expression.ListInit(newExpression, methodInfo, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a specified method to add elements to a collection.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method that takes one argument, that adds an element to a collection.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="initializers" /> is null.-or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.-or-<paramref name="addMethod" /> is not null and it does not represent an instance method named "Add" (case insensitive) that takes exactly one argument.-or-<paramref name="addMethod" /> is not null and the type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="initializers" /> is not assignable to the argument type of the method that <paramref name="addMethod" /> represents.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="addMethod" /> is null and no instance method named "Add" that takes one type-compatible argument exists on <paramref name="newExpression" />.Type or its base type.</exception>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00031AC6 File Offset: 0x0002FCC6
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, params Expression[] initializers)
		{
			return Expression.ListInit(newExpression, addMethod, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a specified method to add elements to a collection.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method named "Add" (case insensitive), that adds an element to a collection.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="initializers" /> is null.-or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.-or-<paramref name="addMethod" /> is not null and it does not represent an instance method named "Add" (case insensitive) that takes exactly one argument.-or-<paramref name="addMethod" /> is not null and the type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="initializers" /> is not assignable to the argument type of the method that <paramref name="addMethod" /> represents.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="addMethod" /> is null and no instance method named "Add" that takes one type-compatible argument exists on <paramref name="newExpression" />.Type or its base type.</exception>
		// Token: 0x06000EAA RID: 3754 RVA: 0x00031AD0 File Offset: 0x0002FCD0
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, IEnumerable<Expression> initializers)
		{
			if (addMethod == null)
			{
				return Expression.ListInit(newExpression, initializers);
			}
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			ElementInit[] array = new ElementInit[readOnlyCollection.Count];
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				array[i] = Expression.ElementInit(addMethod, new Expression[] { readOnlyCollection[i] });
			}
			return Expression.ListInit(newExpression, new TrueReadOnlyCollection<ElementInit>(array));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses specified <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> properties set to the specified values.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="initializers" /> is null.-or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000EAB RID: 3755 RVA: 0x00031B4D File Offset: 0x0002FD4D
		public static ListInitExpression ListInit(NewExpression newExpression, params ElementInit[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses specified <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> properties set to the specified values.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="initializers" /> is null.-or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000EAC RID: 3756 RVA: 0x00031B58 File Offset: 0x0002FD58
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<ElementInit> readOnlyCollection = initializers.ToReadOnly<ElementInit>();
			Expression.ValidateListInitArgs(newExpression.Type, readOnlyCollection, "newExpression");
			return new ListInitExpression(newExpression, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LoopExpression" /> with the given body.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.LoopExpression" />.</returns>
		/// <param name="body">The body of the loop.</param>
		// Token: 0x06000EAD RID: 3757 RVA: 0x00031B9A File Offset: 0x0002FD9A
		public static LoopExpression Loop(Expression body)
		{
			return Expression.Loop(body, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LoopExpression" /> with the given body and break target.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.LoopExpression" />.</returns>
		/// <param name="body">The body of the loop.</param>
		/// <param name="break">The break target used by the loop body.</param>
		// Token: 0x06000EAE RID: 3758 RVA: 0x00031BA3 File Offset: 0x0002FDA3
		public static LoopExpression Loop(Expression body, LabelTarget @break)
		{
			return Expression.Loop(body, @break, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LoopExpression" /> with the given body.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.LoopExpression" />.</returns>
		/// <param name="body">The body of the loop.</param>
		/// <param name="break">The break target used by the loop body.</param>
		/// <param name="continue">The continue target used by the loop body.</param>
		// Token: 0x06000EAF RID: 3759 RVA: 0x00031BAD File Offset: 0x0002FDAD
		public static LoopExpression Loop(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			ExpressionUtils.RequiresCanRead(body, "body");
			if (@continue != null && @continue.Type != typeof(void))
			{
				throw Error.LabelTypeMustBeVoid("continue");
			}
			return new LoopExpression(body, @break, @continue);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberAssignment" /> that represents the initialization of a field or property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberAssignment" /> that has <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> equal to <see cref="F:System.Linq.Expressions.MemberBindingType.Assignment" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> properties set to the specified values.</returns>
		/// <param name="member">A <see cref="T:System.Reflection.MemberInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="member" /> or <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> does not represent a field or property.-or-The property represented by <paramref name="member" /> does not have a set accessor.-or-<paramref name="expression" />.Type is not assignable to the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		public static MemberAssignment Bind(MemberInfo member, Expression expression)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ExpressionUtils.RequiresCanRead(expression, "expression");
			Type type;
			Expression.ValidateSettableFieldOrPropertyMember(member, out type);
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return new MemberAssignment(member, expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberAssignment" /> that represents the initialization of a member by using a property accessor method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberAssignment" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.Assignment" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and the <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property set to <paramref name="expression" />.</returns>
		/// <param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyAccessor" /> or <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The property accessed by <paramref name="propertyAccessor" /> does not have a set accessor.-or-<paramref name="expression" />.Type is not assignable to the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00031C2E File Offset: 0x0002FE2E
		public static MemberAssignment Bind(MethodInfo propertyAccessor, Expression expression)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(expression, "expression");
			Expression.ValidateMethodInfo(propertyAccessor, "propertyAccessor");
			return Expression.Bind(Expression.GetProperty(propertyAccessor, "propertyAccessor", -1), expression);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00031C64 File Offset: 0x0002FE64
		private static void ValidateSettableFieldOrPropertyMember(MemberInfo member, out Type memberType)
		{
			Type declaringType = member.DeclaringType;
			if (declaringType == null)
			{
				throw Error.NotAMemberOfAnyType(member, "member");
			}
			TypeUtils.ValidateType(declaringType, null);
			if (member != null)
			{
				PropertyInfo propertyInfo;
				if ((propertyInfo = member as PropertyInfo) == null)
				{
					FieldInfo fieldInfo;
					if ((fieldInfo = member as FieldInfo) != null)
					{
						FieldInfo fieldInfo2 = fieldInfo;
						memberType = fieldInfo2.FieldType;
						return;
					}
				}
				else
				{
					PropertyInfo propertyInfo2 = propertyInfo;
					if (!propertyInfo2.CanWrite)
					{
						throw Error.PropertyDoesNotHaveSetter(propertyInfo2, "member");
					}
					memberType = propertyInfo2.PropertyType;
					return;
				}
			}
			throw Error.ArgumentMustBeFieldInfoOrPropertyInfo("member");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a field.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" /> and the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> and <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property equal to. For static (Shared in Visual Basic), <paramref name="expression" /> must be null.</param>
		/// <param name="field">The <see cref="T:System.Reflection.FieldInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="field" /> is null.-or-The field represented by <paramref name="field" /> is not static (Shared in Visual Basic) and <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="expression" />.Type is not assignable to the declaring type of the field represented by <paramref name="field" />.</exception>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00031CE4 File Offset: 0x0002FEE4
		public static MemberExpression Field(Expression expression, FieldInfo field)
		{
			ContractUtils.RequiresNotNull(field, "field");
			if (field.IsStatic)
			{
				if (expression != null)
				{
					throw Error.OnlyStaticFieldsHaveNullInstance("expression");
				}
			}
			else
			{
				if (expression == null)
				{
					throw Error.OnlyStaticFieldsHaveNullInstance("field");
				}
				ExpressionUtils.RequiresCanRead(expression, "expression");
				if (!TypeUtils.AreReferenceAssignable(field.DeclaringType, expression.Type))
				{
					throw Error.FieldInfoNotDefinedForType(field.DeclaringType, field.Name, expression.Type);
				}
			}
			return MemberExpression.Make(expression, field);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a field given the name of the field.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" />, and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.FieldInfo" /> that represents the field denoted by <paramref name="fieldName" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> contains a field named <paramref name="fieldName" />. This can be null for static fields.</param>
		/// <param name="fieldName">The name of a field to be accessed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="fieldName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">No field named <paramref name="fieldName" /> is defined in <paramref name="expression" />.Type or its base types.</exception>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x00031D60 File Offset: 0x0002FF60
		public static MemberExpression Field(Expression expression, string fieldName)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(fieldName, "fieldName");
			FieldInfo fieldInfo = expression.Type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? expression.Type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (fieldInfo == null)
			{
				throw Error.InstanceFieldNotDefinedForType(fieldName, expression.Type);
			}
			return Expression.Field(expression, fieldInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a field.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.MemberExpression" />.</returns>
		/// <param name="expression">The containing object of the field. This can be null for static fields.</param>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> that contains the field.</param>
		/// <param name="fieldName">The field to be accessed.</param>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00031DC4 File Offset: 0x0002FFC4
		public static MemberExpression Field(Expression expression, Type type, string fieldName)
		{
			ContractUtils.RequiresNotNull(type, "type");
			FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (fieldInfo == null)
			{
				throw Error.FieldNotDefinedForType(fieldName, type);
			}
			return Expression.Field(expression, fieldInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" />, and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property denoted by <paramref name="propertyName" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> contains a property named <paramref name="propertyName" />. This can be null for static properties.</param>
		/// <param name="propertyName">The name of a property to be accessed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="propertyName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">No property named <paramref name="propertyName" /> is defined in <paramref name="expression" />.Type or its base types.</exception>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x00031E0C File Offset: 0x0003000C
		public static MemberExpression Property(Expression expression, string propertyName)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo propertyInfo = expression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? expression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (propertyInfo == null)
			{
				throw Error.InstancePropertyNotDefinedForType(propertyName, expression.Type, "propertyName");
			}
			return Expression.Property(expression, propertyInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> accessing a property.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.MemberExpression" />.</returns>
		/// <param name="expression">The containing object of the property. This can be null for static properties.</param>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> that contains the property.</param>
		/// <param name="propertyName">The property to be accessed.</param>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00031E74 File Offset: 0x00030074
		public static MemberExpression Property(Expression expression, Type type, string propertyName)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (propertyInfo == null)
			{
				throw Error.PropertyNotDefinedForType(propertyName, type, "propertyName");
			}
			return Expression.Property(expression, propertyInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" /> and the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> and <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property equal to. This can be null for static properties.</param>
		/// <param name="property">The <see cref="T:System.Reflection.PropertyInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="property" /> is null.-or-The property that <paramref name="property" /> represents is not static (Shared in Visual Basic) and <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="expression" />.Type is not assignable to the declaring type of the property that <paramref name="property" /> represents.</exception>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00031ECC File Offset: 0x000300CC
		public static MemberExpression Property(Expression expression, PropertyInfo property)
		{
			ContractUtils.RequiresNotNull(property, "property");
			MethodInfo methodInfo = property.GetGetMethod(true);
			if (methodInfo == null)
			{
				methodInfo = property.GetSetMethod(true);
				if (methodInfo == null)
				{
					throw Error.PropertyDoesNotHaveAccessor(property, "property");
				}
				if (methodInfo.GetParametersCached().Length != 1)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(methodInfo, "property");
				}
			}
			else if (methodInfo.GetParametersCached().Length != 0)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(methodInfo, "property");
			}
			if (methodInfo.IsStatic)
			{
				if (expression != null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("expression");
				}
			}
			else
			{
				if (expression == null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("property");
				}
				ExpressionUtils.RequiresCanRead(expression, "expression");
				if (!TypeUtils.IsValidInstanceType(property, expression.Type))
				{
					throw Error.PropertyNotDefinedForType(property, expression.Type, "property");
				}
			}
			Expression.ValidateMethodInfo(methodInfo, "property");
			return MemberExpression.Make(expression, property);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property by using a property accessor method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" /> and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property equal to. This can be null for static properties.</param>
		/// <param name="propertyAccessor">The <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyAccessor" /> is null.-or-The method that <paramref name="propertyAccessor" /> represents is not static (Shared in Visual Basic) and <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="expression" />.Type is not assignable to the declaring type of the method represented by <paramref name="propertyAccessor" />.-or-The method that <paramref name="propertyAccessor" /> represents is not a property accessor method.</exception>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00031F9F File Offset: 0x0003019F
		public static MemberExpression Property(Expression expression, MethodInfo propertyAccessor)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			Expression.ValidateMethodInfo(propertyAccessor, "propertyAccessor");
			return Expression.Property(expression, Expression.GetProperty(propertyAccessor, "propertyAccessor", -1));
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00031FCC File Offset: 0x000301CC
		private static PropertyInfo GetProperty(MethodInfo mi, string paramName, int index = -1)
		{
			Type declaringType = mi.DeclaringType;
			if (declaringType != null)
			{
				BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
				bindingFlags |= (mi.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
				foreach (PropertyInfo propertyInfo in declaringType.GetProperties(bindingFlags))
				{
					if (propertyInfo.CanRead && Expression.CheckMethod(mi, propertyInfo.GetGetMethod(true)))
					{
						return propertyInfo;
					}
					if (propertyInfo.CanWrite && Expression.CheckMethod(mi, propertyInfo.GetSetMethod(true)))
					{
						return propertyInfo;
					}
				}
			}
			throw Error.MethodNotPropertyAccessor(mi.DeclaringType, mi.Name, paramName, index);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00032064 File Offset: 0x00030264
		private static bool CheckMethod(MethodInfo method, MethodInfo propertyMethod)
		{
			if (method.Equals(propertyMethod))
			{
				return true;
			}
			Type declaringType = method.DeclaringType;
			return declaringType.IsInterface && method.Name == propertyMethod.Name && declaringType.GetMethod(method.Name) == propertyMethod;
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property or field.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" />, and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> or <see cref="T:System.Reflection.FieldInfo" /> that represents the property or field denoted by <paramref name="propertyOrFieldName" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> contains a property or field named <paramref name="propertyOrFieldName" />. This can be null for static members.</param>
		/// <param name="propertyOrFieldName">The name of a property or field to be accessed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="propertyOrFieldName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">No property or field named <paramref name="propertyOrFieldName" /> is defined in <paramref name="expression" />.Type or its base types.</exception>
		// Token: 0x06000EBC RID: 3772 RVA: 0x000320B8 File Offset: 0x000302B8
		public static MemberExpression PropertyOrField(Expression expression, string propertyOrFieldName)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			PropertyInfo propertyInfo = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (propertyInfo != null)
			{
				return Expression.Property(expression, propertyInfo);
			}
			FieldInfo fieldInfo = expression.Type.GetField(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (fieldInfo != null)
			{
				return Expression.Field(expression, fieldInfo);
			}
			propertyInfo = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (propertyInfo != null)
			{
				return Expression.Property(expression, propertyInfo);
			}
			fieldInfo = expression.Type.GetField(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (fieldInfo != null)
			{
				return Expression.Field(expression, fieldInfo);
			}
			throw Error.NotAMemberOfType(propertyOrFieldName, expression.Type, "propertyOrFieldName");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing either a field or a property.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.MemberExpression" /> that results from calling the appropriate factory method.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the object that the member belongs to. This can be null for static members.</param>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> that describes the field or property to be accessed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="member" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> does not represent a field or property.</exception>
		// Token: 0x06000EBD RID: 3773 RVA: 0x00032164 File Offset: 0x00030364
		public static MemberExpression MakeMemberAccess(Expression expression, MemberInfo member)
		{
			ContractUtils.RequiresNotNull(member, "member");
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return Expression.Field(expression, fieldInfo);
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return Expression.Property(expression, propertyInfo);
			}
			throw Error.MemberNotFieldOrProperty(member, "member");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberInitExpression" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberInit" /> and the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> properties set to the specified values.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="bindings">An array of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="bindings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type that <paramref name="newExpression" />.Type represents.</exception>
		// Token: 0x06000EBE RID: 3774 RVA: 0x000321B7 File Offset: 0x000303B7
		public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings)
		{
			return Expression.MemberInit(newExpression, bindings);
		}

		/// <summary>Represents an expression that creates a new object and initializes a property of the object.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberInit" /> and the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> properties set to the specified values.</returns>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="bindings">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newExpression" /> or <paramref name="bindings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type that <paramref name="newExpression" />.Type represents.</exception>
		// Token: 0x06000EBF RID: 3775 RVA: 0x000321C0 File Offset: 0x000303C0
		public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			ReadOnlyCollection<MemberBinding> readOnlyCollection = bindings.ToReadOnly<MemberBinding>();
			Expression.ValidateMemberInitArgs(newExpression.Type, readOnlyCollection);
			return new MemberInitExpression(newExpression, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> where the member is a field or property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> properties set to the specified values.</returns>
		/// <param name="member">A <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="member" /> is null. -or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Reflection.FieldInfo.FieldType" /> or <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the field or property that <paramref name="member" /> represents does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000EC0 RID: 3776 RVA: 0x000321FD File Offset: 0x000303FD
		public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers)
		{
			return Expression.ListBind(member, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> where the member is a field or property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> properties set to the specified values.</returns>
		/// <param name="member">A <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="member" /> is null. -or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Reflection.FieldInfo.FieldType" /> or <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the field or property that <paramref name="member" /> represents does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x00032208 File Offset: 0x00030408
		public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			Type type;
			Expression.ValidateGettableFieldOrPropertyMember(member, out type);
			ReadOnlyCollection<ElementInit> readOnlyCollection = initializers.ToReadOnly<ElementInit>();
			Expression.ValidateListInitArgs(type, readOnlyCollection, "member");
			return new MemberListBinding(member, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> object based on a specified property accessor method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.MemberInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> populated with the elements of <paramref name="initializers" />.</returns>
		/// <param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyAccessor" /> is null. -or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the property that the method represented by <paramref name="propertyAccessor" /> accesses does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003224D File Offset: 0x0003044D
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers)
		{
			return Expression.ListBind(propertyAccessor, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> based on a specified property accessor method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.MemberInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> populated with the elements of <paramref name="initializers" />.</returns>
		/// <param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyAccessor" /> is null. -or-One or more elements of <paramref name="initializers" /> are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the property that the method represented by <paramref name="propertyAccessor" /> accesses does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000EC3 RID: 3779 RVA: 0x00032256 File Offset: 0x00030456
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListBind(Expression.GetProperty(propertyAccessor, "propertyAccessor", -1), initializers);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00032280 File Offset: 0x00030480
		private static void ValidateListInitArgs(Type listType, ReadOnlyCollection<ElementInit> initializers, string listTypeParamName)
		{
			if (!typeof(IEnumerable).IsAssignableFrom(listType))
			{
				throw Error.TypeNotIEnumerable(listType, listTypeParamName);
			}
			int i = 0;
			int count = initializers.Count;
			while (i < count)
			{
				ElementInit elementInit = initializers[i];
				ContractUtils.RequiresNotNull(elementInit, "initializers", i);
				Expression.ValidateCallInstanceType(listType, elementInit.AddMethod);
				i++;
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a field or property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="bindings">An array of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="member" /> or <paramref name="bindings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000EC5 RID: 3781 RVA: 0x000322DA File Offset: 0x000304DA
		public static MemberMemberBinding MemberBind(MemberInfo member, params MemberBinding[] bindings)
		{
			return Expression.MemberBind(member, bindings);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a field or property.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="bindings">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="member" /> or <paramref name="bindings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x000322E4 File Offset: 0x000304E4
		public static MemberMemberBinding MemberBind(MemberInfo member, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			ReadOnlyCollection<MemberBinding> readOnlyCollection = bindings.ToReadOnly<MemberBinding>();
			Type type;
			Expression.ValidateGettableFieldOrPropertyMember(member, out type);
			Expression.ValidateMemberInitArgs(type, readOnlyCollection);
			return new MemberMemberBinding(member, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a member that is accessed by using a property accessor method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <param name="propertyAccessor">The <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="bindings">An array of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyAccessor" /> or <paramref name="bindings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the property accessed by the method that <paramref name="propertyAccessor" /> represents.</exception>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00032324 File Offset: 0x00030524
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, params MemberBinding[] bindings)
		{
			return Expression.MemberBind(propertyAccessor, bindings);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a member that is accessed by using a property accessor method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <param name="propertyAccessor">The <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="bindings">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyAccessor" /> or <paramref name="bindings" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the property accessed by the method that <paramref name="propertyAccessor" /> represents.</exception>
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003232D File Offset: 0x0003052D
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			return Expression.MemberBind(Expression.GetProperty(propertyAccessor, "propertyAccessor", -1), bindings);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003234C File Offset: 0x0003054C
		private static void ValidateGettableFieldOrPropertyMember(MemberInfo member, out Type memberType)
		{
			Type declaringType = member.DeclaringType;
			if (declaringType == null)
			{
				throw Error.NotAMemberOfAnyType(member, "member");
			}
			TypeUtils.ValidateType(declaringType, null, true, true);
			if (member != null)
			{
				PropertyInfo propertyInfo;
				if ((propertyInfo = member as PropertyInfo) == null)
				{
					FieldInfo fieldInfo;
					if ((fieldInfo = member as FieldInfo) != null)
					{
						FieldInfo fieldInfo2 = fieldInfo;
						memberType = fieldInfo2.FieldType;
						return;
					}
				}
				else
				{
					PropertyInfo propertyInfo2 = propertyInfo;
					if (!propertyInfo2.CanRead)
					{
						throw Error.PropertyDoesNotHaveGetter(propertyInfo2, "member");
					}
					memberType = propertyInfo2.PropertyType;
					return;
				}
			}
			throw Error.ArgumentMustBeFieldInfoOrPropertyInfo("member");
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000323D0 File Offset: 0x000305D0
		private static void ValidateMemberInitArgs(Type type, ReadOnlyCollection<MemberBinding> bindings)
		{
			int i = 0;
			int count = bindings.Count;
			while (i < count)
			{
				MemberBinding memberBinding = bindings[i];
				ContractUtils.RequiresNotNull(memberBinding, "bindings");
				memberBinding.ValidateAsDefinedHere(i);
				if (!memberBinding.Member.DeclaringType.IsAssignableFrom(type))
				{
					throw Error.NotAMemberOfType(memberBinding.Member.Name, type, "bindings", i);
				}
				i++;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00032438 File Offset: 0x00030638
		internal static MethodCallExpression Call(MethodInfo method)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 0, array);
			return new MethodCallExpression0(method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static (Shared in Visual Basic) method that takes one argument.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		// Token: 0x06000ECC RID: 3788 RVA: 0x00032468 File Offset: 0x00030668
		public static MethodCallExpression Call(MethodInfo method, Expression arg0)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 1, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			return new MethodCallExpression1(method, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes two arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		// Token: 0x06000ECD RID: 3789 RVA: 0x000324BC File Offset: 0x000306BC
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 2, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			return new MethodCallExpression2(method, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes three arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		// Token: 0x06000ECE RID: 3790 RVA: 0x00032534 File Offset: 0x00030734
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 3, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			return new MethodCallExpression3(method, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes four arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <param name="arg3">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the fourth argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		// Token: 0x06000ECF RID: 3791 RVA: 0x000325CC File Offset: 0x000307CC
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ContractUtils.RequiresNotNull(arg3, "arg3");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 4, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			arg3 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg3, array[3], "method", "arg3");
			return new MethodCallExpression4(method, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes five arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <param name="arg3">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the fourth argument.</param>
		/// <param name="arg4">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the fifth argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003268C File Offset: 0x0003088C
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ContractUtils.RequiresNotNull(arg3, "arg3");
			ContractUtils.RequiresNotNull(arg4, "arg4");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 5, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			arg3 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg3, array[3], "method", "arg3");
			arg4 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg4, array[4], "method", "arg4");
			return new MethodCallExpression5(method, arg0, arg1, arg2, arg3, arg4);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static (Shared in Visual Basic) method that has arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents a static (Shared in Visual Basic) method to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="arguments" /> does not equal the number of parameters for the method represented by <paramref name="method" />.-or-One or more of the elements of <paramref name="arguments" /> is not assignable to the corresponding parameter for the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00032770 File Offset: 0x00030970
		public static MethodCallExpression Call(MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static (Shared in Visual Basic) method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the target method.</param>
		/// <param name="arguments">A collection of <see cref="T:System.Linq.Expressions.Expression" /> that represents the call arguments.</param>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003277A File Offset: 0x0003097A
		public static MethodCallExpression Call(MethodInfo method, IEnumerable<Expression> arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes no arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance method call (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.-or-<paramref name="instance" /> is null and <paramref name="method" /> represents an instance method.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="instance" />.Type is not assignable to the declaring type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x00032784 File Offset: 0x00030984
		public static MethodCallExpression Call(Expression instance, MethodInfo method)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 0, array);
			if (instance != null)
			{
				return new InstanceMethodCallExpression0(method, instance);
			}
			return new MethodCallExpression0(method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" />, <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" />, and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance fo an instance method call (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.-or-<paramref name="instance" /> is null and <paramref name="method" /> represents an instance method.-or-<paramref name="arguments" /> is not null and one or more of its elements is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="instance" />.Type is not assignable to the declaring type of the method represented by <paramref name="method" />.-or-The number of elements in <paramref name="arguments" /> does not equal the number of parameters for the method represented by <paramref name="method" />.-or-One or more of the elements of <paramref name="arguments" /> is not assignable to the corresponding parameter for the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x000327BE File Offset: 0x000309BE
		public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(instance, method, arguments);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000327C8 File Offset: 0x000309C8
		internal static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 1, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			if (instance != null)
			{
				return new InstanceMethodCallExpression1(method, instance, arg0);
			}
			return new MethodCallExpression1(method, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes two arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance call. (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the target method.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00032828 File Offset: 0x00030A28
		public static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 2, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			if (instance != null)
			{
				return new InstanceMethodCallExpression2(method, instance, arg0, arg1);
			}
			return new MethodCallExpression2(method, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes three arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance call. (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the target method.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x000328AC File Offset: 0x00030AAC
		public static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 3, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			if (instance != null)
			{
				return new InstanceMethodCallExpression3(method, instance, arg0, arg1, arg2);
			}
			return new MethodCallExpression3(method, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method by calling the appropriate factory method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" />, the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property equal to <paramref name="instance" />, <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> set to the <see cref="T:System.Reflection.MethodInfo" /> that represents the specified instance method, and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> set to the specified arguments.</returns>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> property value will be searched for a specific method.</param>
		/// <param name="methodName">The name of the method.</param>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that specify the type parameters of the generic method. This argument should be null when methodName specifies a non-generic method.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that represents the arguments to the method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> or <paramref name="methodName" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="instance" />.Type or its base types.-or-More than one method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="instance" />.Type or its base types.</exception>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00032958 File Offset: 0x00030B58
		public static MethodCallExpression Call(Expression instance, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			ContractUtils.RequiresNotNull(instance, "instance");
			ContractUtils.RequiresNotNull(methodName, "methodName");
			if (arguments == null)
			{
				arguments = Array.Empty<Expression>();
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return Expression.Call(instance, Expression.FindMethod(instance.Type, methodName, typeArguments, arguments, bindingFlags), arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static (Shared in Visual Basic) method by calling the appropriate factory method.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" />, the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property set to the <see cref="T:System.Reflection.MethodInfo" /> that represents the specified static (Shared in Visual Basic) method, and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> property set to the specified arguments.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that specifies the type that contains the specified static (Shared in Visual Basic) method.</param>
		/// <param name="methodName">The name of the method.</param>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that specify the type parameters of the generic method. This argument should be null when methodName specifies a non-generic method.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments to the method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="methodName" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="type" /> or its base types.-or-More than one method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="type" /> or its base types.</exception>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x000329A0 File Offset: 0x00030BA0
		public static MethodCallExpression Call(Type type, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(methodName, "methodName");
			if (arguments == null)
			{
				arguments = Array.Empty<Expression>();
			}
			BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return Expression.Call(null, Expression.FindMethod(type, methodName, typeArguments, arguments, bindingFlags), arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" />, <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" />, and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property equal to (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="method" /> is null.-or-<paramref name="instance" /> is null and <paramref name="method" /> represents an instance method.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="instance" />.Type is not assignable to the declaring type of the method represented by <paramref name="method" />.-or-The number of elements in <paramref name="arguments" /> does not equal the number of parameters for the method represented by <paramref name="method" />.-or-One or more of the elements of <paramref name="arguments" /> is not assignable to the corresponding parameter for the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000EDA RID: 3802 RVA: 0x000329E4 File Offset: 0x00030BE4
		public static MethodCallExpression Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
		{
			IReadOnlyList<Expression> readOnlyList = (arguments as IReadOnlyList<Expression>) ?? arguments.ToReadOnly<Expression>();
			int count = readOnlyList.Count;
			switch (count)
			{
			case 0:
				return Expression.Call(instance, method);
			case 1:
				return Expression.Call(instance, method, readOnlyList[0]);
			case 2:
				return Expression.Call(instance, method, readOnlyList[0], readOnlyList[1]);
			case 3:
				return Expression.Call(instance, method, readOnlyList[0], readOnlyList[1], readOnlyList[2]);
			default:
			{
				if (instance == null)
				{
					if (count == 4)
					{
						return Expression.Call(method, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3]);
					}
					if (count == 5)
					{
						return Expression.Call(method, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3], readOnlyList[4]);
					}
				}
				ContractUtils.RequiresNotNull(method, "method");
				ReadOnlyCollection<Expression> readOnlyCollection = readOnlyList.ToReadOnly<Expression>();
				Expression.ValidateMethodInfo(method, "method");
				Expression.ValidateStaticOrInstanceMethod(instance, method);
				Expression.ValidateArgumentTypes(method, ExpressionType.Call, ref readOnlyCollection, "method");
				if (instance == null)
				{
					return new MethodCallExpressionN(method, readOnlyCollection);
				}
				return new InstanceMethodCallExpressionN(method, instance, readOnlyCollection);
			}
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00032B0A File Offset: 0x00030D0A
		private static ParameterInfo[] ValidateMethodAndGetParameters(Expression instance, MethodInfo method)
		{
			Expression.ValidateMethodInfo(method, "method");
			Expression.ValidateStaticOrInstanceMethod(instance, method);
			return Expression.GetParametersForValidation(method, ExpressionType.Call);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00032B25 File Offset: 0x00030D25
		private static void ValidateStaticOrInstanceMethod(Expression instance, MethodInfo method)
		{
			if (method.IsStatic)
			{
				if (instance != null)
				{
					throw Error.OnlyStaticMethodsHaveNullInstance();
				}
			}
			else
			{
				if (instance == null)
				{
					throw Error.OnlyStaticMethodsHaveNullInstance();
				}
				ExpressionUtils.RequiresCanRead(instance, "instance");
				Expression.ValidateCallInstanceType(instance.Type, method);
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00032B58 File Offset: 0x00030D58
		private static void ValidateCallInstanceType(Type instanceType, MethodInfo method)
		{
			if (!TypeUtils.IsValidInstanceType(method, instanceType))
			{
				throw Error.InstanceAndMethodTypeMismatch(method, method.DeclaringType, instanceType);
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00032B71 File Offset: 0x00030D71
		private static void ValidateArgumentTypes(MethodBase method, ExpressionType nodeKind, ref ReadOnlyCollection<Expression> arguments, string methodParamName)
		{
			ExpressionUtils.ValidateArgumentTypes(method, nodeKind, ref arguments, methodParamName);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00032B7C File Offset: 0x00030D7C
		private static ParameterInfo[] GetParametersForValidation(MethodBase method, ExpressionType nodeKind)
		{
			return ExpressionUtils.GetParametersForValidation(method, nodeKind);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00032B85 File Offset: 0x00030D85
		private static void ValidateArgumentCount(MethodBase method, ExpressionType nodeKind, int count, ParameterInfo[] pis)
		{
			ExpressionUtils.ValidateArgumentCount(method, nodeKind, count, pis);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00032B90 File Offset: 0x00030D90
		private static Expression ValidateOneArgument(MethodBase method, ExpressionType nodeKind, Expression arg, ParameterInfo pi, string methodParamName, string argumentParamName)
		{
			return ExpressionUtils.ValidateOneArgument(method, nodeKind, arg, pi, methodParamName, argumentParamName, -1);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00032BA0 File Offset: 0x00030DA0
		private static bool TryQuote(Type parameterType, ref Expression argument)
		{
			return ExpressionUtils.TryQuote(parameterType, ref argument);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00032BAC File Offset: 0x00030DAC
		private static MethodInfo FindMethod(Type type, string methodName, Type[] typeArgs, Expression[] args, BindingFlags flags)
		{
			int num = 0;
			MethodInfo methodInfo = null;
			foreach (MethodInfo methodInfo2 in type.GetMethods(flags))
			{
				if (methodInfo2.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase))
				{
					MethodInfo methodInfo3 = Expression.ApplyTypeArgs(methodInfo2, typeArgs);
					if (methodInfo3 != null && Expression.IsCompatible(methodInfo3, args))
					{
						if (methodInfo == null || (!methodInfo.IsPublic && methodInfo3.IsPublic))
						{
							methodInfo = methodInfo3;
							num = 1;
						}
						else if (methodInfo.IsPublic == methodInfo3.IsPublic)
						{
							num++;
						}
					}
				}
			}
			if (num == 0)
			{
				if (typeArgs != null && typeArgs.Length != 0)
				{
					throw Error.GenericMethodWithArgsDoesNotExistOnType(methodName, type);
				}
				throw Error.MethodWithArgsDoesNotExistOnType(methodName, type);
			}
			else
			{
				if (num > 1)
				{
					throw Error.MethodWithMoreThanOneMatch(methodName, type);
				}
				return methodInfo;
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00032C64 File Offset: 0x00030E64
		private static bool IsCompatible(MethodBase m, Expression[] arguments)
		{
			ParameterInfo[] parametersCached = m.GetParametersCached();
			if (parametersCached.Length != arguments.Length)
			{
				return false;
			}
			for (int i = 0; i < arguments.Length; i++)
			{
				Expression expression = arguments[i];
				ContractUtils.RequiresNotNull(expression, "arguments");
				Type type = expression.Type;
				Type type2 = parametersCached[i].ParameterType;
				if (type2.IsByRef)
				{
					type2 = type2.GetElementType();
				}
				if (!TypeUtils.AreReferenceAssignable(type2, type) && (!TypeUtils.IsSameOrSubclass(typeof(LambdaExpression), type2) || !type2.IsAssignableFrom(expression.GetType())))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00032CF1 File Offset: 0x00030EF1
		private static MethodInfo ApplyTypeArgs(MethodInfo m, Type[] typeArgs)
		{
			if (typeArgs == null || typeArgs.Length == 0)
			{
				if (!m.IsGenericMethodDefinition)
				{
					return m;
				}
			}
			else if (m.IsGenericMethodDefinition && m.GetGenericArguments().Length == typeArgs.Length)
			{
				return m.MakeGenericMethod(typeArgs);
			}
			return null;
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents applying an array index operator to a multidimensional array.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="array">An array of <see cref="T:System.Linq.Expressions.Expression" /> instances - indexes for the array index operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> or <paramref name="indexes" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" />.Type does not represent an array type.-or-The rank of <paramref name="array" />.Type does not match the number of elements in <paramref name="indexes" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="indexes" /> does not represent the <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00032D22 File Offset: 0x00030F22
		public static MethodCallExpression ArrayIndex(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayIndex(array, indexes);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents applying an array index operator to an array of rank more than one.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="array">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property equal to.</param>
		/// <param name="indexes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> or <paramref name="indexes" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" />.Type does not represent an array type.-or-The rank of <paramref name="array" />.Type does not match the number of elements in <paramref name="indexes" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="indexes" /> does not represent the <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00032D2C File Offset: 0x00030F2C
		public static MethodCallExpression ArrayIndex(Expression array, IEnumerable<Expression> indexes)
		{
			ExpressionUtils.RequiresCanRead(array, "array", -1);
			ContractUtils.RequiresNotNull(indexes, "indexes");
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray("array");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnly<Expression>();
			if (type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			int i = 0;
			int count = readOnlyCollection.Count;
			while (i < count)
			{
				Expression expression = readOnlyCollection[i];
				ExpressionUtils.RequiresCanRead(expression, "indexes", i);
				if (expression.Type != typeof(int))
				{
					throw Error.ArgumentMustBeArrayIndexType("indexes", i);
				}
				i++;
			}
			MethodInfo method = array.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public);
			return Expression.Call(array, method, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating a one-dimensional array and initializing it from a list of elements.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="initializers" /> is null.-or-An element of <paramref name="initializers" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="initializers" /> represents a type that is not assignable to the type <paramref name="type" />.</exception>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00032DE6 File Offset: 0x00030FE6
		public static NewArrayExpression NewArrayInit(Type type, params Expression[] initializers)
		{
			return Expression.NewArrayInit(type, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating a one-dimensional array and initializing it from a list of elements.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="initializers" /> is null.-or-An element of <paramref name="initializers" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="initializers" /> represents a type that is not assignable to the type that <paramref name="type" /> represents.</exception>
		// Token: 0x06000EE9 RID: 3817 RVA: 0x00032DF0 File Offset: 0x00030FF0
		public static NewArrayExpression NewArrayInit(Type type, IEnumerable<Expression> initializers)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
			TypeUtils.ValidateType(type, "type");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			Expression[] array = null;
			int i = 0;
			int count = readOnlyCollection.Count;
			while (i < count)
			{
				Expression expression = readOnlyCollection[i];
				ExpressionUtils.RequiresCanRead(expression, "initializers", i);
				if (!TypeUtils.AreReferenceAssignable(type, expression.Type))
				{
					if (!Expression.TryQuote(type, ref expression))
					{
						throw Error.ExpressionTypeCannotInitializeArrayType(expression.Type, type);
					}
					if (array == null)
					{
						array = new Expression[readOnlyCollection.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = readOnlyCollection[j];
						}
					}
				}
				if (array != null)
				{
					array[i] = expression;
				}
				i++;
			}
			if (array != null)
			{
				readOnlyCollection = new TrueReadOnlyCollection<Expression>(array);
			}
			return NewArrayExpression.Make(ExpressionType.NewArrayInit, type.MakeArrayType(), readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating an array that has a specified rank.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="bounds">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="bounds" /> is null.-or-An element of <paramref name="bounds" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="bounds" /> does not represent an integral type.</exception>
		// Token: 0x06000EEA RID: 3818 RVA: 0x00032EDF File Offset: 0x000310DF
		public static NewArrayExpression NewArrayBounds(Type type, params Expression[] bounds)
		{
			return Expression.NewArrayBounds(type, bounds);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating an array that has a specified rank.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="bounds">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="bounds" /> is null.-or-An element of <paramref name="bounds" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="bounds" /> does not represent an integral type.</exception>
		// Token: 0x06000EEB RID: 3819 RVA: 0x00032EE8 File Offset: 0x000310E8
		public static NewArrayExpression NewArrayBounds(Type type, IEnumerable<Expression> bounds)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(bounds, "bounds");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
			TypeUtils.ValidateType(type, "type");
			ReadOnlyCollection<Expression> readOnlyCollection = bounds.ToReadOnly<Expression>();
			int count = readOnlyCollection.Count;
			if (count <= 0)
			{
				throw Error.BoundsCannotBeLessThanOne("bounds");
			}
			for (int i = 0; i < count; i++)
			{
				Expression expression = readOnlyCollection[i];
				ExpressionUtils.RequiresCanRead(expression, "bounds", i);
				if (!expression.Type.IsInteger())
				{
					throw Error.ArgumentMustBeInteger("bounds", i);
				}
			}
			Type type2;
			if (count == 1)
			{
				type2 = type.MakeArrayType();
			}
			else
			{
				type2 = type.MakeArrayType(count);
			}
			return NewArrayExpression.Make(ExpressionType.NewArrayBounds, type2, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor that takes no arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property set to the specified value.</returns>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constructor" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The constructor that <paramref name="constructor" /> represents has at least one parameter.</exception>
		// Token: 0x06000EEC RID: 3820 RVA: 0x00032FA5 File Offset: 0x000311A5
		public static NewExpression New(ConstructorInfo constructor)
		{
			return Expression.New(constructor, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> and <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constructor" /> is null.-or-An element of <paramref name="arguments" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="arguments" /> does match the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.</exception>
		// Token: 0x06000EED RID: 3821 RVA: 0x00032FAE File Offset: 0x000311AE
		public static NewExpression New(ConstructorInfo constructor, params Expression[] arguments)
		{
			return Expression.New(constructor, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> and <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> properties set to the specified values.</returns>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constructor" /> is null.-or-An element of <paramref name="arguments" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="arguments" /> parameter does not contain the same number of elements as the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.</exception>
		// Token: 0x06000EEE RID: 3822 RVA: 0x00032FB8 File Offset: 0x000311B8
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(constructor, "constructor");
			ContractUtils.RequiresNotNull(constructor.DeclaringType, "constructor.DeclaringType");
			TypeUtils.ValidateType(constructor.DeclaringType, "constructor", true, true);
			Expression.ValidateConstructor(constructor, "constructor");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			Expression.ValidateArgumentTypes(constructor, ExpressionType.New, ref readOnlyCollection, "constructor");
			return new NewExpression(constructor, readOnlyCollection, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments. The members that access the constructor initialized fields are specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" />, <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> and <see cref="P:System.Linq.Expressions.NewExpression.Members" /> properties set to the specified values.</returns>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <param name="members">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Reflection.MemberInfo" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Members" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constructor" /> is null.-or-An element of <paramref name="arguments" /> is null.-or-An element of <paramref name="members" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="arguments" /> parameter does not contain the same number of elements as the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.-or-The <paramref name="members" /> parameter does not have the same number of elements as <paramref name="arguments" />.-or-An element of <paramref name="arguments" /> has a <see cref="P:System.Linq.Expressions.Expression.Type" /> property that represents a type that is not assignable to the type of the member that is represented by the corresponding element of <paramref name="members" />.</exception>
		// Token: 0x06000EEF RID: 3823 RVA: 0x0003301C File Offset: 0x0003121C
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, IEnumerable<MemberInfo> members)
		{
			ContractUtils.RequiresNotNull(constructor, "constructor");
			ContractUtils.RequiresNotNull(constructor.DeclaringType, "constructor.DeclaringType");
			TypeUtils.ValidateType(constructor.DeclaringType, "constructor", true, true);
			Expression.ValidateConstructor(constructor, "constructor");
			ReadOnlyCollection<MemberInfo> readOnlyCollection = members.ToReadOnly<MemberInfo>();
			ReadOnlyCollection<Expression> readOnlyCollection2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateNewArgs(constructor, ref readOnlyCollection2, ref readOnlyCollection);
			return new NewExpression(constructor, readOnlyCollection2, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments. The members that access the constructor initialized fields are specified as an array.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" />, <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> and <see cref="P:System.Linq.Expressions.NewExpression.Members" /> properties set to the specified values.</returns>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <param name="members">An array of <see cref="T:System.Reflection.MemberInfo" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Members" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="constructor" /> is null.-or-An element of <paramref name="arguments" /> is null.-or-An element of <paramref name="members" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="arguments" /> parameter does not contain the same number of elements as the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.-or-The <paramref name="members" /> parameter does not have the same number of elements as <paramref name="arguments" />.-or-An element of <paramref name="arguments" /> has a <see cref="P:System.Linq.Expressions.Expression.Type" /> property that represents a type that is not assignable to the type of the member that is represented by the corresponding element of <paramref name="members" />.</exception>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x00033081 File Offset: 0x00031281
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
		{
			return Expression.New(constructor, arguments, members);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the parameterless constructor of the specified type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property set to the <see cref="T:System.Reflection.ConstructorInfo" /> that represents the constructor without parameters for the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that has a constructor that takes no arguments.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The type that <paramref name="type" /> represents does not have a constructor without parameters.</exception>
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003308C File Offset: 0x0003128C
		public static NewExpression New(Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
			TypeUtils.ValidateType(type, "type");
			if (type.IsValueType)
			{
				return new NewValueTypeExpression(type, EmptyReadOnlyCollection<Expression>.Instance, null);
			}
			ConstructorInfo constructorInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SingleOrDefault((ConstructorInfo c) => c.GetParametersCached().Length == 0);
			if (constructorInfo == null)
			{
				throw Error.TypeMissingDefaultConstructor(type, "type");
			}
			return Expression.New(constructorInfo);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00033128 File Offset: 0x00031328
		private static void ValidateNewArgs(ConstructorInfo constructor, ref ReadOnlyCollection<Expression> arguments, ref ReadOnlyCollection<MemberInfo> members)
		{
			ParameterInfo[] parametersCached;
			if ((parametersCached = constructor.GetParametersCached()).Length != 0)
			{
				if (arguments.Count != parametersCached.Length)
				{
					throw Error.IncorrectNumberOfConstructorArguments();
				}
				if (arguments.Count != members.Count)
				{
					throw Error.IncorrectNumberOfArgumentsForMembers();
				}
				Expression[] array = null;
				MemberInfo[] array2 = null;
				int i = 0;
				int count = arguments.Count;
				while (i < count)
				{
					Expression expression = arguments[i];
					ExpressionUtils.RequiresCanRead(expression, "arguments", i);
					MemberInfo memberInfo = members[i];
					ContractUtils.RequiresNotNull(memberInfo, "members", i);
					if (!TypeUtils.AreEquivalent(memberInfo.DeclaringType, constructor.DeclaringType))
					{
						throw Error.ArgumentMemberNotDeclOnType(memberInfo.Name, constructor.DeclaringType.Name, "members", i);
					}
					Type type;
					Expression.ValidateAnonymousTypeMember(ref memberInfo, out type, "members", i);
					if (!TypeUtils.AreReferenceAssignable(type, expression.Type) && !Expression.TryQuote(type, ref expression))
					{
						throw Error.ArgumentTypeDoesNotMatchMember(expression.Type, type, "arguments", i);
					}
					Type type2 = parametersCached[i].ParameterType;
					if (type2.IsByRef)
					{
						type2 = type2.GetElementType();
					}
					if (!TypeUtils.AreReferenceAssignable(type2, expression.Type) && !Expression.TryQuote(type2, ref expression))
					{
						throw Error.ExpressionTypeDoesNotMatchConstructorParameter(expression.Type, type2, "arguments", i);
					}
					if (array == null && expression != arguments[i])
					{
						array = new Expression[arguments.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = arguments[j];
						}
					}
					if (array != null)
					{
						array[i] = expression;
					}
					if (array2 == null && memberInfo != members[i])
					{
						array2 = new MemberInfo[members.Count];
						for (int k = 0; k < i; k++)
						{
							array2[k] = members[k];
						}
					}
					if (array2 != null)
					{
						array2[i] = memberInfo;
					}
					i++;
				}
				if (array != null)
				{
					arguments = new TrueReadOnlyCollection<Expression>(array);
				}
				if (array2 != null)
				{
					members = new TrueReadOnlyCollection<MemberInfo>(array2);
					return;
				}
			}
			else
			{
				if (arguments != null && arguments.Count > 0)
				{
					throw Error.IncorrectNumberOfConstructorArguments();
				}
				if (members != null && members.Count > 0)
				{
					throw Error.IncorrectNumberOfMembersForGivenConstructor();
				}
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003333C File Offset: 0x0003153C
		private static void ValidateAnonymousTypeMember(ref MemberInfo member, out Type memberType, string paramName, int index)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				if (fieldInfo.IsStatic)
				{
					throw Error.ArgumentMustBeInstanceMember(paramName, index);
				}
				memberType = fieldInfo.FieldType;
				return;
			}
			else
			{
				PropertyInfo propertyInfo = member as PropertyInfo;
				if (propertyInfo != null)
				{
					if (!propertyInfo.CanRead)
					{
						throw Error.PropertyDoesNotHaveGetter(propertyInfo, paramName, index);
					}
					if (propertyInfo.GetGetMethod().IsStatic)
					{
						throw Error.ArgumentMustBeInstanceMember(paramName, index);
					}
					memberType = propertyInfo.PropertyType;
					return;
				}
				else
				{
					MethodInfo methodInfo = member as MethodInfo;
					if (!(methodInfo != null))
					{
						throw Error.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(paramName, index);
					}
					if (methodInfo.IsStatic)
					{
						throw Error.ArgumentMustBeInstanceMember(paramName, index);
					}
					PropertyInfo property = Expression.GetProperty(methodInfo, paramName, index);
					member = property;
					memberType = property.PropertyType;
					return;
				}
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000333F0 File Offset: 0x000315F0
		private static void ValidateConstructor(ConstructorInfo constructor, string paramName)
		{
			if (constructor.IsStatic)
			{
				throw Error.NonStaticConstructorRequired(paramName);
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> node with the specified name and type.</returns>
		/// <param name="type">The type of the parameter or variable.</param>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00033401 File Offset: 0x00031601
		public static ParameterExpression Parameter(Type type)
		{
			return Expression.Parameter(type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> node with the specified name and type</returns>
		/// <param name="type">The type of the parameter or variable.</param>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003340A File Offset: 0x0003160A
		public static ParameterExpression Variable(Type type)
		{
			return Expression.Variable(type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Parameter" /> and the <see cref="P:System.Linq.Expressions.Expression.Type" /> and <see cref="P:System.Linq.Expressions.ParameterExpression.Name" /> properties set to the specified values.</returns>
		/// <param name="type">The type of the parameter or variable.</param>
		/// <param name="name">The name of the parameter or variable, used for debugging or printing purpose only.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00033414 File Offset: 0x00031614
		public static ParameterExpression Parameter(Type type, string name)
		{
			Expression.Validate(type, true);
			bool isByRef = type.IsByRef;
			if (isByRef)
			{
				type = type.GetElementType();
			}
			return ParameterExpression.Make(type, name, isByRef);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> node with the specified name and type.</returns>
		/// <param name="type">The type of the parameter or variable.</param>
		/// <param name="name">The name of the parameter or variable. This name is used for debugging or printing purpose only.</param>
		// Token: 0x06000EF8 RID: 3832 RVA: 0x00033442 File Offset: 0x00031642
		public static ParameterExpression Variable(Type type, string name)
		{
			Expression.Validate(type, false);
			return ParameterExpression.Make(type, name, false);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00033453 File Offset: 0x00031653
		private static void Validate(Type type, bool allowByRef)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type", allowByRef, false);
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RuntimeVariables" /> and the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> property set to the specified value.</returns>
		/// <param name="variables">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> collection.</param>
		// Token: 0x06000EFA RID: 3834 RVA: 0x0003348A File Offset: 0x0003168A
		public static RuntimeVariablesExpression RuntimeVariables(params ParameterExpression[] variables)
		{
			return Expression.RuntimeVariables(variables);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RuntimeVariables" /> and the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> property set to the specified value.</returns>
		/// <param name="variables">A collection of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> collection.</param>
		// Token: 0x06000EFB RID: 3835 RVA: 0x00033494 File Offset: 0x00031694
		public static RuntimeVariablesExpression RuntimeVariables(IEnumerable<ParameterExpression> variables)
		{
			ContractUtils.RequiresNotNull(variables, "variables");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = variables.ToReadOnly<ParameterExpression>();
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				ContractUtils.RequiresNotNull(readOnlyCollection[i], "variables", i);
			}
			return new RuntimeVariablesExpression(readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchCase" /> for use in a <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchCase" />.</returns>
		/// <param name="body">The body of the case.</param>
		/// <param name="testValues">The test values of the case.</param>
		// Token: 0x06000EFC RID: 3836 RVA: 0x000334DC File Offset: 0x000316DC
		public static SwitchCase SwitchCase(Expression body, params Expression[] testValues)
		{
			return Expression.SwitchCase(body, testValues);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchCase" /> object to be used in a <see cref="T:System.Linq.Expressions.SwitchExpression" /> object.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchCase" />.</returns>
		/// <param name="body">The body of the case.</param>
		/// <param name="testValues">The test values of the case.</param>
		// Token: 0x06000EFD RID: 3837 RVA: 0x000334E8 File Offset: 0x000316E8
		public static SwitchCase SwitchCase(Expression body, IEnumerable<Expression> testValues)
		{
			ExpressionUtils.RequiresCanRead(body, "body");
			ReadOnlyCollection<Expression> readOnlyCollection = testValues.ToReadOnly<Expression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "testValues");
			Expression.RequiresCanRead(readOnlyCollection, "testValues");
			return new SwitchCase(body, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a switch statement without a default case.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		// Token: 0x06000EFE RID: 3838 RVA: 0x00033524 File Offset: 0x00031724
		public static SwitchExpression Switch(Expression switchValue, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, null, null, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a switch statement that has a default case.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		// Token: 0x06000EFF RID: 3839 RVA: 0x0003352F File Offset: 0x0003172F
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, defaultBody, null, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a switch statement that has a default case.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		// Token: 0x06000F00 RID: 3840 RVA: 0x0003353A File Offset: 0x0003173A
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, MethodInfo comparison, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, defaultBody, comparison, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a switch statement that has a default case..</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		/// <param name="type">The result type of the switch.</param>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		// Token: 0x06000F01 RID: 3841 RVA: 0x00033545 File Offset: 0x00031745
		public static SwitchExpression Switch(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, params SwitchCase[] cases)
		{
			return Expression.Switch(type, switchValue, defaultBody, comparison, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a switch statement that has a default case.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		// Token: 0x06000F02 RID: 3842 RVA: 0x00033552 File Offset: 0x00031752
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, MethodInfo comparison, IEnumerable<SwitchCase> cases)
		{
			return Expression.Switch(null, switchValue, defaultBody, comparison, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a switch statement that has a default case.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		/// <param name="type">The result type of the switch.</param>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		// Token: 0x06000F03 RID: 3843 RVA: 0x00033560 File Offset: 0x00031760
		public static SwitchExpression Switch(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, IEnumerable<SwitchCase> cases)
		{
			ExpressionUtils.RequiresCanRead(switchValue, "switchValue");
			if (switchValue.Type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("switchValue");
			}
			ReadOnlyCollection<SwitchCase> readOnlyCollection = cases.ToReadOnly<SwitchCase>();
			ContractUtils.RequiresNotNullItems<SwitchCase>(readOnlyCollection, "cases");
			Type type2;
			if (type != null)
			{
				type2 = type;
			}
			else if (readOnlyCollection.Count != 0)
			{
				type2 = readOnlyCollection[0].Body.Type;
			}
			else if (defaultBody != null)
			{
				type2 = defaultBody.Type;
			}
			else
			{
				type2 = typeof(void);
			}
			bool flag = type != null;
			if (comparison != null)
			{
				Expression.ValidateMethodInfo(comparison, "comparison");
				ParameterInfo[] parametersCached = comparison.GetParametersCached();
				if (parametersCached.Length != 2)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(comparison, "comparison");
				}
				ParameterInfo parameterInfo = parametersCached[0];
				bool flag2 = false;
				if (!Expression.ParameterIsAssignable(parameterInfo, switchValue.Type))
				{
					flag2 = Expression.ParameterIsAssignable(parameterInfo, switchValue.Type.GetNonNullableType());
					if (!flag2)
					{
						throw Error.SwitchValueTypeDoesNotMatchComparisonMethodParameter(switchValue.Type, parameterInfo.ParameterType);
					}
				}
				ParameterInfo parameterInfo2 = parametersCached[1];
				foreach (SwitchCase switchCase in readOnlyCollection)
				{
					ContractUtils.RequiresNotNull(switchCase, "cases");
					Expression.ValidateSwitchCaseType(switchCase.Body, flag, type2, "cases");
					int i = 0;
					int count = switchCase.TestValues.Count;
					while (i < count)
					{
						Type type3 = switchCase.TestValues[i].Type;
						if (flag2)
						{
							if (!type3.IsNullableType())
							{
								throw Error.TestValueTypeDoesNotMatchComparisonMethodParameter(type3, parameterInfo2.ParameterType);
							}
							type3 = type3.GetNonNullableType();
						}
						if (!Expression.ParameterIsAssignable(parameterInfo2, type3))
						{
							throw Error.TestValueTypeDoesNotMatchComparisonMethodParameter(type3, parameterInfo2.ParameterType);
						}
						i++;
					}
				}
				if (comparison.ReturnType != typeof(bool))
				{
					throw Error.EqualityMustReturnBoolean(comparison, "comparison");
				}
			}
			else if (readOnlyCollection.Count != 0)
			{
				Expression expression = readOnlyCollection[0].TestValues[0];
				foreach (SwitchCase switchCase2 in readOnlyCollection)
				{
					ContractUtils.RequiresNotNull(switchCase2, "cases");
					Expression.ValidateSwitchCaseType(switchCase2.Body, flag, type2, "cases");
					int j = 0;
					int count2 = switchCase2.TestValues.Count;
					while (j < count2)
					{
						if (!TypeUtils.AreEquivalent(expression.Type, switchCase2.TestValues[j].Type))
						{
							throw Error.AllTestValuesMustHaveSameType("cases");
						}
						j++;
					}
				}
				comparison = Expression.Equal(switchValue, expression, false, comparison).Method;
			}
			if (defaultBody == null)
			{
				if (type2 != typeof(void))
				{
					throw Error.DefaultBodyMustBeSupplied("defaultBody");
				}
			}
			else
			{
				Expression.ValidateSwitchCaseType(defaultBody, flag, type2, "defaultBody");
			}
			return new SwitchExpression(type2, switchValue, defaultBody, comparison, readOnlyCollection);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00033868 File Offset: 0x00031A68
		private static void ValidateSwitchCaseType(Expression @case, bool customType, Type resultType, string parameterName)
		{
			if (customType)
			{
				if (resultType != typeof(void) && !TypeUtils.AreReferenceAssignable(resultType, @case.Type))
				{
					throw Error.ArgumentTypesMustMatch(parameterName);
				}
			}
			else if (!TypeUtils.AreEquivalent(resultType, @case.Type))
			{
				throw Error.AllCaseBodiesMustHaveSameType(parameterName);
			}
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> property set to the specified value.</returns>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		// Token: 0x06000F05 RID: 3845 RVA: 0x000338B4 File Offset: 0x00031AB4
		public static SymbolDocumentInfo SymbolDocument(string fileName)
		{
			return new SymbolDocumentInfo(fileName);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> properties set to the specified value.</returns>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <param name="language">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> equal to.</param>
		// Token: 0x06000F06 RID: 3846 RVA: 0x000338BC File Offset: 0x00031ABC
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language)
		{
			return new SymbolDocumentWithGuids(fileName, ref language);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> properties set to the specified value.</returns>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <param name="language">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> equal to.</param>
		/// <param name="languageVendor">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> equal to.</param>
		// Token: 0x06000F07 RID: 3847 RVA: 0x000338C6 File Offset: 0x00031AC6
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor)
		{
			return new SymbolDocumentWithGuids(fileName, ref language, ref languageVendor);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.DocumentType" /> properties set to the specified value.</returns>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <param name="language">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> equal to.</param>
		/// <param name="languageVendor">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> equal to.</param>
		/// <param name="documentType">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.DocumentType" /> equal to.</param>
		// Token: 0x06000F08 RID: 3848 RVA: 0x000338D2 File Offset: 0x00031AD2
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor, Guid documentType)
		{
			return new SymbolDocumentWithGuids(fileName, ref language, ref languageVendor, ref documentType);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with a fault block and no catch statements.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		/// <param name="body">The body of the try block.</param>
		/// <param name="fault">The body of the fault block.</param>
		// Token: 0x06000F09 RID: 3849 RVA: 0x000338E0 File Offset: 0x00031AE0
		public static TryExpression TryFault(Expression body, Expression fault)
		{
			return Expression.MakeTry(null, body, null, fault, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with a finally block and no catch statements.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		/// <param name="body">The body of the try block.</param>
		/// <param name="finally">The body of the finally block.</param>
		// Token: 0x06000F0A RID: 3850 RVA: 0x000338EC File Offset: 0x00031AEC
		public static TryExpression TryFinally(Expression body, Expression @finally)
		{
			return Expression.MakeTry(null, body, @finally, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with any number of catch statements and neither a fault nor finally block.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		/// <param name="body">The body of the try block.</param>
		/// <param name="handlers">The array of zero or more <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions representing the catch statements to be associated with the try block.</param>
		// Token: 0x06000F0B RID: 3851 RVA: 0x000338F8 File Offset: 0x00031AF8
		public static TryExpression TryCatch(Expression body, params CatchBlock[] handlers)
		{
			return Expression.MakeTry(null, body, null, null, handlers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with any number of catch statements and a finally block.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		/// <param name="body">The body of the try block.</param>
		/// <param name="finally">The body of the finally block.</param>
		/// <param name="handlers">The array of zero or more <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions representing the catch statements to be associated with the try block.</param>
		// Token: 0x06000F0C RID: 3852 RVA: 0x00033904 File Offset: 0x00031B04
		public static TryExpression TryCatchFinally(Expression body, Expression @finally, params CatchBlock[] handlers)
		{
			return Expression.MakeTry(null, body, @finally, null, handlers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with the specified elements.</summary>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		/// <param name="type">The result type of the try expression. If null, bodh and all handlers must have identical type.</param>
		/// <param name="body">The body of the try block.</param>
		/// <param name="finally">The body of the finally block. Pass null if the try block has no finally block associated with it.</param>
		/// <param name="fault">The body of the fault block. Pass null if the try block has no fault block associated with it.</param>
		/// <param name="handlers">A collection of <see cref="T:System.Linq.Expressions.CatchBlock" />s representing the catch statements to be associated with the try block.</param>
		// Token: 0x06000F0D RID: 3853 RVA: 0x00033910 File Offset: 0x00031B10
		public static TryExpression MakeTry(Type type, Expression body, Expression @finally, Expression fault, IEnumerable<CatchBlock> handlers)
		{
			ExpressionUtils.RequiresCanRead(body, "body");
			ReadOnlyCollection<CatchBlock> readOnlyCollection = handlers.ToReadOnly<CatchBlock>();
			ContractUtils.RequiresNotNullItems<CatchBlock>(readOnlyCollection, "handlers");
			Expression.ValidateTryAndCatchHaveSameType(type, body, readOnlyCollection);
			if (fault != null)
			{
				if (@finally != null || readOnlyCollection.Count > 0)
				{
					throw Error.FaultCannotHaveCatchOrFinally("fault");
				}
				ExpressionUtils.RequiresCanRead(fault, "fault");
			}
			else if (@finally != null)
			{
				ExpressionUtils.RequiresCanRead(@finally, "finally");
			}
			else if (readOnlyCollection.Count == 0)
			{
				throw Error.TryMustHaveCatchFinallyOrFault();
			}
			return new TryExpression(type ?? body.Type, body, @finally, fault, readOnlyCollection);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003399C File Offset: 0x00031B9C
		private static void ValidateTryAndCatchHaveSameType(Type type, Expression tryBody, ReadOnlyCollection<CatchBlock> handlers)
		{
			if (type != null)
			{
				if (!(type != typeof(void)))
				{
					return;
				}
				if (!TypeUtils.AreReferenceAssignable(type, tryBody.Type))
				{
					throw Error.ArgumentTypesMustMatch();
				}
				using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CatchBlock catchBlock = enumerator.Current;
						if (!TypeUtils.AreReferenceAssignable(type, catchBlock.Body.Type))
						{
							throw Error.ArgumentTypesMustMatch();
						}
					}
					return;
				}
			}
			if (tryBody.Type == typeof(void))
			{
				using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Body.Type != typeof(void))
						{
							throw Error.BodyOfCatchMustHaveSameTypeAsBodyOfTry();
						}
					}
					return;
				}
			}
			type = tryBody.Type;
			using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!TypeUtils.AreEquivalent(enumerator.Current.Body.Type, type))
					{
						throw Error.BodyOfCatchMustHaveSameTypeAsBodyOfTry();
					}
				}
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TypeBinaryExpression" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.TypeBinaryExpression" /> for which the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is equal to <see cref="F:System.Linq.Expressions.ExpressionType.TypeIs" /> and for which the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> and <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> properties are set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> property equal to.</param>
		/// <param name="type">A <see cref="P:System.Linq.Expressions.Expression.Type" /> to set the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="type" /> is null.</exception>
		// Token: 0x06000F0F RID: 3855 RVA: 0x00033AE4 File Offset: 0x00031CE4
		public static TypeBinaryExpression TypeIs(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef("type");
			}
			return new TypeBinaryExpression(expression, type, ExpressionType.TypeIs);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TypeBinaryExpression" /> that compares run-time type identity.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.TypeBinaryExpression" /> for which the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is equal to <see cref="M:System.Linq.Expressions.Expression.TypeEqual(System.Linq.Expressions.Expression,System.Type)" /> and for which the <see cref="T:System.Linq.Expressions.Expression" /> and <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> properties are set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="T:System.Linq.Expressions.Expression" /> property equal to.</param>
		/// <param name="type">A <see cref="P:System.Linq.Expressions.Expression.Type" /> to set the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> property equal to.</param>
		// Token: 0x06000F10 RID: 3856 RVA: 0x00033B18 File Offset: 0x00031D18
		public static TypeBinaryExpression TypeEqual(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef("type");
			}
			return new TypeBinaryExpression(expression, type, ExpressionType.TypeEqual);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" />, given an operand, by calling the appropriate factory method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.UnaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <param name="unaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of unary operation.</param>
		/// <param name="operand">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand.</param>
		/// <param name="type">The <see cref="T:System.Type" /> that specifies the type to be converted to (pass null if not applicable).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="operand" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unaryType" /> does not correspond to a unary expression node.</exception>
		// Token: 0x06000F11 RID: 3857 RVA: 0x00033B4C File Offset: 0x00031D4C
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type)
		{
			return Expression.MakeUnary(unaryType, operand, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" />, given an operand and implementing method, by calling the appropriate factory method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.UnaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <param name="unaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of unary operation.</param>
		/// <param name="operand">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand.</param>
		/// <param name="type">The <see cref="T:System.Type" /> that specifies the type to be converted to (pass null if not applicable).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="operand" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unaryType" /> does not correspond to a unary expression node.</exception>
		// Token: 0x06000F12 RID: 3858 RVA: 0x00033B58 File Offset: 0x00031D58
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type, MethodInfo method)
		{
			if (unaryType <= ExpressionType.Quote)
			{
				if (unaryType <= ExpressionType.Convert)
				{
					if (unaryType == ExpressionType.ArrayLength)
					{
						return Expression.ArrayLength(operand);
					}
					if (unaryType == ExpressionType.Convert)
					{
						return Expression.Convert(operand, type, method);
					}
				}
				else
				{
					if (unaryType == ExpressionType.ConvertChecked)
					{
						return Expression.ConvertChecked(operand, type, method);
					}
					switch (unaryType)
					{
					case ExpressionType.Negate:
						return Expression.Negate(operand, method);
					case ExpressionType.UnaryPlus:
						return Expression.UnaryPlus(operand, method);
					case ExpressionType.NegateChecked:
						return Expression.NegateChecked(operand, method);
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						break;
					case ExpressionType.Not:
						return Expression.Not(operand, method);
					default:
						if (unaryType == ExpressionType.Quote)
						{
							return Expression.Quote(operand);
						}
						break;
					}
				}
			}
			else if (unaryType <= ExpressionType.Increment)
			{
				if (unaryType == ExpressionType.TypeAs)
				{
					return Expression.TypeAs(operand, type);
				}
				if (unaryType == ExpressionType.Decrement)
				{
					return Expression.Decrement(operand, method);
				}
				if (unaryType == ExpressionType.Increment)
				{
					return Expression.Increment(operand, method);
				}
			}
			else
			{
				if (unaryType == ExpressionType.Throw)
				{
					return Expression.Throw(operand, type);
				}
				if (unaryType == ExpressionType.Unbox)
				{
					return Expression.Unbox(operand, type);
				}
				switch (unaryType)
				{
				case ExpressionType.PreIncrementAssign:
					return Expression.PreIncrementAssign(operand, method);
				case ExpressionType.PreDecrementAssign:
					return Expression.PreDecrementAssign(operand, method);
				case ExpressionType.PostIncrementAssign:
					return Expression.PostIncrementAssign(operand, method);
				case ExpressionType.PostDecrementAssign:
					return Expression.PostDecrementAssign(operand, method);
				case ExpressionType.OnesComplement:
					return Expression.OnesComplement(operand, method);
				case ExpressionType.IsTrue:
					return Expression.IsTrue(operand, method);
				case ExpressionType.IsFalse:
					return Expression.IsFalse(operand, method);
				}
			}
			throw Error.UnhandledUnary(unaryType, "unaryType");
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00033CCC File Offset: 0x00031ECC
		private static UnaryExpression GetUserDefinedUnaryOperatorOrThrow(ExpressionType unaryType, string name, Expression operand)
		{
			UnaryExpression userDefinedUnaryOperator = Expression.GetUserDefinedUnaryOperator(unaryType, name, operand);
			if (userDefinedUnaryOperator != null)
			{
				Expression.ValidateParamswithOperandsOrThrow(userDefinedUnaryOperator.Method.GetParametersCached()[0].ParameterType, operand.Type, unaryType, name);
				return userDefinedUnaryOperator;
			}
			throw Error.UnaryOperatorNotDefined(unaryType, operand.Type);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00033D18 File Offset: 0x00031F18
		private static UnaryExpression GetUserDefinedUnaryOperator(ExpressionType unaryType, string name, Expression operand)
		{
			Type type = operand.Type;
			Type[] array = new Type[] { type };
			Type nonNullableType = type.GetNonNullableType();
			MethodInfo methodInfo = nonNullableType.GetAnyStaticMethodValidated(name, array);
			if (methodInfo != null)
			{
				return new UnaryExpression(unaryType, operand, methodInfo.ReturnType, methodInfo);
			}
			if (type.IsNullableType())
			{
				array[0] = nonNullableType;
				methodInfo = nonNullableType.GetAnyStaticMethodValidated(name, array);
				if (methodInfo != null && methodInfo.ReturnType.IsValueType && !methodInfo.ReturnType.IsNullableType())
				{
					return new UnaryExpression(unaryType, operand, methodInfo.ReturnType.GetNullableType(), methodInfo);
				}
			}
			return null;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00033DAC File Offset: 0x00031FAC
		private static UnaryExpression GetMethodBasedUnaryOperator(ExpressionType unaryType, Expression operand, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], operand.Type))
			{
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, operand.Type, unaryType, method.Name);
				return new UnaryExpression(unaryType, operand, method.ReturnType, method);
			}
			if (operand.Type.IsNullableType() && Expression.ParameterIsAssignable(parametersCached[0], operand.Type.GetNonNullableType()) && method.ReturnType.IsValueType && !method.ReturnType.IsNullableType())
			{
				return new UnaryExpression(unaryType, operand, method.ReturnType.GetNullableType(), method);
			}
			throw Error.OperandTypesDoNotMatchParameters(unaryType, method.Name);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00033E74 File Offset: 0x00032074
		private static UnaryExpression GetUserDefinedCoercionOrThrow(ExpressionType coercionType, Expression expression, Type convertToType)
		{
			UnaryExpression userDefinedCoercion = Expression.GetUserDefinedCoercion(coercionType, expression, convertToType);
			if (userDefinedCoercion != null)
			{
				return userDefinedCoercion;
			}
			throw Error.CoercionOperatorNotDefined(expression.Type, convertToType);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00033E9C File Offset: 0x0003209C
		private static UnaryExpression GetUserDefinedCoercion(ExpressionType coercionType, Expression expression, Type convertToType)
		{
			MethodInfo userDefinedCoercionMethod = TypeUtils.GetUserDefinedCoercionMethod(expression.Type, convertToType);
			if (userDefinedCoercionMethod != null)
			{
				return new UnaryExpression(coercionType, expression, convertToType, userDefinedCoercionMethod);
			}
			return null;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00033ECC File Offset: 0x000320CC
		private static UnaryExpression GetMethodBasedCoercionOperator(ExpressionType unaryType, Expression operand, Type convertToType, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], operand.Type) && TypeUtils.AreEquivalent(method.ReturnType, convertToType))
			{
				return new UnaryExpression(unaryType, operand, method.ReturnType, method);
			}
			if ((operand.Type.IsNullableType() || convertToType.IsNullableType()) && Expression.ParameterIsAssignable(parametersCached[0], operand.Type.GetNonNullableType()) && (TypeUtils.AreEquivalent(method.ReturnType, convertToType.GetNonNullableType()) || TypeUtils.AreEquivalent(method.ReturnType, convertToType)))
			{
				return new UnaryExpression(unaryType, operand, convertToType, method);
			}
			throw Error.OperandTypesDoNotMatchParameters(unaryType, method.Name);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Negate" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary minus operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000F19 RID: 3865 RVA: 0x00033F8B File Offset: 0x0003218B
		public static UnaryExpression Negate(Expression expression)
		{
			return Expression.Negate(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Negate" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the unary minus operator is not defined for <paramref name="expression" />.Type.-or-<paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000F1A RID: 3866 RVA: 0x00033F94 File Offset: 0x00032194
		public static UnaryExpression Negate(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Negate, expression, method);
			}
			if (expression.Type.IsArithmetic() && !expression.Type.IsUnsignedInt())
			{
				return new UnaryExpression(ExpressionType.Negate, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Negate, "op_UnaryNegation", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a unary plus operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.UnaryPlus" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary plus operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000F1B RID: 3867 RVA: 0x00033FF6 File Offset: 0x000321F6
		public static UnaryExpression UnaryPlus(Expression expression)
		{
			return Expression.UnaryPlus(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a unary plus operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.UnaryPlus" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the unary plus operator is not defined for <paramref name="expression" />.Type.-or-<paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000F1C RID: 3868 RVA: 0x00034000 File Offset: 0x00032200
		public static UnaryExpression UnaryPlus(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.UnaryPlus, expression, method);
			}
			if (expression.Type.IsArithmetic())
			{
				return new UnaryExpression(ExpressionType.UnaryPlus, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.UnaryPlus, "op_UnaryPlus", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation that has overflow checking.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NegateChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary minus operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000F1D RID: 3869 RVA: 0x00034055 File Offset: 0x00032255
		public static UnaryExpression NegateChecked(Expression expression)
		{
			return Expression.NegateChecked(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation that has overflow checking. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NegateChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the unary minus operator is not defined for <paramref name="expression" />.Type.-or-<paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000F1E RID: 3870 RVA: 0x00034060 File Offset: 0x00032260
		public static UnaryExpression NegateChecked(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.NegateChecked, expression, method);
			}
			if (expression.Type.IsArithmetic() && !expression.Type.IsUnsignedInt())
			{
				return new UnaryExpression(ExpressionType.NegateChecked, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.NegateChecked, "op_UnaryNegation", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a bitwise complement operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Not" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary not operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000F1F RID: 3871 RVA: 0x000340C2 File Offset: 0x000322C2
		public static UnaryExpression Not(Expression expression)
		{
			return Expression.Not(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a bitwise complement operation. The implementing method can be specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Not" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="method" /> is null and the unary not operator is not defined for <paramref name="expression" />.Type.-or-<paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000F20 RID: 3872 RVA: 0x000340CC File Offset: 0x000322CC
		public static UnaryExpression Not(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Not, expression, method);
			}
			if (expression.Type.IsIntegerOrBool())
			{
				return new UnaryExpression(ExpressionType.Not, expression, expression.Type, null);
			}
			UnaryExpression userDefinedUnaryOperator = Expression.GetUserDefinedUnaryOperator(ExpressionType.Not, "op_LogicalNot", expression);
			if (userDefinedUnaryOperator != null)
			{
				return userDefinedUnaryOperator;
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Not, "op_OnesComplement", expression);
		}

		/// <summary>Returns whether the expression evaluates to false.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		// Token: 0x06000F21 RID: 3873 RVA: 0x00034134 File Offset: 0x00032334
		public static UnaryExpression IsFalse(Expression expression)
		{
			return Expression.IsFalse(expression, null);
		}

		/// <summary>Returns whether the expression evaluates to false.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F22 RID: 3874 RVA: 0x00034140 File Offset: 0x00032340
		public static UnaryExpression IsFalse(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.IsFalse, expression, method);
			}
			if (expression.Type.IsBool())
			{
				return new UnaryExpression(ExpressionType.IsFalse, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.IsFalse, "op_False", expression);
		}

		/// <summary>Returns whether the expression evaluates to true.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		// Token: 0x06000F23 RID: 3875 RVA: 0x00034195 File Offset: 0x00032395
		public static UnaryExpression IsTrue(Expression expression)
		{
			return Expression.IsTrue(expression, null);
		}

		/// <summary>Returns whether the expression evaluates to true.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F24 RID: 3876 RVA: 0x000341A0 File Offset: 0x000323A0
		public static UnaryExpression IsTrue(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.IsTrue, expression, method);
			}
			if (expression.Type.IsBool())
			{
				return new UnaryExpression(ExpressionType.IsTrue, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.IsTrue, "op_True", expression);
		}

		/// <summary>Returns the expression representing the ones complement.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		// Token: 0x06000F25 RID: 3877 RVA: 0x000341F5 File Offset: 0x000323F5
		public static UnaryExpression OnesComplement(Expression expression)
		{
			return Expression.OnesComplement(expression, null);
		}

		/// <summary>Returns the expression representing the ones complement.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F26 RID: 3878 RVA: 0x00034200 File Offset: 0x00032400
		public static UnaryExpression OnesComplement(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.OnesComplement, expression, method);
			}
			if (expression.Type.IsInteger())
			{
				return new UnaryExpression(ExpressionType.OnesComplement, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.OnesComplement, "op_OnesComplement", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an explicit reference or boxing conversion where null is supplied if the conversion fails.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.TypeAs" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="type" /> is null.</exception>
		// Token: 0x06000F27 RID: 3879 RVA: 0x00034258 File Offset: 0x00032458
		public static UnaryExpression TypeAs(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (type.IsValueType && !type.IsNullableType())
			{
				throw Error.IncorrectTypeForTypeAs(type, "type");
			}
			return new UnaryExpression(ExpressionType.TypeAs, expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an explicit unboxing.</summary>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to unbox.</param>
		/// <param name="type">The new <see cref="T:System.Type" /> of the expression.</param>
		// Token: 0x06000F28 RID: 3880 RVA: 0x000342AC File Offset: 0x000324AC
		public static UnaryExpression Unbox(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (!expression.Type.IsInterface && expression.Type != typeof(object))
			{
				throw Error.InvalidUnboxType("expression");
			}
			if (!type.IsValueType)
			{
				throw Error.InvalidUnboxType("type");
			}
			TypeUtils.ValidateType(type, "type");
			return new UnaryExpression(ExpressionType.Unbox, expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a type conversion operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Convert" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.</exception>
		// Token: 0x06000F29 RID: 3881 RVA: 0x00034326 File Offset: 0x00032526
		public static UnaryExpression Convert(Expression expression, Type type)
		{
			return Expression.Convert(expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a conversion operation for which the implementing method is specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Convert" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" />, <see cref="P:System.Linq.Expressions.Expression.Type" />, and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.-or-<paramref name="expression" />.Type is not assignable to the argument type of the method represented by <paramref name="method" />.-or-The return type of the method represented by <paramref name="method" /> is not assignable to <paramref name="type" />.-or-<paramref name="expression" />.Type or <paramref name="type" /> is a nullable value type and the corresponding non-nullable value type does not equal the argument type or the return type, respectively, of the method represented by <paramref name="method" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method that matches the <paramref name="method" /> description was found.</exception>
		// Token: 0x06000F2A RID: 3882 RVA: 0x00034330 File Offset: 0x00032530
		public static UnaryExpression Convert(Expression expression, Type type, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (!(method == null))
			{
				return Expression.GetMethodBasedCoercionOperator(ExpressionType.Convert, expression, type, method);
			}
			if (expression.Type.HasIdentityPrimitiveOrNullableConversionTo(type) || expression.Type.HasReferenceConversionTo(type))
			{
				return new UnaryExpression(ExpressionType.Convert, expression, type, null);
			}
			return Expression.GetUserDefinedCoercionOrThrow(ExpressionType.Convert, expression, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a conversion operation that throws an exception if the target type is overflowed.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ConvertChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.</exception>
		// Token: 0x06000F2B RID: 3883 RVA: 0x000343A2 File Offset: 0x000325A2
		public static UnaryExpression ConvertChecked(Expression expression, Type type)
		{
			return Expression.ConvertChecked(expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a conversion operation that throws an exception if the target type is overflowed and for which the implementing method is specified.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ConvertChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" />, <see cref="P:System.Linq.Expressions.Expression.Type" />, and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> or <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is not null and the method it represents returns void, is not static (Shared in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.-or-<paramref name="expression" />.Type is not assignable to the argument type of the method represented by <paramref name="method" />.-or-The return type of the method represented by <paramref name="method" /> is not assignable to <paramref name="type" />.-or-<paramref name="expression" />.Type or <paramref name="type" /> is a nullable value type and the corresponding non-nullable value type does not equal the argument type or the return type, respectively, of the method represented by <paramref name="method" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method that matches the <paramref name="method" /> description was found.</exception>
		// Token: 0x06000F2C RID: 3884 RVA: 0x000343AC File Offset: 0x000325AC
		public static UnaryExpression ConvertChecked(Expression expression, Type type, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (!(method == null))
			{
				return Expression.GetMethodBasedCoercionOperator(ExpressionType.ConvertChecked, expression, type, method);
			}
			if (expression.Type.HasIdentityPrimitiveOrNullableConversionTo(type))
			{
				return new UnaryExpression(ExpressionType.ConvertChecked, expression, type, null);
			}
			if (expression.Type.HasReferenceConversionTo(type))
			{
				return new UnaryExpression(ExpressionType.Convert, expression, type, null);
			}
			return Expression.GetUserDefinedCoercionOrThrow(ExpressionType.ConvertChecked, expression, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an expression for obtaining the length of a one-dimensional array.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ArrayLength" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to <paramref name="array" />.</returns>
		/// <param name="array">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" />.Type does not represent an array type.</exception>
		// Token: 0x06000F2D RID: 3885 RVA: 0x0003442C File Offset: 0x0003262C
		public static UnaryExpression ArrayLength(Expression array)
		{
			ExpressionUtils.RequiresCanRead(array, "array");
			if (array.Type.IsSZArray)
			{
				return new UnaryExpression(ExpressionType.ArrayLength, array, typeof(int), null);
			}
			if (!array.Type.IsArray || !typeof(Array).IsAssignableFrom(array.Type))
			{
				throw Error.ArgumentMustBeArray("array");
			}
			throw Error.ArgumentMustBeSingleDimensionalArrayType("array");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an expression that has a constant value of type <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Quote" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null.</exception>
		// Token: 0x06000F2E RID: 3886 RVA: 0x000344A0 File Offset: 0x000326A0
		public static UnaryExpression Quote(Expression expression)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			LambdaExpression lambdaExpression = expression as LambdaExpression;
			if (lambdaExpression == null)
			{
				throw Error.QuotedExpressionMustBeLambda("expression");
			}
			return new UnaryExpression(ExpressionType.Quote, lambdaExpression, lambdaExpression.PublicType, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception.</returns>
		// Token: 0x06000F2F RID: 3887 RVA: 0x000344DC File Offset: 0x000326DC
		public static UnaryExpression Rethrow()
		{
			return Expression.Throw(null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception with a given type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception.</returns>
		/// <param name="type">The new <see cref="T:System.Type" /> of the expression.</param>
		// Token: 0x06000F30 RID: 3888 RVA: 0x000344E4 File Offset: 0x000326E4
		public static UnaryExpression Rethrow(Type type)
		{
			return Expression.Throw(null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a throwing of an exception.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the exception.</returns>
		/// <param name="value">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		// Token: 0x06000F31 RID: 3889 RVA: 0x000344ED File Offset: 0x000326ED
		public static UnaryExpression Throw(Expression value)
		{
			return Expression.Throw(value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a throwing of an exception with a given type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the exception.</returns>
		/// <param name="value">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		/// <param name="type">The new <see cref="T:System.Type" /> of the expression.</param>
		// Token: 0x06000F32 RID: 3890 RVA: 0x00034500 File Offset: 0x00032700
		public static UnaryExpression Throw(Expression value, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (value != null)
			{
				ExpressionUtils.RequiresCanRead(value, "value");
				if (value.Type.IsValueType)
				{
					throw Error.ArgumentMustNotHaveValueType("value");
				}
			}
			return new UnaryExpression(ExpressionType.Throw, value, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incrementing of the expression value by 1.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incremented expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to increment.</param>
		// Token: 0x06000F33 RID: 3891 RVA: 0x00034553 File Offset: 0x00032753
		public static UnaryExpression Increment(Expression expression)
		{
			return Expression.Increment(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incrementing of the expression by 1.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incremented expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to increment.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F34 RID: 3892 RVA: 0x0003455C File Offset: 0x0003275C
		public static UnaryExpression Increment(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Increment, expression, method);
			}
			if (expression.Type.IsArithmetic())
			{
				return new UnaryExpression(ExpressionType.Increment, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Increment, "op_Increment", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decrementing of the expression by 1.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decremented expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to decrement.</param>
		// Token: 0x06000F35 RID: 3893 RVA: 0x000345B1 File Offset: 0x000327B1
		public static UnaryExpression Decrement(Expression expression)
		{
			return Expression.Decrement(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decrementing of the expression by 1.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decremented expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to decrement.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F36 RID: 3894 RVA: 0x000345BC File Offset: 0x000327BC
		public static UnaryExpression Decrement(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Decrement, expression, method);
			}
			if (expression.Type.IsArithmetic())
			{
				return new UnaryExpression(ExpressionType.Decrement, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Decrement, "op_Decrement", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that increments the expression by 1 and assigns the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		// Token: 0x06000F37 RID: 3895 RVA: 0x00034611 File Offset: 0x00032811
		public static UnaryExpression PreIncrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreIncrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that increments the expression by 1 and assigns the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F38 RID: 3896 RVA: 0x0003461C File Offset: 0x0003281C
		public static UnaryExpression PreIncrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreIncrementAssign, expression, method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that decrements the expression by 1 and assigns the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		// Token: 0x06000F39 RID: 3897 RVA: 0x00034627 File Offset: 0x00032827
		public static UnaryExpression PreDecrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreDecrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that decrements the expression by 1 and assigns the result back to the expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F3A RID: 3898 RVA: 0x00034632 File Offset: 0x00032832
		public static UnaryExpression PreDecrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreDecrementAssign, expression, method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent increment by 1 of the original expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003463D File Offset: 0x0003283D
		public static UnaryExpression PostIncrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostIncrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent increment by 1 of the original expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F3C RID: 3900 RVA: 0x00034648 File Offset: 0x00032848
		public static UnaryExpression PostIncrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostIncrementAssign, expression, method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent decrement by 1 of the original expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		// Token: 0x06000F3D RID: 3901 RVA: 0x00034653 File Offset: 0x00032853
		public static UnaryExpression PostDecrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostDecrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent decrement by 1 of the original expression.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		// Token: 0x06000F3E RID: 3902 RVA: 0x0003465E File Offset: 0x0003285E
		public static UnaryExpression PostDecrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostDecrementAssign, expression, method);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003466C File Offset: 0x0003286C
		private static UnaryExpression MakeOpAssignUnary(ExpressionType kind, Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			Expression.RequiresCanWrite(expression, "expression");
			UnaryExpression unaryExpression;
			if (method == null)
			{
				if (expression.Type.IsArithmetic())
				{
					return new UnaryExpression(kind, expression, expression.Type, null);
				}
				string text;
				if (kind == ExpressionType.PreIncrementAssign || kind == ExpressionType.PostIncrementAssign)
				{
					text = "op_Increment";
				}
				else
				{
					text = "op_Decrement";
				}
				unaryExpression = Expression.GetUserDefinedUnaryOperatorOrThrow(kind, text, expression);
			}
			else
			{
				unaryExpression = Expression.GetMethodBasedUnaryOperator(kind, expression, method);
			}
			if (!TypeUtils.AreReferenceAssignable(expression.Type, unaryExpression.Type))
			{
				throw Error.UserDefinedOpMustHaveValidReturnType(kind, method.Name);
			}
			return unaryExpression;
		}

		// Token: 0x04000889 RID: 2185
		private static readonly CacheDict<Type, MethodInfo> s_lambdaDelegateCache = new CacheDict<Type, MethodInfo>(40);

		// Token: 0x0400088A RID: 2186
		private static volatile CacheDict<Type, Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>> s_lambdaFactories;

		// Token: 0x0400088B RID: 2187
		private static ConditionalWeakTable<Expression, Expression.ExtensionInfo> s_legacyCtorSupportTable;

		// Token: 0x02000228 RID: 552
		internal class BinaryExpressionProxy
		{
			// Token: 0x06000F41 RID: 3905 RVA: 0x00034714 File Offset: 0x00032914
			public BinaryExpressionProxy(BinaryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0003472E File Offset: 0x0003292E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0003473B File Offset: 0x0003293B
			public LambdaExpression Conversion
			{
				get
				{
					return this._node.Conversion;
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00034748 File Offset: 0x00032948
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00034755 File Offset: 0x00032955
			public bool IsLifted
			{
				get
				{
					return this._node.IsLifted;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00034762 File Offset: 0x00032962
			public bool IsLiftedToNull
			{
				get
				{
					return this._node.IsLiftedToNull;
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0003476F File Offset: 0x0003296F
			public Expression Left
			{
				get
				{
					return this._node.Left;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003477C File Offset: 0x0003297C
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00034789 File Offset: 0x00032989
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00034796 File Offset: 0x00032996
			public Expression Right
			{
				get
				{
					return this._node.Right;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000347A3 File Offset: 0x000329A3
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400088C RID: 2188
			private readonly BinaryExpression _node;
		}

		// Token: 0x02000229 RID: 553
		internal class BlockExpressionProxy
		{
			// Token: 0x06000F4C RID: 3916 RVA: 0x000347B0 File Offset: 0x000329B0
			public BlockExpressionProxy(BlockExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000347CA File Offset: 0x000329CA
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06000F4E RID: 3918 RVA: 0x000347D7 File Offset: 0x000329D7
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000347E4 File Offset: 0x000329E4
			public ReadOnlyCollection<Expression> Expressions
			{
				get
				{
					return this._node.Expressions;
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06000F50 RID: 3920 RVA: 0x000347F1 File Offset: 0x000329F1
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000347FE File Offset: 0x000329FE
			public Expression Result
			{
				get
				{
					return this._node.Result;
				}
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003480B File Offset: 0x00032A0B
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00034818 File Offset: 0x00032A18
			public ReadOnlyCollection<ParameterExpression> Variables
			{
				get
				{
					return this._node.Variables;
				}
			}

			// Token: 0x0400088D RID: 2189
			private readonly BlockExpression _node;
		}

		// Token: 0x0200022A RID: 554
		internal class CatchBlockProxy
		{
			// Token: 0x06000F54 RID: 3924 RVA: 0x00034825 File Offset: 0x00032A25
			public CatchBlockProxy(CatchBlock node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003483F File Offset: 0x00032A3F
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003484C File Offset: 0x00032A4C
			public Expression Filter
			{
				get
				{
					return this._node.Filter;
				}
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00034859 File Offset: 0x00032A59
			public Type Test
			{
				get
				{
					return this._node.Test;
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00034866 File Offset: 0x00032A66
			public ParameterExpression Variable
			{
				get
				{
					return this._node.Variable;
				}
			}

			// Token: 0x0400088E RID: 2190
			private readonly CatchBlock _node;
		}

		// Token: 0x0200022B RID: 555
		internal class ConditionalExpressionProxy
		{
			// Token: 0x06000F59 RID: 3929 RVA: 0x00034873 File Offset: 0x00032A73
			public ConditionalExpressionProxy(ConditionalExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0003488D File Offset: 0x00032A8D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003489A File Offset: 0x00032A9A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000348A7 File Offset: 0x00032AA7
			public Expression IfFalse
			{
				get
				{
					return this._node.IfFalse;
				}
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06000F5D RID: 3933 RVA: 0x000348B4 File Offset: 0x00032AB4
			public Expression IfTrue
			{
				get
				{
					return this._node.IfTrue;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06000F5E RID: 3934 RVA: 0x000348C1 File Offset: 0x00032AC1
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000348CE File Offset: 0x00032ACE
			public Expression Test
			{
				get
				{
					return this._node.Test;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06000F60 RID: 3936 RVA: 0x000348DB File Offset: 0x00032ADB
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400088F RID: 2191
			private readonly ConditionalExpression _node;
		}

		// Token: 0x0200022C RID: 556
		internal class ConstantExpressionProxy
		{
			// Token: 0x06000F61 RID: 3937 RVA: 0x000348E8 File Offset: 0x00032AE8
			public ConstantExpressionProxy(ConstantExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00034902 File Offset: 0x00032B02
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0003490F File Offset: 0x00032B0F
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003491C File Offset: 0x00032B1C
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00034929 File Offset: 0x00032B29
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00034936 File Offset: 0x00032B36
			public object Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x04000890 RID: 2192
			private readonly ConstantExpression _node;
		}

		// Token: 0x0200022D RID: 557
		internal class DebugInfoExpressionProxy
		{
			// Token: 0x06000F67 RID: 3943 RVA: 0x00034943 File Offset: 0x00032B43
			public DebugInfoExpressionProxy(DebugInfoExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003495D File Offset: 0x00032B5D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0003496A File Offset: 0x00032B6A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00034977 File Offset: 0x00032B77
			public SymbolDocumentInfo Document
			{
				get
				{
					return this._node.Document;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00034984 File Offset: 0x00032B84
			public int EndColumn
			{
				get
				{
					return this._node.EndColumn;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00034991 File Offset: 0x00032B91
			public int EndLine
			{
				get
				{
					return this._node.EndLine;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003499E File Offset: 0x00032B9E
			public bool IsClear
			{
				get
				{
					return this._node.IsClear;
				}
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000349AB File Offset: 0x00032BAB
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000349B8 File Offset: 0x00032BB8
			public int StartColumn
			{
				get
				{
					return this._node.StartColumn;
				}
			}

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000349C5 File Offset: 0x00032BC5
			public int StartLine
			{
				get
				{
					return this._node.StartLine;
				}
			}

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000349D2 File Offset: 0x00032BD2
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000891 RID: 2193
			private readonly DebugInfoExpression _node;
		}

		// Token: 0x0200022E RID: 558
		internal class DefaultExpressionProxy
		{
			// Token: 0x06000F72 RID: 3954 RVA: 0x000349DF File Offset: 0x00032BDF
			public DefaultExpressionProxy(DefaultExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000231 RID: 561
			// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000349F9 File Offset: 0x00032BF9
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000232 RID: 562
			// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00034A06 File Offset: 0x00032C06
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00034A13 File Offset: 0x00032C13
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00034A20 File Offset: 0x00032C20
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000892 RID: 2194
			private readonly DefaultExpression _node;
		}

		// Token: 0x0200022F RID: 559
		internal class GotoExpressionProxy
		{
			// Token: 0x06000F77 RID: 3959 RVA: 0x00034A2D File Offset: 0x00032C2D
			public GotoExpressionProxy(GotoExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00034A47 File Offset: 0x00032C47
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00034A54 File Offset: 0x00032C54
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00034A61 File Offset: 0x00032C61
			public GotoExpressionKind Kind
			{
				get
				{
					return this._node.Kind;
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00034A6E File Offset: 0x00032C6E
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00034A7B File Offset: 0x00032C7B
			public LabelTarget Target
			{
				get
				{
					return this._node.Target;
				}
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00034A88 File Offset: 0x00032C88
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00034A95 File Offset: 0x00032C95
			public Expression Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x04000893 RID: 2195
			private readonly GotoExpression _node;
		}

		// Token: 0x02000230 RID: 560
		internal class IndexExpressionProxy
		{
			// Token: 0x06000F7F RID: 3967 RVA: 0x00034AA2 File Offset: 0x00032CA2
			public IndexExpressionProxy(IndexExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00034ABC File Offset: 0x00032CBC
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00034AC9 File Offset: 0x00032CC9
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06000F82 RID: 3970 RVA: 0x00034AD6 File Offset: 0x00032CD6
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00034AE3 File Offset: 0x00032CE3
			public PropertyInfo Indexer
			{
				get
				{
					return this._node.Indexer;
				}
			}

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00034AF0 File Offset: 0x00032CF0
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00034AFD File Offset: 0x00032CFD
			public Expression Object
			{
				get
				{
					return this._node.Object;
				}
			}

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00034B0A File Offset: 0x00032D0A
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000894 RID: 2196
			private readonly IndexExpression _node;
		}

		// Token: 0x02000231 RID: 561
		internal class InvocationExpressionProxy
		{
			// Token: 0x06000F87 RID: 3975 RVA: 0x00034B17 File Offset: 0x00032D17
			public InvocationExpressionProxy(InvocationExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00034B31 File Offset: 0x00032D31
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00034B3E File Offset: 0x00032D3E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00034B4B File Offset: 0x00032D4B
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00034B58 File Offset: 0x00032D58
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00034B65 File Offset: 0x00032D65
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00034B72 File Offset: 0x00032D72
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000895 RID: 2197
			private readonly InvocationExpression _node;
		}

		// Token: 0x02000232 RID: 562
		internal class LabelExpressionProxy
		{
			// Token: 0x06000F8E RID: 3982 RVA: 0x00034B7F File Offset: 0x00032D7F
			public LabelExpressionProxy(LabelExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00034B99 File Offset: 0x00032D99
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00034BA6 File Offset: 0x00032DA6
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00034BB3 File Offset: 0x00032DB3
			public Expression DefaultValue
			{
				get
				{
					return this._node.DefaultValue;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00034BC0 File Offset: 0x00032DC0
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00034BCD File Offset: 0x00032DCD
			public LabelTarget Target
			{
				get
				{
					return this._node.Target;
				}
			}

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00034BDA File Offset: 0x00032DDA
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000896 RID: 2198
			private readonly LabelExpression _node;
		}

		// Token: 0x02000233 RID: 563
		internal class LambdaExpressionProxy
		{
			// Token: 0x06000F95 RID: 3989 RVA: 0x00034BE7 File Offset: 0x00032DE7
			public LambdaExpressionProxy(LambdaExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00034C01 File Offset: 0x00032E01
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00034C0E File Offset: 0x00032E0E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00034C1B File Offset: 0x00032E1B
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000252 RID: 594
			// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00034C28 File Offset: 0x00032E28
			public string Name
			{
				get
				{
					return this._node.Name;
				}
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00034C35 File Offset: 0x00032E35
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00034C42 File Offset: 0x00032E42
			public ReadOnlyCollection<ParameterExpression> Parameters
			{
				get
				{
					return this._node.Parameters;
				}
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00034C4F File Offset: 0x00032E4F
			public Type ReturnType
			{
				get
				{
					return this._node.ReturnType;
				}
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00034C5C File Offset: 0x00032E5C
			public bool TailCall
			{
				get
				{
					return this._node.TailCall;
				}
			}

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00034C69 File Offset: 0x00032E69
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000897 RID: 2199
			private readonly LambdaExpression _node;
		}

		// Token: 0x02000234 RID: 564
		internal class ListInitExpressionProxy
		{
			// Token: 0x06000F9F RID: 3999 RVA: 0x00034C76 File Offset: 0x00032E76
			public ListInitExpressionProxy(ListInitExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00034C90 File Offset: 0x00032E90
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00034C9D File Offset: 0x00032E9D
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00034CAA File Offset: 0x00032EAA
			public ReadOnlyCollection<ElementInit> Initializers
			{
				get
				{
					return this._node.Initializers;
				}
			}

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00034CB7 File Offset: 0x00032EB7
			public NewExpression NewExpression
			{
				get
				{
					return this._node.NewExpression;
				}
			}

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00034CC4 File Offset: 0x00032EC4
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00034CD1 File Offset: 0x00032ED1
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000898 RID: 2200
			private readonly ListInitExpression _node;
		}

		// Token: 0x02000235 RID: 565
		internal class LoopExpressionProxy
		{
			// Token: 0x06000FA6 RID: 4006 RVA: 0x00034CDE File Offset: 0x00032EDE
			public LoopExpressionProxy(LoopExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00034CF8 File Offset: 0x00032EF8
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00034D05 File Offset: 0x00032F05
			public LabelTarget BreakLabel
			{
				get
				{
					return this._node.BreakLabel;
				}
			}

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00034D12 File Offset: 0x00032F12
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00034D1F File Offset: 0x00032F1F
			public LabelTarget ContinueLabel
			{
				get
				{
					return this._node.ContinueLabel;
				}
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00034D2C File Offset: 0x00032F2C
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00034D39 File Offset: 0x00032F39
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00034D46 File Offset: 0x00032F46
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000899 RID: 2201
			private readonly LoopExpression _node;
		}

		// Token: 0x02000236 RID: 566
		internal class MemberExpressionProxy
		{
			// Token: 0x06000FAE RID: 4014 RVA: 0x00034D53 File Offset: 0x00032F53
			public MemberExpressionProxy(MemberExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00034D6D File Offset: 0x00032F6D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00034D7A File Offset: 0x00032F7A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00034D87 File Offset: 0x00032F87
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00034D94 File Offset: 0x00032F94
			public MemberInfo Member
			{
				get
				{
					return this._node.Member;
				}
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00034DA1 File Offset: 0x00032FA1
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00034DAE File Offset: 0x00032FAE
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400089A RID: 2202
			private readonly MemberExpression _node;
		}

		// Token: 0x02000237 RID: 567
		internal class MemberInitExpressionProxy
		{
			// Token: 0x06000FB5 RID: 4021 RVA: 0x00034DBB File Offset: 0x00032FBB
			public MemberInitExpressionProxy(MemberInitExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00034DD5 File Offset: 0x00032FD5
			public ReadOnlyCollection<MemberBinding> Bindings
			{
				get
				{
					return this._node.Bindings;
				}
			}

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00034DE2 File Offset: 0x00032FE2
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00034DEF File Offset: 0x00032FEF
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00034DFC File Offset: 0x00032FFC
			public NewExpression NewExpression
			{
				get
				{
					return this._node.NewExpression;
				}
			}

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00034E09 File Offset: 0x00033009
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00034E16 File Offset: 0x00033016
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400089B RID: 2203
			private readonly MemberInitExpression _node;
		}

		// Token: 0x02000238 RID: 568
		internal class MethodCallExpressionProxy
		{
			// Token: 0x06000FBC RID: 4028 RVA: 0x00034E23 File Offset: 0x00033023
			public MethodCallExpressionProxy(MethodCallExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00034E3D File Offset: 0x0003303D
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00034E4A File Offset: 0x0003304A
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00034E57 File Offset: 0x00033057
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00034E64 File Offset: 0x00033064
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00034E71 File Offset: 0x00033071
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00034E7E File Offset: 0x0003307E
			public Expression Object
			{
				get
				{
					return this._node.Object;
				}
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00034E8B File Offset: 0x0003308B
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400089C RID: 2204
			private readonly MethodCallExpression _node;
		}

		// Token: 0x02000239 RID: 569
		internal class NewArrayExpressionProxy
		{
			// Token: 0x06000FC4 RID: 4036 RVA: 0x00034E98 File Offset: 0x00033098
			public NewArrayExpressionProxy(NewArrayExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00034EB2 File Offset: 0x000330B2
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00034EBF File Offset: 0x000330BF
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00034ECC File Offset: 0x000330CC
			public ReadOnlyCollection<Expression> Expressions
			{
				get
				{
					return this._node.Expressions;
				}
			}

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00034ED9 File Offset: 0x000330D9
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00034EE6 File Offset: 0x000330E6
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400089D RID: 2205
			private readonly NewArrayExpression _node;
		}

		// Token: 0x0200023A RID: 570
		internal class NewExpressionProxy
		{
			// Token: 0x06000FCA RID: 4042 RVA: 0x00034EF3 File Offset: 0x000330F3
			public NewExpressionProxy(NewExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00034F0D File Offset: 0x0003310D
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00034F1A File Offset: 0x0003311A
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00034F27 File Offset: 0x00033127
			public ConstructorInfo Constructor
			{
				get
				{
					return this._node.Constructor;
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00034F34 File Offset: 0x00033134
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00034F41 File Offset: 0x00033141
			public ReadOnlyCollection<MemberInfo> Members
			{
				get
				{
					return this._node.Members;
				}
			}

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00034F4E File Offset: 0x0003314E
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00034F5B File Offset: 0x0003315B
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400089E RID: 2206
			private readonly NewExpression _node;
		}

		// Token: 0x0200023B RID: 571
		internal class ParameterExpressionProxy
		{
			// Token: 0x06000FD2 RID: 4050 RVA: 0x00034F68 File Offset: 0x00033168
			public ParameterExpressionProxy(ParameterExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00034F82 File Offset: 0x00033182
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00034F8F File Offset: 0x0003318F
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00034F9C File Offset: 0x0003319C
			public bool IsByRef
			{
				get
				{
					return this._node.IsByRef;
				}
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00034FA9 File Offset: 0x000331A9
			public string Name
			{
				get
				{
					return this._node.Name;
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00034FB6 File Offset: 0x000331B6
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00034FC3 File Offset: 0x000331C3
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400089F RID: 2207
			private readonly ParameterExpression _node;
		}

		// Token: 0x0200023C RID: 572
		internal class RuntimeVariablesExpressionProxy
		{
			// Token: 0x06000FD9 RID: 4057 RVA: 0x00034FD0 File Offset: 0x000331D0
			public RuntimeVariablesExpressionProxy(RuntimeVariablesExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00034FEA File Offset: 0x000331EA
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00034FF7 File Offset: 0x000331F7
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00035004 File Offset: 0x00033204
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00035011 File Offset: 0x00033211
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003501E File Offset: 0x0003321E
			public ReadOnlyCollection<ParameterExpression> Variables
			{
				get
				{
					return this._node.Variables;
				}
			}

			// Token: 0x040008A0 RID: 2208
			private readonly RuntimeVariablesExpression _node;
		}

		// Token: 0x0200023D RID: 573
		internal class SwitchCaseProxy
		{
			// Token: 0x06000FDF RID: 4063 RVA: 0x0003502B File Offset: 0x0003322B
			public SwitchCaseProxy(SwitchCase node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00035045 File Offset: 0x00033245
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00035052 File Offset: 0x00033252
			public ReadOnlyCollection<Expression> TestValues
			{
				get
				{
					return this._node.TestValues;
				}
			}

			// Token: 0x040008A1 RID: 2209
			private readonly SwitchCase _node;
		}

		// Token: 0x0200023E RID: 574
		internal class SwitchExpressionProxy
		{
			// Token: 0x06000FE2 RID: 4066 RVA: 0x0003505F File Offset: 0x0003325F
			public SwitchExpressionProxy(SwitchExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00035079 File Offset: 0x00033279
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00035086 File Offset: 0x00033286
			public ReadOnlyCollection<SwitchCase> Cases
			{
				get
				{
					return this._node.Cases;
				}
			}

			// Token: 0x17000293 RID: 659
			// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00035093 File Offset: 0x00033293
			public MethodInfo Comparison
			{
				get
				{
					return this._node.Comparison;
				}
			}

			// Token: 0x17000294 RID: 660
			// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000350A0 File Offset: 0x000332A0
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x000350AD File Offset: 0x000332AD
			public Expression DefaultBody
			{
				get
				{
					return this._node.DefaultBody;
				}
			}

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x000350BA File Offset: 0x000332BA
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x000350C7 File Offset: 0x000332C7
			public Expression SwitchValue
			{
				get
				{
					return this._node.SwitchValue;
				}
			}

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06000FEA RID: 4074 RVA: 0x000350D4 File Offset: 0x000332D4
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040008A2 RID: 2210
			private readonly SwitchExpression _node;
		}

		// Token: 0x0200023F RID: 575
		internal class TryExpressionProxy
		{
			// Token: 0x06000FEB RID: 4075 RVA: 0x000350E1 File Offset: 0x000332E1
			public TryExpressionProxy(TryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06000FEC RID: 4076 RVA: 0x000350FB File Offset: 0x000332FB
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06000FED RID: 4077 RVA: 0x00035108 File Offset: 0x00033308
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00035115 File Offset: 0x00033315
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00035122 File Offset: 0x00033322
			public Expression Fault
			{
				get
				{
					return this._node.Fault;
				}
			}

			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0003512F File Offset: 0x0003332F
			public Expression Finally
			{
				get
				{
					return this._node.Finally;
				}
			}

			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0003513C File Offset: 0x0003333C
			public ReadOnlyCollection<CatchBlock> Handlers
			{
				get
				{
					return this._node.Handlers;
				}
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00035149 File Offset: 0x00033349
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00035156 File Offset: 0x00033356
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040008A3 RID: 2211
			private readonly TryExpression _node;
		}

		// Token: 0x02000240 RID: 576
		internal class TypeBinaryExpressionProxy
		{
			// Token: 0x06000FF4 RID: 4084 RVA: 0x00035163 File Offset: 0x00033363
			public TypeBinaryExpressionProxy(TypeBinaryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0003517D File Offset: 0x0003337D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0003518A File Offset: 0x0003338A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00035197 File Offset: 0x00033397
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x000351A4 File Offset: 0x000333A4
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x000351B1 File Offset: 0x000333B1
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000FFA RID: 4090 RVA: 0x000351BE File Offset: 0x000333BE
			public Type TypeOperand
			{
				get
				{
					return this._node.TypeOperand;
				}
			}

			// Token: 0x040008A4 RID: 2212
			private readonly TypeBinaryExpression _node;
		}

		// Token: 0x02000241 RID: 577
		internal class UnaryExpressionProxy
		{
			// Token: 0x06000FFB RID: 4091 RVA: 0x000351CB File Offset: 0x000333CB
			public UnaryExpressionProxy(UnaryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000351E5 File Offset: 0x000333E5
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000351F2 File Offset: 0x000333F2
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000FFE RID: 4094 RVA: 0x000351FF File Offset: 0x000333FF
			public bool IsLifted
			{
				get
				{
					return this._node.IsLifted;
				}
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0003520C File Offset: 0x0003340C
			public bool IsLiftedToNull
			{
				get
				{
					return this._node.IsLiftedToNull;
				}
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06001000 RID: 4096 RVA: 0x00035219 File Offset: 0x00033419
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x170002AC RID: 684
			// (get) Token: 0x06001001 RID: 4097 RVA: 0x00035226 File Offset: 0x00033426
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170002AD RID: 685
			// (get) Token: 0x06001002 RID: 4098 RVA: 0x00035233 File Offset: 0x00033433
			public Expression Operand
			{
				get
				{
					return this._node.Operand;
				}
			}

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06001003 RID: 4099 RVA: 0x00035240 File Offset: 0x00033440
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x040008A5 RID: 2213
			private readonly UnaryExpression _node;
		}

		// Token: 0x02000242 RID: 578
		private class ExtensionInfo
		{
			// Token: 0x06001004 RID: 4100 RVA: 0x0003524D File Offset: 0x0003344D
			public ExtensionInfo(ExpressionType nodeType, Type type)
			{
				this.NodeType = nodeType;
				this.Type = type;
			}

			// Token: 0x040008A6 RID: 2214
			internal readonly ExpressionType NodeType;

			// Token: 0x040008A7 RID: 2215
			internal readonly Type Type;
		}

		// Token: 0x02000243 RID: 579
		private enum TryGetFuncActionArgsResult
		{
			// Token: 0x040008A9 RID: 2217
			Valid,
			// Token: 0x040008AA RID: 2218
			ArgumentNull,
			// Token: 0x040008AB RID: 2219
			ByRef,
			// Token: 0x040008AC RID: 2220
			PointerOrVoid
		}
	}
}
