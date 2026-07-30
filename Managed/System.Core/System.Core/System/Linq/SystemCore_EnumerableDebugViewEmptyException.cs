using System;

namespace System.Linq
{
	// Token: 0x020000E3 RID: 227
	internal sealed class SystemCore_EnumerableDebugViewEmptyException : Exception
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001AF8E File Offset: 0x0001918E
		public string Empty
		{
			get
			{
				return "Enumeration yielded no results";
			}
		}
	}
}
