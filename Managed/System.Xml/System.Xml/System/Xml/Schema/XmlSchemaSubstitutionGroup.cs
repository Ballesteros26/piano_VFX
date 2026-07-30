using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000485 RID: 1157
	internal class XmlSchemaSubstitutionGroup : XmlSchemaObject
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x0010A048 File Offset: 0x00108248
		[XmlIgnore]
		internal ArrayList Members
		{
			get
			{
				return this.membersList;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x0010A050 File Offset: 0x00108250
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x0010A058 File Offset: 0x00108258
		[XmlIgnore]
		internal XmlQualifiedName Examplar
		{
			get
			{
				return this.examplar;
			}
			set
			{
				this.examplar = value;
			}
		}

		// Token: 0x04001E26 RID: 7718
		private ArrayList membersList = new ArrayList();

		// Token: 0x04001E27 RID: 7719
		private XmlQualifiedName examplar = XmlQualifiedName.Empty;
	}
}
