using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200029A RID: 666
	internal sealed class MethodCallExpression1 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x0003C07E File Offset: 0x0003A27E
		public MethodCallExpression1(MethodInfo method, Expression arg0)
			: base(method)
		{
			this._arg0 = arg0;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003C08E File Offset: 0x0003A28E
		public override Expression GetArgument(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0000AA13 File Offset: 0x00008C13
		public override int ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003C0A9 File Offset: 0x0003A2A9
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0003C0B8 File Offset: 0x0003A2B8
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

		// Token: 0x0600137B RID: 4987 RVA: 0x0003C114 File Offset: 0x0003A314
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0]);
			}
			return Expression.Call(base.Method, ExpressionUtils.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x0400099C RID: 2460
		private object _arg0;
	}
}
