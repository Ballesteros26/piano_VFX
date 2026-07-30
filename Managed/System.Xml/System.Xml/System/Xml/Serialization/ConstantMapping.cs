using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002EC RID: 748
	internal class ConstantMapping : Mapping
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0009A01D File Offset: 0x0009821D
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0009A033 File Offset: 0x00098233
		internal string XmlName
		{
			get
			{
				if (this.xmlName != null)
				{
					return this.xmlName;
				}
				return string.Empty;
			}
			set
			{
				this.xmlName = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0009A03C File Offset: 0x0009823C
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x0009A052 File Offset: 0x00098252
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0009A05B File Offset: 0x0009825B
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x0009A063 File Offset: 0x00098263
		internal long Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04001619 RID: 5657
		private string xmlName;

		// Token: 0x0400161A RID: 5658
		private string name;

		// Token: 0x0400161B RID: 5659
		private long value;
	}
}
