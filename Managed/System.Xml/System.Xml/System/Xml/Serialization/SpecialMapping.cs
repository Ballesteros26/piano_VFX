using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F3 RID: 755
	internal class SpecialMapping : TypeMapping
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0009AC99 File Offset: 0x00098E99
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x0009ACA1 File Offset: 0x00098EA1
		internal bool NamedAny
		{
			get
			{
				return this.namedAny;
			}
			set
			{
				this.namedAny = value;
			}
		}

		// Token: 0x0400163D RID: 5693
		private bool namedAny;
	}
}
