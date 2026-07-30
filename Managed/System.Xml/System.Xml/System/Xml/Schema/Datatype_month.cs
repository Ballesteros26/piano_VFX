using System;

namespace System.Xml.Schema
{
	// Token: 0x020003CD RID: 973
	internal class Datatype_month : Datatype_dateTimeBase
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000E47C9 File Offset: 0x000E29C9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonth;
			}
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000E47CD File Offset: 0x000E29CD
		internal Datatype_month()
			: base(XsdDateTimeFlags.GMonth)
		{
		}
	}
}
