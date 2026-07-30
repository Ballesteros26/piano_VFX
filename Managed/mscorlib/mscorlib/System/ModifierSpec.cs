using System;
using System.Text;

namespace System
{
	// Token: 0x02000247 RID: 583
	internal interface ModifierSpec
	{
		// Token: 0x06001B4F RID: 6991
		Type Resolve(Type type);

		// Token: 0x06001B50 RID: 6992
		StringBuilder Append(StringBuilder sb);
	}
}
