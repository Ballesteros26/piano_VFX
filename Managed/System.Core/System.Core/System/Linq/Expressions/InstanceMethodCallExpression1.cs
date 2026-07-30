using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020002A0 RID: 672
	internal sealed class InstanceMethodCallExpression1 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x0003C72D File Offset: 0x0003A92D
		public InstanceMethodCallExpression1(MethodInfo method, Expression instance, Expression arg0)
			: base(method, instance)
		{
			this._arg0 = arg0;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0003C73E File Offset: 0x0003A93E
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003C75C File Offset: 0x0003A95C
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			if (arguments != null && arguments.Count == 1)
			{
				using (IEnumerator<Expression> enumerator = arguments.GetEnumerator())
				{
					enumerator.MoveNext();
					return enumerator.Current == ExpressionUtils.ReturnObject<Expression>(this._arg0);
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0003C7C6 File Offset: 0x0003A9C6
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0]);
			}
			return Expression.Call(instance, base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x040009AB RID: 2475
		private object _arg0;
	}
}
