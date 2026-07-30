using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x020009D6 RID: 2518
	[DebuggerDisplay("{value}", Name = "[{key}]", Type = "")]
	internal class KeyValuePairs
	{
		// Token: 0x06005D0D RID: 23821 RVA: 0x00133472 File Offset: 0x00131672
		public KeyValuePairs(object key, object value)
		{
			this.value = value;
			this.key = key;
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x00133488 File Offset: 0x00131688
		public object Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06005D0F RID: 23823 RVA: 0x00133490 File Offset: 0x00131690
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04002F66 RID: 12134
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object key;

		// Token: 0x04002F67 RID: 12135
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object value;
	}
}
