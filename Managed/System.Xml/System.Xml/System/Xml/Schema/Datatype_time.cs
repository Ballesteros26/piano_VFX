using System;

namespace System.Xml.Schema
{
	// Token: 0x020003C7 RID: 967
	internal class Datatype_time : Datatype_dateTimeBase
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000E4789 File Offset: 0x000E2989
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Time;
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000E4780 File Offset: 0x000E2980
		internal Datatype_time()
			: base(XsdDateTimeFlags.Time)
		{
		}
	}
}
