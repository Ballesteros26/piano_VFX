using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000108 RID: 264
	internal class EmptyEnumerable<T> : ParallelQuery<T>
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0001D648 File Offset: 0x0001B848
		private EmptyEnumerable()
			: base(QuerySettings.Empty)
		{
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001D655 File Offset: 0x0001B855
		internal static EmptyEnumerable<T> Instance
		{
			get
			{
				if (EmptyEnumerable<T>.s_instance == null)
				{
					EmptyEnumerable<T>.s_instance = new EmptyEnumerable<T>();
				}
				return EmptyEnumerable<T>.s_instance;
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001D673 File Offset: 0x0001B873
		public override IEnumerator<T> GetEnumerator()
		{
			if (EmptyEnumerable<T>.s_enumeratorInstance == null)
			{
				EmptyEnumerable<T>.s_enumeratorInstance = new EmptyEnumerator<T>();
			}
			return EmptyEnumerable<T>.s_enumeratorInstance;
		}

		// Token: 0x04000546 RID: 1350
		private static volatile EmptyEnumerable<T> s_instance;

		// Token: 0x04000547 RID: 1351
		private static volatile EmptyEnumerator<T> s_enumeratorInstance;
	}
}
