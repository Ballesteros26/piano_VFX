using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000299 RID: 665
	internal sealed class MethodCallExpression0 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x0003C058 File Offset: 0x0003A258
		public MethodCallExpression0(MethodInfo method)
			: base(method)
		{
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0003B0ED File Offset: 0x000392ED
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00002285 File Offset: 0x00000485
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0003B0E6 File Offset: 0x000392E6
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return EmptyReadOnlyCollection<Expression>.Instance;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0003C061 File Offset: 0x0003A261
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return arguments == null || arguments.Count == 0;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0003C071 File Offset: 0x0003A271
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(base.Method);
		}
	}
}
