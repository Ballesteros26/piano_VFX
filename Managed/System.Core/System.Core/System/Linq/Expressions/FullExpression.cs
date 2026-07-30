using System;
using System.Collections.Generic;

namespace System.Linq.Expressions
{
	// Token: 0x02000289 RID: 649
	internal sealed class FullExpression<TDelegate> : ExpressionN<TDelegate>
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x0003BA28 File Offset: 0x00039C28
		public FullExpression(Expression body, string name, bool tailCall, IReadOnlyList<ParameterExpression> parameters)
			: base(body, parameters)
		{
			this.NameCore = name;
			this.TailCallCore = tailCall;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0003BA41 File Offset: 0x00039C41
		internal override string NameCore { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0003BA49 File Offset: 0x00039C49
		internal override bool TailCallCore { get; }
	}
}
