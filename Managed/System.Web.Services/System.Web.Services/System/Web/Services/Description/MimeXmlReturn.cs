using System;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x020000DD RID: 221
	internal class MimeXmlReturn : MimeReturn
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00019212 File Offset: 0x00017412
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001921A File Offset: 0x0001741A
		internal XmlTypeMapping TypeMapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x0400039F RID: 927
		private XmlTypeMapping mapping;
	}
}
