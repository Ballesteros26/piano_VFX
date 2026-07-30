using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000E2 RID: 226
	internal sealed class SystemCore_EnumerableDebugView<T>
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0001AF59 File Offset: 0x00019159
		public SystemCore_EnumerableDebugView(IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				throw Error.ArgumentNull("enumerable");
			}
			this._enumerable = enumerable;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001AF77 File Offset: 0x00019177
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = this._enumerable.ToArray<T>();
				if (array.Length == 0)
				{
					throw new SystemCore_EnumerableDebugViewEmptyException();
				}
				return array;
			}
		}

		// Token: 0x040004DA RID: 1242
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable<T> _enumerable;
	}
}
