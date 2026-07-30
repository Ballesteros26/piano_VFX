using System;

namespace System.Xml.Schema
{
	// Token: 0x020003C9 RID: 969
	internal class Datatype_yearMonth : Datatype_dateTimeBase
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000E479A File Offset: 0x000E299A
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYearMonth;
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000E479E File Offset: 0x000E299E
		internal Datatype_yearMonth()
			: base(XsdDateTimeFlags.GYearMonth)
		{
		}
	}
}
