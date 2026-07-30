using System;

namespace System.Xml.Schema
{
	// Token: 0x020003CB RID: 971
	internal class Datatype_monthDay : Datatype_dateTimeBase
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x00002A0A File Offset: 0x00000C0A
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonthDay;
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000E47B5 File Offset: 0x000E29B5
		internal Datatype_monthDay()
			: base(XsdDateTimeFlags.GMonthDay)
		{
		}
	}
}
