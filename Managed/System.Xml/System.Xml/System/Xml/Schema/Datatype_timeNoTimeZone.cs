using System;

namespace System.Xml.Schema
{
	// Token: 0x020003C5 RID: 965
	internal class Datatype_timeNoTimeZone : Datatype_dateTimeBase
	{
		// Token: 0x0600265F RID: 9823 RVA: 0x000E4773 File Offset: 0x000E2973
		internal Datatype_timeNoTimeZone()
			: base(XsdDateTimeFlags.XdrTimeNoTz)
		{
		}
	}
}
