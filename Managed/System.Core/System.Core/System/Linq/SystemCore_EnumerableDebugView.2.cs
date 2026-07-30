using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000E4 RID: 228
	internal sealed class SystemCore_EnumerableDebugView
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x0001AF9D File Offset: 0x0001919D
		public SystemCore_EnumerableDebugView(IEnumerable enumerable)
		{
			if (enumerable == null)
			{
				throw Error.ArgumentNull("enumerable");
			}
			this._enumerable = enumerable;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001AFBC File Offset: 0x000191BC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public object[] Items
		{
			get
			{
				List<object> list = new List<object>();
				foreach (object obj in this._enumerable)
				{
					list.Add(obj);
				}
				if (list.Count == 0)
				{
					throw new SystemCore_EnumerableDebugViewEmptyException();
				}
				return list.ToArray();
			}
		}

		// Token: 0x040004DB RID: 1243
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable _enumerable;
	}
}
