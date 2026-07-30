using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200020F RID: 527
	internal struct Pair<T, U>
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x0002B980 File Offset: 0x00029B80
		public Pair(T first, U second)
		{
			this._first = first;
			this._second = second;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002B990 File Offset: 0x00029B90
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0002B998 File Offset: 0x00029B98
		public T First
		{
			get
			{
				return this._first;
			}
			set
			{
				this._first = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002B9A1 File Offset: 0x00029BA1
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0002B9A9 File Offset: 0x00029BA9
		public U Second
		{
			get
			{
				return this._second;
			}
			set
			{
				this._second = value;
			}
		}

		// Token: 0x0400082B RID: 2091
		internal T _first;

		// Token: 0x0400082C RID: 2092
		internal U _second;
	}
}
