using System;
using System.Collections;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000075 RID: 117
	internal class SoapHeaderAttributeComparer : IComparer
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000D2FB File Offset: 0x0000B4FB
		public int Compare(object x, object y)
		{
			return string.Compare(((SoapHeaderAttribute)x).MemberName, ((SoapHeaderAttribute)y).MemberName, StringComparison.Ordinal);
		}
	}
}
