using System;

namespace System.Xml.Schema
{
	// Token: 0x0200041E RID: 1054
	internal sealed class SchemaNotation
	{
		// Token: 0x06002972 RID: 10610 RVA: 0x000FB020 File Offset: 0x000F9220
		internal SchemaNotation(XmlQualifiedName name)
		{
			this.name = name;
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000FB02F File Offset: 0x000F922F
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000FB037 File Offset: 0x000F9237
		// (set) Token: 0x06002975 RID: 10613 RVA: 0x000FB03F File Offset: 0x000F923F
		internal string SystemLiteral
		{
			get
			{
				return this.systemLiteral;
			}
			set
			{
				this.systemLiteral = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000FB048 File Offset: 0x000F9248
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x000FB050 File Offset: 0x000F9250
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x04001C47 RID: 7239
		internal const int SYSTEM = 0;

		// Token: 0x04001C48 RID: 7240
		internal const int PUBLIC = 1;

		// Token: 0x04001C49 RID: 7241
		private XmlQualifiedName name;

		// Token: 0x04001C4A RID: 7242
		private string systemLiteral;

		// Token: 0x04001C4B RID: 7243
		private string pubid;
	}
}
