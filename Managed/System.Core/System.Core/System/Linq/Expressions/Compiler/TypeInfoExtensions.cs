using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002E8 RID: 744
	internal static class TypeInfoExtensions
	{
		// Token: 0x060016BB RID: 5819 RVA: 0x0004A86B File Offset: 0x00048A6B
		public static Type MakeDelegateType(this DelegateHelpers.TypeInfo info, Type retType, params Expression[] args)
		{
			return info.MakeDelegateType(retType, args);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004A878 File Offset: 0x00048A78
		public static Type MakeDelegateType(this DelegateHelpers.TypeInfo info, Type retType, IList<Expression> args)
		{
			Type[] array = new Type[args.Count + 2];
			array[0] = typeof(CallSite);
			array[array.Length - 1] = retType;
			for (int i = 0; i < args.Count; i++)
			{
				array[i + 1] = args[i].Type;
			}
			return info.DelegateType = DelegateHelpers.MakeNewDelegate(array);
		}
	}
}
