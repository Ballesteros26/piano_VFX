using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D7 RID: 215
	public abstract class Asn1Numeric : Asn1Object
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x000173BF File Offset: 0x000155BF
		internal Asn1Numeric(Asn1Identifier id, int value_Renamed)
			: base(id)
		{
			this.content = (long)value_Renamed;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000173D0 File Offset: 0x000155D0
		internal Asn1Numeric(Asn1Identifier id, long value_Renamed)
			: base(id)
		{
			this.content = value_Renamed;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000173E0 File Offset: 0x000155E0
		public int intValue()
		{
			return (int)this.content;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000173E9 File Offset: 0x000155E9
		public long longValue()
		{
			return this.content;
		}

		// Token: 0x040004AD RID: 1197
		private long content;
	}
}
