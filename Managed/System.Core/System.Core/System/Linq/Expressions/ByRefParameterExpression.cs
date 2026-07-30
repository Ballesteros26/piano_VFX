using System;

namespace System.Linq.Expressions
{
	// Token: 0x020002A9 RID: 681
	internal sealed class ByRefParameterExpression : TypedParameterExpression
	{
		// Token: 0x060013CD RID: 5069 RVA: 0x0003CD3B File Offset: 0x0003AF3B
		internal ByRefParameterExpression(Type type, string name)
			: base(type, name)
		{
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0000AA13 File Offset: 0x00008C13
		internal override bool GetIsByRef()
		{
			return true;
		}
	}
}
