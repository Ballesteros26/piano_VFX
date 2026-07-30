using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000298 RID: 664
	internal sealed class InstanceMethodCallExpressionN : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600136A RID: 4970 RVA: 0x0003BFF8 File Offset: 0x0003A1F8
		public InstanceMethodCallExpressionN(MethodInfo method, Expression instance, IReadOnlyList<Expression> args)
			: base(method, instance)
		{
			this._arguments = args;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0003C009 File Offset: 0x0003A209
		public override Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0003C017 File Offset: 0x0003A217
		public override int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0003C024 File Offset: 0x0003A224
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return ExpressionUtils.SameElements<Expression>(arguments, this._arguments);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0003C032 File Offset: 0x0003A232
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0003C03F File Offset: 0x0003A23F
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(instance, base.Method, args ?? this._arguments);
		}

		// Token: 0x0400099B RID: 2459
		private IReadOnlyList<Expression> _arguments;
	}
}
