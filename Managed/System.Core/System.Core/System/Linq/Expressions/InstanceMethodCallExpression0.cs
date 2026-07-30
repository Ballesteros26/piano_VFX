using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200029F RID: 671
	internal sealed class InstanceMethodCallExpression0 : InstanceMethodCallExpression, IArgumentProvider
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x0003C715 File Offset: 0x0003A915
		public InstanceMethodCallExpression0(MethodInfo method, Expression instance)
			: base(method, instance)
		{
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0003B0ED File Offset: 0x000392ED
		public override Expression GetArgument(int index)
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00002285 File Offset: 0x00000485
		public override int ArgumentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003B0E6 File Offset: 0x000392E6
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return EmptyReadOnlyCollection<Expression>.Instance;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003C061 File Offset: 0x0003A261
		internal override bool SameArguments(ICollection<Expression> arguments)
		{
			return arguments == null || arguments.Count == 0;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003C71F File Offset: 0x0003A91F
		internal override MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			return Expression.Call(instance, base.Method);
		}
	}
}
