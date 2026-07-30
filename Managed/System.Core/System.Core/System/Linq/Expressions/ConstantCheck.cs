using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000254 RID: 596
	internal static class ConstantCheck
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x00035CD0 File Offset: 0x00033ED0
		internal static bool IsNull(Expression e)
		{
			ExpressionType nodeType = e.NodeType;
			if (nodeType != ExpressionType.Constant)
			{
				return nodeType == ExpressionType.Default && e.Type.IsNullableOrReferenceType();
			}
			return ((ConstantExpression)e).Value == null;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00035D0C File Offset: 0x00033F0C
		internal static AnalyzeTypeIsResult AnalyzeTypeIs(TypeBinaryExpression typeIs)
		{
			return ConstantCheck.AnalyzeTypeIs(typeIs.Expression, typeIs.TypeOperand);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00035D20 File Offset: 0x00033F20
		private static AnalyzeTypeIsResult AnalyzeTypeIs(Expression operand, Type testType)
		{
			Type type = operand.Type;
			if (type == typeof(void))
			{
				if (!(testType == typeof(void)))
				{
					return AnalyzeTypeIsResult.KnownFalse;
				}
				return AnalyzeTypeIsResult.KnownTrue;
			}
			else
			{
				if (testType == typeof(void) || testType.IsPointer)
				{
					return AnalyzeTypeIsResult.KnownFalse;
				}
				Type nonNullableType = type.GetNonNullableType();
				if (!testType.GetNonNullableType().IsAssignableFrom(nonNullableType))
				{
					return AnalyzeTypeIsResult.Unknown;
				}
				if (type.IsValueType && !type.IsNullableType())
				{
					return AnalyzeTypeIsResult.KnownTrue;
				}
				return AnalyzeTypeIsResult.KnownAssignable;
			}
		}
	}
}
