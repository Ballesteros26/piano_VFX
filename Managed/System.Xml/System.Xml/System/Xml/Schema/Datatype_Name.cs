using System;

namespace System.Xml.Schema
{
	// Token: 0x020003D8 RID: 984
	internal class Datatype_Name : Datatype_token
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000E4AB4 File Offset: 0x000E2CB4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Name;
			}
		}
	}
}
