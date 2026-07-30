using System;

namespace System.Net
{
	// Token: 0x020004B9 RID: 1209
	internal struct HeaderVariantInfo
	{
		// Token: 0x060023AE RID: 9134 RVA: 0x0008AB01 File Offset: 0x00088D01
		internal HeaderVariantInfo(string name, CookieVariant variant)
		{
			this.m_name = name;
			this.m_variant = variant;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x0008AB11 File Offset: 0x00088D11
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0008AB19 File Offset: 0x00088D19
		internal CookieVariant Variant
		{
			get
			{
				return this.m_variant;
			}
		}

		// Token: 0x04001FDF RID: 8159
		private string m_name;

		// Token: 0x04001FE0 RID: 8160
		private CookieVariant m_variant;
	}
}
