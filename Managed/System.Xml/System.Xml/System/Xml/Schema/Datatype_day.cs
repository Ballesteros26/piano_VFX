using System;

namespace System.Xml.Schema
{
	// Token: 0x020003CC RID: 972
	internal class Datatype_day : Datatype_dateTimeBase
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000E3E97 File Offset: 0x000E2097
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GDay;
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000E47BF File Offset: 0x000E29BF
		internal Datatype_day()
			: base(XsdDateTimeFlags.GDay)
		{
		}
	}
}
