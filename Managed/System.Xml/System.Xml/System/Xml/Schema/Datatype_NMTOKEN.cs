using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D7 RID: 983
	internal class Datatype_NMTOKEN : Datatype_token
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000296BA File Offset: 0x000278BA
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NmToken;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x00006B15 File Offset: 0x00004D15
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKEN;
			}
		}
	}
}
