using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002DB RID: 731
	internal interface ILocalCache
	{
		// Token: 0x0600165E RID: 5726
		LocalBuilder GetLocal(Type type);

		// Token: 0x0600165F RID: 5727
		void FreeLocal(LocalBuilder local);
	}
}
