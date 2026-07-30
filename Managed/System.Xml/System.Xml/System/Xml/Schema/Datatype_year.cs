using System;

namespace System.Xml.Schema
{
	// Token: 0x020003CA RID: 970
	internal class Datatype_year : Datatype_dateTimeBase
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000E47A7 File Offset: 0x000E29A7
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYear;
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000E47AB File Offset: 0x000E29AB
		internal Datatype_year()
			: base(XsdDateTimeFlags.GYear)
		{
		}
	}
}
