using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DC RID: 220
	public class Asn1Set : Asn1Structured
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x000176B5 File Offset: 0x000158B5
		public Asn1Set()
			: base(Asn1Set.ID)
		{
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000176C2 File Offset: 0x000158C2
		public Asn1Set(int size)
			: base(Asn1Set.ID, size)
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000176D0 File Offset: 0x000158D0
		[CLSCompliant(false)]
		public Asn1Set(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Set.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000176E6 File Offset: 0x000158E6
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET: { ");
		}

		// Token: 0x040004B6 RID: 1206
		public const int TAG = 17;

		// Token: 0x040004B7 RID: 1207
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
