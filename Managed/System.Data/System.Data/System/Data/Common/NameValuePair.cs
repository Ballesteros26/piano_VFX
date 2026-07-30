using System;

namespace System.Data.Common
{
	// Token: 0x02000324 RID: 804
	[Serializable]
	internal sealed class NameValuePair
	{
		// Token: 0x0600248A RID: 9354 RVA: 0x000A7214 File Offset: 0x000A5414
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000A7231 File Offset: 0x000A5431
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x000A7239 File Offset: 0x000A5439
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000A7241 File Offset: 0x000A5441
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x000A7249 File Offset: 0x000A5449
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x000A7251 File Offset: 0x000A5451
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NameValuePairNext);
				}
				this._next = value;
			}
		}

		// Token: 0x040017D8 RID: 6104
		private readonly string _name;

		// Token: 0x040017D9 RID: 6105
		private readonly string _value;

		// Token: 0x040017DA RID: 6106
		private readonly int _length;

		// Token: 0x040017DB RID: 6107
		private NameValuePair _next;
	}
}
