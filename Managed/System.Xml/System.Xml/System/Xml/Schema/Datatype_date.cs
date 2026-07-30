using System;

namespace System.Xml.Schema
{
	// Token: 0x020003C8 RID: 968
	internal class Datatype_date : Datatype_dateTimeBase
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000E478D File Offset: 0x000E298D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Date;
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000E4791 File Offset: 0x000E2991
		internal Datatype_date()
			: base(XsdDateTimeFlags.Date)
		{
		}
	}
}
