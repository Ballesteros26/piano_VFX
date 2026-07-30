using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000274 RID: 628
	public interface IArgumentProvider
	{
		// Token: 0x06001278 RID: 4728
		Expression GetArgument(int index);

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001279 RID: 4729
		int ArgumentCount { get; }
	}
}
