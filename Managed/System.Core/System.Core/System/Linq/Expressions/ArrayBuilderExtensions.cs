using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000252 RID: 594
	internal static class ArrayBuilderExtensions
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x00035CC0 File Offset: 0x00033EC0
		public static ReadOnlyCollection<T> ToReadOnly<T>(this ArrayBuilder<T> builder)
		{
			return new TrueReadOnlyCollection<T>(builder.ToArray());
		}
	}
}
