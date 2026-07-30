using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000275 RID: 629
	public interface IDynamicExpression : IArgumentProvider
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600127A RID: 4730
		Type DelegateType { get; }

		// Token: 0x0600127B RID: 4731
		Expression Rewrite(Expression[] args);

		// Token: 0x0600127C RID: 4732
		object CreateCallSite();
	}
}
