using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000276 RID: 630
	internal interface IParameterProvider
	{
		// Token: 0x0600127D RID: 4733
		ParameterExpression GetParameter(int index);

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600127E RID: 4734
		int ParameterCount { get; }
	}
}
