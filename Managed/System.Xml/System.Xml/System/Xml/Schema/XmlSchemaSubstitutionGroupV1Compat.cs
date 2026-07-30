using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000486 RID: 1158
	internal class XmlSchemaSubstitutionGroupV1Compat : XmlSchemaSubstitutionGroup
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x0010A07F File Offset: 0x0010827F
		[XmlIgnore]
		internal XmlSchemaChoice Choice
		{
			get
			{
				return this.choice;
			}
		}

		// Token: 0x04001E28 RID: 7720
		private XmlSchemaChoice choice = new XmlSchemaChoice();
	}
}
